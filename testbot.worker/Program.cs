using Microsoft.Extensions.Options;
using Telegram.Bot.Types;
using Telegram.Bot;
using testbot.worker.Features;
using testbot.worker;
using testbot.worker.Options;
using testbot.sdk;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TelegramBotBackgroundService>();

builder.Services.AddTransient<ITelegramBotClient, TelegramBotClient>(serviceProvider =>
{
    var token = serviceProvider.GetRequiredService<IOptions<TelegramOptions>>().Value.Token;

    return new(token);
});

builder.Services.AddGrpcSdk();

builder.Services.AddTransient<IHandler<Message>, MessageHandler>();
builder.Services.AddScoped<MessageService>();

//”казываем конфигурацию где лежит токен
builder.Services.Configure<TelegramOptions>(builder.Configuration.GetSection(TelegramOptions.Telegram));

var host = builder.Build();
host.Run();
