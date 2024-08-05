using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Twcqrs.Models;

namespace Twcqrs.Queries
{
    public class GetTweetsByUserQuery : IRequest<List<Tweet>>
    {
        public string User { get; set; }

        public GetTweetsByUserQuery(string user)
        {
            User = user;
        }
    }
}