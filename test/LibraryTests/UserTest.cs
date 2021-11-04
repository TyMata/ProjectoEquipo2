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
        public IRole Role;
        public int id;
        IRole role = new AdminRole();
        [SetUp]
        public void Setup()
        {
            this.Role = role;
            this.id = 1;
        }
        [Test]

        public void CreateUserTest()
        {
            User usuario = new User(this.id, this.Role);
            Assert.AreEqual(this.role,usuario.Role);
            Assert.AreEqual(this.id,usuario.Id);
        }

    }
}