namespace Twcqrs.Data
{
    public abstract class BaseHandler
    {
        protected int TweetCount { get; set; }
        protected int UserTweetCount { get; set; }

        protected BaseHandler()
        {
            TweetCount = 0;
            UserTweetCount = 0;
        }
    }
}