using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace JobsBotApp.features.telegram.service {
    internal class TelegramServiceImpl : ITelegramService {

        private readonly IConfiguration _configuration;

        public TelegramServiceImpl(IConfiguration configuration) {
            _configuration = configuration;
        }



        public async Task SendMessageAsync(string message) {
            var token = _configuration["Telegram:BotToken"]
                         ?? throw new InvalidOperationException("Telegram BotToken no configurado.");

            var chatId = _configuration["Telegram:ChatId"]
                         ?? throw new InvalidOperationException("Telegram ChatId no configurado.");

            var botClient = new TelegramBotClient(token!);

            await botClient.SendMessage(
                chatId: chatId!,
                text: message);
        }
    }
}
