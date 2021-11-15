using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="Material"/>.
    /// </summary>
    [TestFixture]
    public class UserTest
    {
        private IRole role;
        private int id;

        /// <summary>
        /// Set up del test de User.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.role = new AdminRole();
            this.id = 1;
        }

        /// <summary>
        /// Prueba para crear un user.
        /// </summary>
        [Test]
        public void CreateUserTest()
        {
            User usuario = new User(this.id, this.role);
            Assert.AreEqual(this.role, usuario.Role);
            Assert.AreEqual(this.id, usuario.Id);
        }
    }
}