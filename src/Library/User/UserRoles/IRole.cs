namespace ClassLibrary
{   
    /// <summary>
    /// Interface para los roles
    /// </summary>
    public interface IRole
    {
        //IHandler CreateCoR(IMessageChannel channel);
        /// <summary>
        /// Se devuelve TipoRol
        /// </summary>
        /// <returns></returns>
        string TipoRol();
        
    }
}