using MediatR;
using Twcqrs.Data;
using Twcqrs.Models;
using Nest;

namespace Twcqrs.Commands
{


    public class CreateTweetCommandHandler : BaseHandler, IRequestHandler<CreateTweetCommand, int>
    {
        private readonly DataContext _context;
        private readonly IElasticClient _client;
        public CreateTweetCommandHandler(DataContext context, IElasticClient client)
        {
            _context = context;
            _client = client;
        }

        public async Task<int> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
        {
            var tweet = new Tweet
            {
                User = request.User,
                PostDate = DateTime.Now,
                Message = request.Message
            };

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("DB");
            Console.ResetColor();
            await _context.Tweets.AddAsync(tweet);
            await _context.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("tweet.Id: " + tweet.Id);
            Console.ResetColor();
            if (tweet.Id > 0)
            {
                Console.WriteLine("ES");
                var response = await _client.IndexAsync(tweet, idx => idx.Index("tw"));

                if (!response.IsValid)
                {
                    throw new Exception("Error indexing tweet");
                }
            }
            return tweet.Id;
        }
    }
}