using System.Text;
using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="CompanyRole"/>.
    /// </summary>
    [TestFixture]
    public class CompanyRoleTests
    {
        private Company company;
        private CompanyRole role;
        private Location location = new Location();

        /// <summary>
        /// Se crea una company y su role para las  pruebas.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.company = new Company("empresa", location, "headings");
            this.role = new CompanyRole(this.company);
        }

        /// <summary>
        /// Prueba que la empresa dentro de CompanyRole no sea nula.
        /// </summary>
        [Test]
        public void CompanyTests()
        {
            Assert.IsNotNull(role.Company);
        }

        /// <summary>
        /// Prueba que TipoRole() devuelve la string "company".
        /// </summary>
        [Test]
        public void RoleTypeTest()
        {
            string expected = this.role.RoleType();
            Assert.AreEqual(expected, "company");
        }

        /// <summary>
        /// Prueba que la información del usuario no este vacía.
        /// </summary>
        [Test]
        public void DataTest()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Nombre de la empresa que representa: {this.company.Name}\n")
                .Append($"Ubicacion de la empresa: {this.company.Locations}\n")
                .Append($"Rubro: {this.company.Headings}\n")
                .Append($"Materiales producidos por la empresa: {this.company.ProducedMaterials}");
            string expected = this.role.Data();
            Assert.AreEqual(expected, sb.ToString());
        }
    }
}