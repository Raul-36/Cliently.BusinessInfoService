using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application;
using Core.Businesses.Repositories.Base;
using Infrastructure.Businesses.Repositories;
using Core.InfoLists.Repositories.Base;
using Infrastructure.InfoLists.Repositories;
using Core.InfoTexts.Repositories.Base;
using Infrastructure.InfoTexts.Repositories;
using Core.DynamicItems.Repositories.Base;
using Infrastructure.DynamicItems.Repositories;
using Infrastructure.Messaging.Options;
using Infrastructure.Users.Options;
using Application.Common.Messaging.Consumers.Base;
using Infrastructure.Users.Consumers;
using Core.Users.Repositories;
using Infrastructure.Users.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);

builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.Configure<UserQueuesOptions>(builder.Configuration.GetSection("UserQueues"));

builder.Services.AddDbContext<BusinessInfoEFPostgreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserRepository, UserEFPostgreRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessEFPostgreRepository>();
builder.Services.AddScoped<IInfoListRepository, InfoListEFPostgreRepository>();
builder.Services.AddScoped<IInfoTextRepository, InfoTextEFPostgreRepository>();
builder.Services.AddScoped<IDynamicItemRepository, DynamicItemEFPostgreRepository>();

builder.Services.AddScoped<UserCreatedConsumer>();
builder.Services.AddScoped<UserDeletedConsumer>();

var app = builder.Build();
using var scope = app.Services.CreateScope();
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BusinessInfoEFPostgreContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        
        dbContext.Database.Migrate();
    }
}
using var consumersScope = app.Services.CreateScope();
{
    var baseConsumerType = typeof(IConsumer);
    var consumerTypes = AppDomain.CurrentDomain
        .GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Where(t => baseConsumerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
        .ToList();
    List<IConsumer> consumers = new();
    foreach (var consumerType in consumerTypes)
    {
        var consumerAsObject = consumersScope.ServiceProvider.GetRequiredService(consumerType);
        if (consumerAsObject is IConsumer consumer)
        {
            consumers.Add(consumer);
        }
        else
        {
            throw new InvalidOperationException($"Type {consumerType.FullName} does not implement IConsumer interface.");
        }
    }
    foreach (var consumer in consumers)
    {
        _ = Task.Run(async () => 
        {
            try
            {
                await consumer.ExecuteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in consumer {consumer.GetType().Name}: {ex}");
            }
        });
    }
    
}


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
