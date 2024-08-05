using MediatR;
using Twcqrs.Models;

namespace Twcqrs.Queries
{

    public class GetAllTweetsQuery : IRequest<List<Tweet>>
    {
    }
}