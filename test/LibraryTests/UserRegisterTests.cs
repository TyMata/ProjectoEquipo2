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
        private static UserRegisterTests instance;
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
            UserRegister.Instance.Add(this.user);
            Assert.IsNotNull(UserRegister.Instance.DataUsers);
        }

        /// <summary>
        /// Prueba que se remueva un user de la lista de users, si esta en esta.
        /// </summary>
        [Test]
        public void RemoveTest()
        {
            UserRegister.Instance.Add(this.user);
            UserRegister.Instance.Remove(this.user);
            Assert.IsFalse(UserRegister.Instance.ContainsUser(this.user));   
        }

        /// <summary>
        /// Prueba que GetUserById devuelva un user y que sea el correcto.
        /// </summary>
        [Test]
        public void GetUserByIdTest()
        {
            UserRegister.Instance.Add(this.user);           
            Assert.IsNotNull(UserRegister.Instance.GetUserById(1234567));
        }
    }
}