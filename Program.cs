using Microsoft.EntityFrameworkCore;
using Twcqrs.Data;
using Twcqrs.Models;
using Nest;
using Microsoft.Extensions.DependencyInjection;
using Twcqrs.Queries;
using Twcqrs.Commands;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

var elasticClientSettings = new ConnectionSettings
(new Uri("https://localhost:9200"))
    .CertificateFingerprint("d55da857b5bd0a839583780072f8f4c27ca86678db2be8ea6bc9ac17399b0f2a")
    .BasicAuthentication("elastic", "DfvyQnCmenY0rtOBiXQ1");

var elasticClient = new ElasticClient(elasticClientSettings);
var deleteResponse1 = elasticClient.DeleteByQuery<Tweet>(d => d
    .Query(q => q.MatchAll())
    .Index("tw")
);
var deleteResponse2 = elasticClient.DeleteByQuery<Tweet>(d => d
    .Query(q => q.MatchAll())
    .Index("tweets")
);

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine(deleteResponse1.ToString() + "\n" + deleteResponse2.ToString);
Console.ResetColor();

builder.Services.AddSingleton<IElasticClient>(elasticClient);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add your MediatR handlers here
builder.Services.AddScoped<CreateTweetCommandHandler>();
builder.Services.AddScoped<GetAllTweetsQueryHandler>();
builder.Services.AddScoped<GetTweetsByUserQueryHandler>();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    var e = scope.ServiceProvider.GetRequiredService<IElasticClient>();
    if (!await context.Tweets.AnyAsync())
    {
        await context.Tweets.AddRangeAsync(
            Seed.GetTweets()
        );
        await context.SaveChangesAsync();
    }
    var dbTweets = await context.Tweets.ToListAsync();
    //update elastic search // do not duplicate

    foreach (var tweet in dbTweets)
    {
        var response = await e.IndexAsync(tweet, idx => idx.Index("tw"));
        if (!response.IsValid)
        {
            throw new Exception("Error indexing tweet");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();