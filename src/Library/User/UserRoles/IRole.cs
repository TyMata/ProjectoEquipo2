namespace ClassLibrary
{   
    /// <summary>
    /// Interface para los roles
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// Se devuelve TipoRol
        /// </summary>
        /// <returns></returns>
        string TipoRol();
        /// <summary>
        /// Nos devuelve la data del usuario en string
        /// </summary>
        /// <returns></returns>
        string Data();
    }
}