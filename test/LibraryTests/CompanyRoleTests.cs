using System;
using NUnit.Framework;
using ClassLibrary;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="CompanyRole"/>
    /// </summary>
    [TestFixture]
    public class CompanyRoleTests
    {
        private Company company;
        private CompanyRole role;
        private Location location = new Location();
        /// <summary>
        /// Se crea una company y su role para las  pruebas
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.company = new Company("empresa", location ,"headings","material");
            
        }

        /// <summary>
        /// Prueba que la empresa dentro de CompanyRole no sea nula
        /// </summary>
        public void CompanyTests()
        {
            role = new CompanyRole(company);
            Assert.IsNotNull(role.Company);
        }
        /// <summary>
        /// Prueba que TipoRole() devuelve la string "company"
        /// </summary>
        public void RoleTypeTest()
        {
            role = new CompanyRole(company);
            string expected = this.role.RoleType();
            Assert.AreEqual(expected, "company");
        }

        /// <summary>
        /// Prueba qu ela información del usuario no este vacía
        /// </summary>
        public void DataTest()
        {
            role = new CompanyRole(company);
            string expected = this.role.Data();
            Assert.IsNotEmpty(expected);
        }
    }
}