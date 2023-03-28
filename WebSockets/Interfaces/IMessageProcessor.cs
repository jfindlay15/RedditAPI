using WebSockets.DTOs;
using WebSockets.GeneratedModels;

namespace WebSockets.Interfaces
{
    public interface IMessageProcessor
    {        
        void ProcessMessages(List<Comment> comments);
    }
}
