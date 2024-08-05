using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nest;
using Twcqrs.Data;
using Twcqrs.Models;

namespace Twcqrs.Queries
{
    public class GetAllTweetsQueryHandler : BaseHandler, IRequestHandler<GetAllTweetsQuery, List<Tweet>>
    {
        private readonly DataContext _context;
        private readonly IElasticClient _client;
        public GetAllTweetsQueryHandler(DataContext context, IElasticClient client)
        {
            _context = context;
            _client = client;
        }
        public async Task<List<Tweet>> Handle(GetAllTweetsQuery query, CancellationToken cancellationToken)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("ES");
            Console.ResetColor();
            var searchResponse = await _client.SearchAsync<Tweet>(s => s
                .Index("tw")
                .Query(q => q
                    .MatchAll()
                )
                .Size(1000)
            );

            if (searchResponse.IsValid && searchResponse.Documents.Count > 0)
            {
                return searchResponse.Documents.ToList();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("DB");
                Console.ResetColor();
                return await _context.Tweets.ToListAsync();
            }
        }
    }
}