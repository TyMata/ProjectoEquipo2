using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="EntrepreneurRole"/>.
    /// </summary>
    [TestFixture]
    public class EntrepreneurRoleTests
    {
        private EntrepreneurRole role;
        private LocationAdapter location ;

        /// <summary>
        /// Se crea un EntrepreneurRole para las pruebas.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.location = LocationApiAdapter.Instance.GetLocation("address","city","department");
            this.role = new EntrepreneurRole("emprendedor", location, "headings", "habilitaciones");
        }

        /// <summary>
        /// Prueba que RoleType() devuelve la string "emprendedor".
        /// </summary>
        [Test]
        public void RoletypeTest()
        {
            string expected = this.role.RoleType();
            Assert.AreEqual(expected, "emprendedor");
        }

        /// <summary>
        /// Prueba que la información del usuario no este vacía.
        /// </summary>
        [Test]
        public void DataTest()
        {
            string expected = this.role.Data();
            Assert.IsNotEmpty(expected);
        }
    }
}