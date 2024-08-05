using MediatR;

namespace Twcqrs.Commands
{
    public class CreateTweetCommand : IRequest<int>
    {
        public string User { get; set; }
        public string Message { get; set; }

        public CreateTweetCommand(string user, string message)
        {
            User = user;
            Message = message;
        }
    }
}