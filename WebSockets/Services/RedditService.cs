using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using WebSockets.Helpers;
using WebSockets.Interfaces;
using Newtonsoft.Json;
using WebSockets.GeneratedModels;
using AutoMapper;
using WebSockets.DTOs;

namespace WebSockets.Services
{
    public class RedditService : IRedditService
    {
        private readonly IOptions<RedditSettings> config;
        private readonly IMessageProcessor messageProcessor;
        private readonly IMapper mapper;

        public RedditService(IOptions<RedditSettings> config, IMessageProcessor msesageProcessor, IMapper mapper)
        {
            this.config = config;
            this.messageProcessor = msesageProcessor;
            this.mapper = mapper;
        }

        /// <summary>
        /// Starts the process of fetching data from Reddit
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            var accessToken = await GetAccessTokenAsync();
            if (accessToken == null)
            {
                Console.WriteLine("Failed to obtain access token");
                return;
            }

            await GetData(accessToken);
        }

        /// <summary>
        /// Reach out to reddit via HTTP Get request to get a valid token using a username, password, clientId and secret
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetAccessTokenAsync()
        {
            var accessToken = string.Empty;

            var clientId = config.Value.ClientId;
            var clientSecret = config.Value.ClientSecret;
            var credentials = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");
            var base64Credentials = Convert.ToBase64String(credentials);

            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, config.Value.TokenURL);
            tokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            tokenRequest.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", config.Value.Username),
                new KeyValuePair<string, string>("password", config.Value.Password)
            });

            using var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(tokenRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                accessToken = JsonDocument.Parse(responseContent).RootElement.GetProperty("access_token").GetString();
            }

            return accessToken;
        }

        /// <summary>
        /// Makes the call to get the data from reddit and then hands off the data to a new thread to process
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private async Task GetData(string accessToken)
        {
            string data = string.Empty;

            #region WebSockets
            //webSocket = new ClientWebSocket();
            //webSocket.Options.SetRequestHeader("Authorization", $"Bearer {accessToken}");

            //await webSocket.ConnectAsync(new Uri(""), CancellationToken.None);

            //while(webSocket.State == WebSocketState.Open)
            //{
            //    var buffer = new ArraySegment<byte>(new byte[1024]);
            //    var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            //    Console.WriteLine(Encoding.UTF8.GetString(buffer.Array, 0, result.Count));
            //}

            //await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            #endregion

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("EmploymentTestbyujwfindlay", "1.0"));

            while (true)
            {
                try
                {
                    var dataRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(config.Value.DataURL),
                        Headers = { Authorization = new AuthenticationHeaderValue($"bearer{accessToken}") }
                    };

                    var response = await httpClient.SendAsync(dataRequest);


                    if (!response.IsSuccessStatusCode)
                    {
                        //If access token is invalid or has been revoked request a new one
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            accessToken = await GetAccessTokenAsync();
                            var getDataWithNewAccessTokenRequest = new HttpRequestMessage
                            {
                                Method = HttpMethod.Get,
                                RequestUri = new Uri(config.Value.DataURL),
                                Headers = { Authorization = new AuthenticationHeaderValue($"bearer{accessToken}") }
                            };

                            response = await httpClient.SendAsync(getDataWithNewAccessTokenRequest);
                        }
                        else
                        {
                            throw new Exception($"Http request failed to {dataRequest.RequestUri} with status code: {response.StatusCode}");
                        }
                    }

                    data = await response.Content.ReadAsStringAsync();
                    var messages = JsonConvert.DeserializeObject<Rootobject>(data);
                   
                    var comments = mapper.Map<List<Child>, List<Comment>>(messages.data.children.ToList());

                    //Start a new thread so the main thread does not have to wait for this execution to finish
                    _ = Task.Run(() => messageProcessor.ProcessMessages(comments));
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"An HTTP error occurred: {ex.Message}");
                }
                catch (Newtonsoft.Json.JsonException ex)
                {
                    Console.WriteLine($"A JSON parsing error occurred: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error has occurred: {ex.Message}");
                }

                //Handles the throttling to not violate the 60 requests per minute
                await Task.Delay(TimeSpan.FromSeconds(config.Value.CallBackDuration));
            }
        }
    }
}
