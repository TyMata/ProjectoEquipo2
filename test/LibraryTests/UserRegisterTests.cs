using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase UserRegister.
    /// </summary>
    [TestFixture]
    public class UserRegisterTests
    {
        private User user;

        /// <summary>
        /// Se crea una instancia de User para los tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.user = new User(1234567, new CompanyRole(new Company("empresa", new Location(), "rubro", "materiales")));
        }

        /// <summary>
        /// Prueba que se añada un user a la lista de users.
        /// </summary>
        [Test]
        public void AddTest()
        {
            Singleton<UserRegister>.Instance.Add(this.user);
            Assert.IsNotNull(Singleton<UserRegister>.Instance.DataUsers);
        }

        /// <summary>
        /// Prueba que se remueva un user de la lista de users, si esta en esta.
        /// </summary>
        [Test]
        public void RemoveTest()
        {
            Singleton<UserRegister>.Instance.Add(this.user);
            Singleton<UserRegister>.Instance.Remove(this.user);
            Assert.IsFalse(Singleton<UserRegister>.Instance.Contains(this.user));   
        }

        /// <summary>
        /// Prueba que GetUserById devuelva un user y que sea el correcto.
        /// </summary>
        [Test]
        public void GetUserByIdTest()
        {
            Singleton<UserRegister>.Instance.Add(this.user);
            
            // User result = Singleton<UserRegister>.Instance.GetUserById(1234567);
            Assert.IsNotNull(Singleton<UserRegister>.Instance.GetUserById(1234567));
        }
    }
}