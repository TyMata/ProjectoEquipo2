using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Primer Handler de la CoR.
    /// </summary>
    public class StartHandler : AbstractHandler
    {
        /// <summary>
        /// Estado para el handler de StartHandler.
        /// </summary>
        /// <value></value>
        public StartState State { get; set; }

        /// <summary>
        /// Constructor de los objetos StartHandler.
        /// </summary>
        public StartHandler()
        {
            this.Command = "/menu";
            this.State = StartState.Start;
        }
        /// <summary>
        /// Le otorga por pantalla los comandos que puede utilizar.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input,out string response)
        {
            if (this.State == StartState.Start && this.nextHandler != null) // TODO ver como hacer este handler o como mandlre el next handler
            {
                StringBuilder menu = new StringBuilder("Bienvenido\n");
                menu.Append("Usuario No Registrado:\n")
                    .Append("   /usuarioempresanoregistrado\n")
                    .Append("   /emprendedornoregistrado\n\n")
                    .Append("Usuario Admin:\n")
                    .Append("   /registrarempresa\n")
                    .Append("   /eliminarusuario\n")
                    .Append("   /eliminarempresa\n\n")
                    .Append("Usuario de una Empresa:\n")
                    .Append("   /agregarmaterial\n")
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
                this.State = StartState.NotFirstTime;
                response = menu.ToString();
                return true;
            }
            else if(this.State == StartState.NotFirstTime && CanHandle(input))
            {
                IMessage input2 = input;
                StringBuilder menu = new StringBuilder("Usuario No Registrado\n");
                menu.Append("   /usuarioempresanoregistrado\n")
                    .Append("   /emprendedornoregistrado\n\n")
                    .Append("Usuario Admin:\n")
                    .Append("   /registrarempresa\n")
                    .Append("   /eliminarusuario\n")
                    .Append("   /eliminarempresa\n\n")
                    .Append("Usuario de una Empresa:\n")
                    .Append("   /agregarmaterial\n")
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
                response = menu.ToString();
                return true;
            }
            response = "";
            return false;
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando StartHandler.
        /// </summary>
        public enum StartState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pide un comando a ejecutar y pasa al siguiente estado.
            /// </summary>
            Start,
            NotFirstTime
        }
        /// <summary>
        /// Se crea la clase RemoveUserData para cuando se desea eliminar un usuario  de la UserData.
        /// </summary>
        public class RemoveUserData
        {
            public int User { get; set; }      //TODO Es necesario un data aca???
        }
    }
}