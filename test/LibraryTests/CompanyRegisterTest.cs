using System;
using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase del registro de empresas
    /// </summary>
    [TestFixture]
    public class CompanyRegisterTests
    {
        
        [SetUp]
        public void SetUp()
        {
            
        }

        /// <summary>
        /// Prueba que se agregue una empresa al registro
        /// </summary>
        [Test]
        public void AddTest()
        {   
            Company company = new Company();
            Singleton<CompanyRegister>.Instance.Add(company);
            Assert.IsNotNull(Singleton<CompanyRegister>.Instance.CompanyList);
        }

        /// <summary>
        /// Prueba que se remueva una empresa del registro de empresas
        /// </summary>
        [Test]
        public void RemoveTest()
        {
            Company company = new Company();
            Singleton<CompanyRegister>.Instance.Add(company);
            Singleton<CompanyRegister>.Instance.Remove(company);
            Assert.IsEmpty(Singleton<CompanyRegister>.Instance.CompanyList);
        }

        /// <summary>
        /// Prueba que GetCompanyByUserId devuelva una empresa, y que sea la correcta
        /// </summary>
        public void GetCompanyByUserIdTest()
        {
            Company company = new Company("empresa",new Location(), "rubro", "materiales");
            User user = new User(1234567, new CompanyRole(company));
            company.CompanyUsers.Add(user);
            Company result = Singleton<CompanyRegister>.Instance.GetCompanyByUserId(1234567);
            Assert.AreEqual(company,result);
        }
    }
}