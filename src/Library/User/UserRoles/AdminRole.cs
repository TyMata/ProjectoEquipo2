namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa el rol de Admin
    /// </summary>
    public class AdminRole : IRole
    {
        /// <summary>
        /// Envia un token para que una compania se pueda unir al bot
        /// </summary>
        public void InviteCompany(){}

        
        /// <summary>
        /// Devuelve el tipo de Rol como string
        /// </summary>
        /// <returns></returns>
        public string TipoRol()
        {
            return "admin";
        }
    }
}