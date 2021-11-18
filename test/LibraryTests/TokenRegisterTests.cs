using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase del registro de empresas.
    /// </summary>
    [TestFixture]
    public class TokenRegisterTests
    {
        /// <summary>
        /// Prueba que se agregue un token  al registro.
        /// </summary>
        [Test]
        public void AddTest()
        {
            Company company = new Company("empresa", new Location(), "rubro");
            string token = "145789";
            TokenRegister.Instance.Add(token, company);
            Assert.IsNotNull(TokenRegister.Instance.TokenList);
        }

        /// <summary>
        /// Prueba que se remueva un token  del registro de tokens.
        /// </summary>
        [Test]
        public void RemoveTest()
        {
            Company company = new Company("empresa", new Location(), "rubro");
            string token = "123459";
            TokenRegister.Instance.Add(token, company);
            TokenRegister.Instance.Remove(token);
            Assert.IsFalse(TokenRegister.Instance.IsValid(token));
        }

        /// <summary>
        /// Prueba que un token sea valido y devuelva la empresa correcta.
        /// </summary>
        [Test]
        public void IsValidTest()
        {
            Company company = new Company("empresa", new Location(), "rubro");
            string token = "245789";
            TokenRegister.Instance.Add(token, company);
            Assert.AreEqual(TokenRegister.Instance.GetCompany(token), company);
        }

        /// <summary>
        /// Prueba de que el token este en la lista de tokens.
        /// </summary>
        [Test]
        public void ContainsTest()
        {
            Company company = new Company("empresa", new Location(), "rubro");
            string token = "548796";
            TokenRegister.Instance.Add(token, company);
            Assert.True(TokenRegister.Instance.IsValid(token));
            Assert.False(TokenRegister.Instance.IsValid("54854456"));
        }

        // /// <summary>
        // /// Prueba de que se genere un token nuevo y se agregue a la lista de tokens.
        // /// </summary>
        // [Test]
        // public void GenerateTokenTest()
        // {
        //     Company company2 = new Company("empresa2", new Location(), "rubro", "materiales");
        //     string token = Singleton<TokenRegister>.Instance.GenerateToken(company2);
        //     Assert.AreEqual(company2, Singleton<TokenRegister>.Instance.IsValidToken(token));
        // }
    }
}