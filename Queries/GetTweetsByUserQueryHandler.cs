using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nest;
using Twcqrs.Data;
using Twcqrs.Models;

namespace Twcqrs.Queries
{
    public class GetTweetsByUserQueryHandler : BaseHandler, IRequestHandler<GetTweetsByUserQuery, List<Tweet>>
    {
        private readonly DataContext _context;
        private readonly IElasticClient _client;
        public GetTweetsByUserQueryHandler(DataContext context, IElasticClient client)
        {
            _context = context;
            _client = client;
        }
        public async Task<List<Tweet>> Handle(GetTweetsByUserQuery query, CancellationToken cancellationToken)
        {
            if (query.User == "ls")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("ES");
                Console.ResetColor();
                var eResponse = await _client.SearchAsync<Tweet>(s => s
                .Index("tw")
                .Query(q => q
                    .MatchAll()
                )
                .Size(1000));

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("DB");
                Console.ResetColor();
                var sResponse = await _context.Tweets.ToListAsync();

                var x = new Tweet
                {
                    Id = 0,
                    User = "-------------------------------------",
                    PostDate = DateTime.Now,
                    Message = "-------------------------------------"
                };

                sResponse.Add(x);

                foreach (var y in eResponse.Documents)
                {
                    sResponse.Add(y);
                }

                return sResponse;
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("ES");
            Console.ResetColor();
            var searchResponse = await _client.SearchAsync<Tweet>(s => s
                .Index("tw")
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.User)
                        .Query(query.User)
                    )
                ).Size(1000)
            );

            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.ToList();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("DB");
                Console.ResetColor();
                return await _context.Tweets.Where(t => t.User == query.User).ToListAsync();
            }
        }
    }
}