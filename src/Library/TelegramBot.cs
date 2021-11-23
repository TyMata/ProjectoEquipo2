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
           this.handlers = new UnregisteredCompanyUserHandler();
           this.handlers
                .SetNext(new UnregisteredEntrepeneurUserHandler()
                .SetNext(new AddCompanyHandler()
                .SetNext(new RemoveUserHandler()
                .SetNext(new RemoveCompanyHandler()
                .SetNext(new PublishOfferHandler()
                .SetNext(new RemoveOfferHandler()
                .SetNext(new SuspendOfferHandler()
                .SetNext(new ResumeOfferHandler()
                .SetNext(new ModifyHabilitationsHandler()
                .SetNext(new ModifyPriceHandler()
                .SetNext(new ModifyQuantityHandler()
                .SetNext(new ShowCompanyOffersHandler()
                .SetNext(new ActiveOfferHandler()
                .SetNext(new SearchOfferHandler()))))))))))))));
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

        public int BotId
        {
            get
            {
                return this.BotInfo.Id;
            }
        }

        public string BotName
        {
            get
            {
                return this.BotInfo.FirstName;
            }
        }

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
            //Client.OnMessage += OnMessage;
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

       // private void OnMessage(object sender, MessageEventArgs messageEventArgs)
       private async Task HandleMessageReceived(Message message)
        {
            // IMessageChannel mc = new TelegramBotMessageChannel();
            // IHandler handlers = new AddCompanyHandler(mc);
            // handlers.SetNext(new RemoveUserHandler(mc)
            //         .SetNext(new RemoveCompanyHandler(mc)
            //         .SetNext(new EndHandler(mc, null))));


            //Message message = messageEventArgs.Message;
            int chatId = Convert.ToInt32(message.Chat.Id);
            
            string answer;
            IMessage message1 = new TelegramBotMessage(chatId, message.Text);
            StringBuilder menu = new StringBuilder("Bienvenido\n");
            menu.Append("Usuario No Registrado:\n")
                .Append("   /empresanoregistrada\n")
                .Append("   /emprendedornoregistrado\n\n")
                .Append("Usuario Admin:\n")
                .Append("   /registrarempresa\n")
                .Append("   /eliminarusuario\n")
                .Append("   /eliminarempresa\n\n")
                .Append("Usurio de una Empresa:\n")
                .Append("   /publicaroferta\n")
                .Append("   /retiraroferta\n")
                .Append("   /suspenderoferta\n")
                .Append("   /reanudaroferta\n")
                .Append("   /mostrarofertas\n\n")
                .Append("   Para modificar alguna oferta:\n")
                .Append("       /modificarhabilitaciones\n")
                .Append("       /modificarprecio\n")
                .Append("       /modificarcantidad\n\n")
                .Append("Usuario Emprendedor:\n")
                .Append("   /buscaroferta");
            Client.SendTextMessageAsync(chatId, menu.ToString());
            if (handlers.Handle(message1,out answer) != null)
            {
                Client.SendTextMessageAsync(chatId, answer);
            }
            else
            {
                Client.SendTextMessageAsync(chatId, "No puedo manejar ese mensaje");
            }
        }

        // /// <summary>
        // /// Maneja las actualizaciones del bot (todo lo que llega), incluyendo mensajes, ediciones de mensajes,
        // /// respuestas a botones, etc. En este ejemplo sólo manejamos mensajes de texto.
        // /// </summary>
        // public static async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        // {
        //     try
        //     {
        //         // Sólo respondemos a mensajes de texto
        //         if (update.Type == UpdateType.Message)
        //         {
        //             await HandleMessageReceived(update.Message);
        //         }
        //     }
        //     catch(Exception e)
        //     {
        //         await HandleErrorAsync(e, cancellationToken);
        //     }
        // }

        // /// <summary>
        // /// Maneja los mensajes que se envían al bot.
        // /// Lo único que hacemos por ahora es escuchar 3 tipos de mensajes:
        // /// - "hola": responde con texto
        // /// - "chau": responde con texto
        // /// - "foto": responde con una foto
        // /// </summary>
        // /// <param name="message">El mensaje recibido</param>
        // /// <returns></returns>
        // private static async Task HandleMessageReceived(Message message)
        // {
        //     Console.WriteLine($"Received a message from {message.From.FirstName} saying: {message.Text}");

        //     string response = string.Empty;

        //     firstHandler.Handle(message, out response);

        //     if (!string.IsNullOrEmpty(response))
        //     {
        //         await Client.SendTextMessageAsync(message.Chat.Id, response);
        //     }
        // }

        // /// <summary>
        // /// Manejo de excepciones. Por ahora simplemente la imprimimos en la consola.
        // /// </summary>
        // public static Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        // {
        //     if (exception is null)
        //     {
        //         throw new ArgumentNullException(paramName: nameof(exception));
        //     }
        //     Console.WriteLine(exception.Message);
        //     return Task.CompletedTask;

        // }
    }
}


    