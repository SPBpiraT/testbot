using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace testbot.worker.Features
{
    public class MessageHandler : IHandler<Message>
    {
        private readonly ITelegramBotClient _botClient; 
        private readonly MessageService _messageService;

        public MessageHandler(ITelegramBotClient botClient, MessageService messageService)
        {
            _botClient = botClient;
            _messageService = messageService;
        }

        public async Task Handle(Message message, CancellationToken cancellationToken)
        {

            if (message.Type == MessageType.Text)
            {

            }

            if (message.Text == "o")
            {
                var xx = await _botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);

                var user = new UserDto(xx.User.Username, xx.User.Id, xx.User.FirstName, xx.User.LastName);

                await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"Добавлен новый пользователь. Username: {user.username} Id: {user.id} FirstName: {user.firstName} LastName: {user.lastName}",
                cancellationToken: cancellationToken);

            }

            if (message.Text == "grpc") 
            {
                var result = await _messageService.SayHelloAsync("GRPC", cancellationToken);
                await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"{result}",
                cancellationToken: cancellationToken);

            }
        }
    }

    public record UserDto(string username, long id, string firstName, string lastName) { }
}
