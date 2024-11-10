using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;
using testbot.worker.Features;

namespace testbot.worker
{
    public class TelegramBotBackgroundService : BackgroundService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TelegramBotBackgroundService> _logger;

        public TelegramBotBackgroundService(
            ITelegramBotClient botClient,
            IServiceScopeFactory scopeFactory,
            ILogger<TelegramBotBackgroundService> logger)
        {
            _botClient = botClient;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Опции для настройки телеграма
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = [] //Указываем какие типы сообщений мы можем принимать. В данном случае бот будет на все типы сообщений реагировать.
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                await _botClient.ReceiveAsync(
                    updateHandler: HandleUpdateAsync,
                    pollingErrorHandler: HandleErrorAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: stoppingToken);
            }


        }

        //Обработчик обновлений в чате. Как бот обрабатывает сообщения.
        private async Task HandleUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            var scope = _scopeFactory.CreateScope();

            var messageHandler = scope.ServiceProvider.GetRequiredService<IHandler<Message>>();

            //В зависимости от того какое приходит сообщение мы по разному как-то на него реагируем.
            //CallbackQuery - это если пользователь нажимает какую-то кнопку из InlineKeyboard(кнопочки которые рисует бот). То есть callback это то что отправляется боту
            //при нажатии этой кнопки.
            var handler = update switch
            {
                { ChannelPost: { } post } => messageHandler.Handle(post, cancellationToken),
                { Message: { } message } => messageHandler.Handle(message, cancellationToken),
                { CallbackQuery: { } query } => CallbackQueryHandler(query, cancellationToken),
                _ => UnknownUpdateHandlerAsync(update, cancellationToken)
            };

            await handler;
        }

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unknown type message");
            return Task.CompletedTask;
        }

        private async Task CallbackQueryHandler(CallbackQuery query, CancellationToken cancellationToken)
        {
            if (query.Message is not { } message) //Если query.Message не является пустым объектом типа Message
                return;

            switch (query.Data)
            {
                case "lessons-info":
                    await _botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Вот тебе информация о занятиях, дорогой друг",
                        cancellationToken: cancellationToken);
                    return;
            }
        }

        private Task HandleErrorAsync(
            ITelegramBotClient botClient,
            Exception exception,
            CancellationToken cancellationToken)
        {
            switch (exception)
            {
                case ApiRequestException apiRequestException:
                    _logger.LogError(
                        apiRequestException,
                        "Telegram API Error:\n[{errorCode}]\n{message}",
                        apiRequestException.ErrorCode,
                        apiRequestException.Message);
                    return Task.CompletedTask;

                default:
                    _logger.LogError(exception, "Error while processing message in telegram bot");
                    return Task.CompletedTask;
            }
        }
    }
}
