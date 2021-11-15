using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase del registro de empresas
    /// </summary>
    [TestFixture]
    public class TokenRegisterTests
    {   

        /// <summary>
        /// Prueba que se agregue un token  al registro
        /// </summary>
        [Test]
        public void AddTest()
        {    
            Company company = new Company("empresa", new Location(), "rubro", "materiales");
            string token = "145789";
            Singleton<TokenRegister>.Instance.Add(token, company);
            Assert.IsNotNull(Singleton<TokenRegister>.Instance.TokenList);
        }

        /// <summary>
        /// Prueba que se remueva un token  del registro de tokens
        /// </summary>
        [Test]
        public void RemoveTest()
        {
            Company company = new Company("empresa", new Location(), "rubro", "materiales");
            string token = "123459";
            Singleton<TokenRegister>.Instance.Add(token,company);
            Singleton<TokenRegister>.Instance.Remove(token);
            Assert.IsFalse(Singleton<TokenRegister>.Instance.Contains(token));
        }

        /// <summary>
        /// prueba que un token sea valido y devuelva la empresa correcta
        /// </summary>
        [Test]
       public void IsValidTest()
        {
            Company company = new Company("empresa", new Location(), "rubro", "materiales");
            string token = "245789";
            Singleton<TokenRegister>.Instance.Add(token, company);
            Assert.AreEqual(Singleton<TokenRegister>.Instance.IsValidToken(token),company);
        }

        /// <summary>
        /// Prueba de que el token este en la lista de tokens
        /// </summary>
        [Test]
        public void ContainsTest()
        {
            Company company = new Company("empresa", new Location(), "rubro", "materiales");
            string token = "548796";
            Singleton<TokenRegister>.Instance.Add(token,company);
            Assert.True(Singleton<TokenRegister>.Instance.Contains(token)) ;
            Assert.False(Singleton<TokenRegister>.Instance.Contains("54854456")) ;
        }
        
        /// <summary>
        /// Prueba de que se genere un token nuevo y se agregue a la lista de tokens
        /// </summary>
        [Test]
        public void GenerateTokenTest()
        {   Company company2 = new Company("empresa2", new Location(), "rubro", "materiales");
            string token = Singleton<TokenRegister>.Instance.GenerateToken(company2);
            Assert.AreEqual(company2,Singleton<TokenRegister>.Instance.IsValidToken(token));
        }
    }
}