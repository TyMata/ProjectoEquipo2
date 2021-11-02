namespace ClassLibrary
{
    public class AdminRole : IRole
    {
        /// <summary>
        /// Envia un token para que una compania se pueda unir al bot
        /// </summary>
        public void InviteCompany(){}

        /*IHandler CreateCoR(IMessageChannel channel)
        {

        }*/
        public string TipoRol()
        {
            return "admin";
        }
    }
}