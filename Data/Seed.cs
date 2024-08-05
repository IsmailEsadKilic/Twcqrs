using Twcqrs.Models;

namespace Twcqrs.Data
{
    public class Seed
    {
        public static List<Tweet> GetTweets()
        {
            var tweets = new List<Tweet>
            {
                new Tweet {
                    User = "kimchy",
                    PostDate = DateTime.Now,
                    Message = "trying out NEST"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
                new Tweet {
                    User = "kimchy",
                    PostDate = DateTime.Now,
                    Message = "Another tweet"
                },
                new Tweet {
                    User = "kimchy",
                    PostDate = DateTime.Now,
                    Message = "Hello Elastic"
                },
                new Tweet {
                    User = "Obama",
                    PostDate = DateTime.Now,
                    Message = "Change"
                },
                new Tweet {
                    User = "Kanye",
                    PostDate = DateTime.Now,
                    Message = "I am the best rapper"
                },
                new Tweet {
                    User = "Kanye",
                    PostDate = DateTime.Now,
                    Message = "Who was in paris?"
                },
                new Tweet {
                    User = "Sam Altman",
                    PostDate = DateTime.Now,
                    Message = "AI is the future"
                },
                new Tweet {
                    User = "Obama",
                    PostDate = DateTime.Now,
                    Message = "Yes we can"
                },
                new Tweet {
                    User = "Joe Mama",
                    PostDate = DateTime.Now,
                    Message = "Joe Biden is the best"
                },
                new Tweet {
                    User = "Elon",
                    PostDate = DateTime.Now,
                    Message = "Tesla is the future"
                },
                new Tweet {
                    User = "Elon",
                    PostDate = DateTime.Now,
                    Message = "SpaceX is the future"
                },
                new Tweet {
                    User = "Donald",
                    PostDate = DateTime.Now,
                    Message = "Make America Great Again"
                },
                new Tweet {
                    User = "Donald",
                    PostDate = DateTime.Now,
                    Message = "I won the election"
                },
                new Tweet {
                    User = "Donald",
                    PostDate = DateTime.Now,
                    Message = "I am the best president"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
                new Tweet {
                    User = "Is Heavy Update Out Yet",
                    PostDate = DateTime.Now,
                    Message = "No"
                },
            };

            foreach (var tweet in tweets)

            {
                var randomNumber = new Random().Next(-180, 180);
                tweet.PostDate = DateTime.Now + TimeSpan.FromDays(randomNumber);
                tweet.Id = 0;
            }

            return tweets;
        }
    }
}