using AutoMapper;
using WebSockets.DTOs;
using WebSockets.GeneratedModels;

namespace WebSockets.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Child, Comment>()
                .ForMember(dest => dest.Subreddit, opt => opt.MapFrom(src => src.data.subreddit));
        }
    }
}
