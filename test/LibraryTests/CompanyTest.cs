using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Test de la clase <see cref="Company"/>.
    /// </summary>
    [TestFixture]
    public class CompanyTest
    {
        private int id;
        private string name;
        private LocationAdapter location;
        private string headings;
        private Company company;

        /// <summary>
        /// Se crean variables con los parametros para crear una empresa.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.name = "empresa";
            this.id = 12345678;
            this.headings = "rubro";
            this.location = new LocationAdapter("address","city","department");;
            this.company = new Company(this.name, this.location, this.headings);
        }

        /// <summary>
        /// Prueba que el nombre no sea nulo.
        /// </summary>
        [Test]
        public void CompanyNameTest()
        {
            Assert.IsNotNull(company.Name);
        }

        /// <summary>
        /// Prueba que se añada el usuario a los usuarios de la empresa.
        /// </summary>
        [Test]
        public void AddUserTest()
        {
            IRole empresarole = new CompanyRole(this.company);
            Users user = new Users(1234567, empresarole);
            company.AddUser(user.Id);
            Assert.IsNotEmpty(company.CompanyUsers);
        }

        /// <summary>
        /// Prueba que se remueva un usuario determinado de los usuarios de la empresa.
        /// </summary>
        [Test]
        public void RemoveUserTest()
        {
            IRole empresarole = new CompanyRole(this.company);
            Users user = new Users(223456, empresarole);
            company.AddUser(user.Id);
            company.RemoveUser(user.Id);
            Assert.IsFalse(this.company.CompanyUsers.Contains(user));
        }

        /// <summary>
        /// Prueba que se agreguen los materiales a la lista de materiales.
        /// </summary>
        [Test]
        public void ProducedMaterialsTest()
        {
            Company company = new Company(this.name, this.location, this.headings);
            company.ProducedMaterials.Add(new Material());
            Assert.IsNotEmpty(company.ProducedMaterials);
        }
    }
}