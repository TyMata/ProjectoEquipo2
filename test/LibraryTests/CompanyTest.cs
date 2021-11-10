using System;
using ClassLibrary;
using System.Collections.Generic;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;


namespace Tests
{
    /// <summary>
    /// Test de la clase <see cref="Company"/>
    /// </summary>
    [TestFixture]
    public class CompanyTests
    {
        private int id;
        private string name;
        private Location locations;
        private string headings;
        private string materials;
     
        [SetUp]
        public void SetUp()
        {
            this.name = "empresa";
            this.id = 12345678;
            this.headings = "rubro";
            this.materials = "material";
            this.locations = new Location();
            

        }
 
        public void CompanyNameTest()
        {
            Company company = new Company(null, this.locations, this.headings , this.materials);
            Assert.IsNotNull(company.Name);
        }
        /// <summary>
        /// Prueba que se añada el usuario a los usuarios de la empresa
        /// </summary>
        public void AddUserTest()
        {
            Company company = new Company(null, this.locations, this.headings , this.materials);
            IRole empresarole = new CompanyRole(company);
            User user = new User( 1234567, empresarole);
            company.AddUser(user);
            Assert.IsNotEmpty(company.CompanyUsers);
            
        }
        /// <summary>
        /// Prueba que se remueva un usuario determinado de los usuarios de la empresa
        /// </summary>
        public void RemoveUserTest()
        {
            Company company = new Company(null, this.locations, this.headings , this.materials);
            IRole empresarole = new CompanyRole(company);
            User user = new User( 1234567,empresarole);
            company.AddUser(user);
            company.RemoveUser(user.Id);
            Assert.IsNotEmpty(company.CompanyUsers);
        }


        /// <summary>
        /// Prueba que se agreguen los materiales a la lista de materiales
        /// </summary>
        public void ProducedMaterialsTest()
        {
            this.materials = "madera";
            Company company = new Company(this.name, this.locations, this.headings , this.materials);
            Assert.IsNotEmpty(company.ProducedMaterials);
        }
    }
}