using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase del registro de empresas.
    /// </summary>
    [TestFixture]
    public class CompanyRegisterTest
    {
        private Company company;
        private LocationAdapter location;

        /// <summary>
        /// Set up del test de CompanyRegister.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.location = LocationApiAdapter.Instance.GetLocation("address","city","department");
            this.company = new Company("nombre", this.location, "rubro");
        }

        /// <summary>
        /// Prueba que se agregue una empresa al registro.
        /// </summary>
        [Test]
        public void AddTest()
        {
            CompanyRegister.Instance.Add(this.company);
            Assert.IsTrue(CompanyRegister.Instance.CompanyList.Contains(this.company));
        }

        /// <summary>
        /// Prueba que se remueva una empresa del registro de empresas.
        /// </summary>
        [Test]
        public void RemoveTest()
        {
            CompanyRegister.Instance.Add(this.company);
            CompanyRegister.Instance.Remove(this.company);
            Assert.IsFalse(CompanyRegister.Instance.Contains(this.company));
        }

        /// <summary>
        /// Prueba que GetCompanyByUserId devuelva una empresa, y que sea la correcta.
        /// </summary>
        public void GetCompanyByUserIdTest()
        {
            Users user = new Users(1234567, new CompanyRole(this.company));
            company.CompanyUsers.Add(user);
            Company result = CompanyRegister.Instance.GetCompanyByUserId(1234567);
            Assert.AreEqual(this.company, result);
        }
    }
}