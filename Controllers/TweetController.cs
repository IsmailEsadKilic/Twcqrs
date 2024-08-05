using MediatR;
using Microsoft.AspNetCore.Mvc;
using Twcqrs.Commands;
using Twcqrs.Queries;

namespace Twcqrs.Controllers
{
    public class TweetController : Controller
    {
        private readonly IMediator _mediator;

        public TweetController(IMediator mediator)
        {
            _mediator = mediator;
        }   
        
        [HttpGet("api/tweets")]
        public async Task<IActionResult> GetAllTweets()
        {
            var result = await _mediator.Send(new GetAllTweetsQuery());
            return Ok(result);
        }

        [HttpGet("api/tweets/user/{user}")]
        public async Task<IActionResult> GetTweetsByUser(string user)
        {
            var result = await _mediator.Send(new GetTweetsByUserQuery(user));
            return Ok(result);
        }

        [HttpPost("api/tweets")]
        public async Task<IActionResult> CreateTweet([FromBody] CreateTweetCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}