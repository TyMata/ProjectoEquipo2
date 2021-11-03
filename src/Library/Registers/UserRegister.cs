using System;
using System.Collections.Generic;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de usuarios
    /// </summary>
    public class UserRegister
    {   
        /// <summary>
        /// Lista de usuarios registrados
        /// </summary>
        /// <value></value>
        public static List<User> DataUsers { get; private set; }
    }
}