using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de crear usuarios y 
    /// </summary>
    public class CreateUserServiceProvider
    {
        public static void CreateCompanyUser(IMessage input,Company company)
        {
            IRole rol = new CompanyRole(company);
            User usuario = new User(input.Id, rol);
            
        }
        
    }
}