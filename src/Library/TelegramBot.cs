using System;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using System.Threading;
using Telegram.Bot.Extensions.Polling;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;


namespace ClassLibrary
{
    public class TelegramBot
    {

        private const string TELEGRAM_BOT_TOKEN = "2111701435:AAGo9zhSgJ-c7gteyA8xWopkMDrsgyScUGE";
        private static TelegramBot instance;

        private TelegramBot()
        {
            this.Client = new TelegramBotClient(TELEGRAM_BOT_TOKEN);
            // StartCommunication();
            this.handlers = new StartHandler();
            this.handlers
                .SetNext(new UnregisteredCompanyUserHandler())
                .SetNext(new UnregisteredEntrepreneurUserHandler())
                .SetNext(new AddCompanyHandler())
                .SetNext(new RemoveUserHandler())
                .SetNext(new RemoveCompanyHandler())
                .SetNext(new PublishOfferHandler())
                .SetNext(new RemoveOfferHandler())
                .SetNext(new SuspendOfferHandler())
                .SetNext(new ResumeOfferHandler())
                .SetNext(new ModifyHabilitationsHandler())
                .SetNext(new ModifyPriceHandler())
                .SetNext(new ModifyQuantityHandler())
                .SetNext(new ShowCompanyOffersHandler())
                .SetNext(new ShowCompanySoldOffersHandler())
                .SetNext(new SearchOfferHandler())
                .SetNext(new AddMaterialHandler())
                .SetNext(new ShowBoughtOffersHandler());
        }

        public ITelegramBotClient Client { get; private set; }
        private CancellationTokenSource cts = new CancellationTokenSource();

        private User BotInfo
        {
            get
            {
                return this.Client.GetMeAsync().Result;
            }
        }

        /// <summary>
        /// Id del Bot.
        /// </summary>
        /// <value></value>
        public int BotId
        {
            get
            {
                return this.BotInfo.Id;
            }
        }

        /// <summary>
        /// Nombre del bot.
        /// </summary>
        /// <value></value>
        public string BotName
        {
            get
            {
                return this.BotInfo.FirstName;
            }
        }

        /// <summary>
        /// Se crea un Singleton de la clase TelegramBot.
        /// </summary>
        /// <value></value>
        public static TelegramBot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TelegramBot();
                }
                return instance;
            }
        }

        private IHandler handlers {get; set;}           
    
        public void StartCommunication()
        {
            Client.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token);
        }

        public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            try
            {
                // Sólo respondemos a mensajes de texto
                if (update.Type == UpdateType.Message)
                {
                    await HandleMessageReceived(update.Message);
                }
            }
            catch(Exception e)
            {
                await HandleErrorAsync(e, cancellationToken);
            }
        }
        /// <summary>
        /// Manejo de excepciones. Por ahora simplemente la imprimimos en la consola.
        /// </summary>
        public Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }

       private async Task HandleMessageReceived(Message message)
        {
            int chatId = Convert.ToInt32(message.Chat.Id);
            
            string answer;
            IMessage message1 = new TelegramBotMessage(chatId, message.Text);
            
            if (handlers.Handle(message1,out answer) != null)
            {
                Client.SendTextMessageAsync(chatId, answer);
            }
            else
            {
                Client.SendTextMessageAsync(chatId, "No puedo manejar ese mensaje");
            }            
        }
    }
}


    