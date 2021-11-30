using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="EntrepreneurRole"/>.
    /// </summary>
    [TestFixture]
    public class EntrepreneurRoleTests
    {
        private EntrepreneurRole role;
        private LocationAdapter location;

        /// <summary>
        /// Se crea un EntrepreneurRole para las pruebas.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.location = new LocationAdapter("address", "city", "department");
            this.role = new EntrepreneurRole("emprendedor", "099088077", location, "headings", "habilitaciones");
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