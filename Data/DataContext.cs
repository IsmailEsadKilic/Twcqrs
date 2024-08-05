using Microsoft.EntityFrameworkCore;
using Twcqrs.Models;

namespace Twcqrs.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Tweet> Tweets { get; set; }
    }
}  