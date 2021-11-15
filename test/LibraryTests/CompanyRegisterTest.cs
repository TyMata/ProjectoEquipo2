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

        /// <summary>
        /// Set up del test de CompanyRegister.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.company = new Company("nombre", new Location(), "rubro", "material");
        }

        /// <summary>
        /// Prueba que se agregue una empresa al registro.
        /// </summary>
        [Test]
        public void AddTest()
        {
            Singleton<CompanyRegister>.Instance.Add(this.company);
            Assert.IsTrue(Singleton<CompanyRegister>.Instance.CompanyList.Contains(this.company));
        }

        /// <summary>
        /// Prueba que se remueva una empresa del registro de empresas.
        /// </summary>
        [Test]
        public void RemoveTest()
        {
            Singleton<CompanyRegister>.Instance.Remove(this.company);
            Assert.IsFalse(Singleton<CompanyRegister>.Instance.CompanyList.Contains(this.company));
        }

        /// <summary>
        /// Prueba que GetCompanyByUserId devuelva una empresa, y que sea la correcta.
        /// </summary>
        public void GetCompanyByUserIdTest()
        {
            User user = new User(1234567, new CompanyRole(this.company));
            company.CompanyUsers.Add(user);
            Company result = Singleton<CompanyRegister>.Instance.GetCompanyByUserId(1234567);
            Assert.AreEqual(this.company, result);
        }
    }
}