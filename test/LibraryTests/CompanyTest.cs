using ClassLibrary;
using NUnit.Framework;

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
        private string email;
        private string phoneNumber;
        private Company company;
        private Material material;

        /// <summary>
        /// Se crean variables con los parametros para crear una empresa.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.name = "empresa";
            this.id = 12345678;
            this.headings = "rubro";
            this.email = "company@gmail.com";
            this.phoneNumber = "091919191";
            this.location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            this.company = new Company(this.name, this.location, this.headings, this.email, this.phoneNumber);
            material = new Material("material","type","classification");
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
            Company company = new Company(this.name, this.location, this.headings, this.email, this.phoneNumber);
            company.ProducedMaterials.Add(new Material());
            Assert.IsNotEmpty(company.ProducedMaterials);
        }

        /// <summary>
        /// Prueba que el metodo GetMAterial de company devuelva el material correcto y que no sea nulo.
        /// </summary>
        [Test]
        public void GetMaterialTest()
        {
            this.company.ProducedMaterials.Add(material);
            Material material2 = this.company.GetMaterial("material");
            Assert.AreEqual(material.Name, material2.Name);
            Assert.IsNotNull(material2);
        }
        /// <summary>
        /// Prueba que el metódo GetLocation devuelva la locación correcta.
        /// </summary>
        [Test]
        public void GetLocationTest()
        {
            LocationAdapter result = this.company.GetLocation("Comandante Braga 2715");
            Assert.AreEqual(this.location, result);
        }
        /// <summary>
        /// Prueba que el metódo AddMaterial devuelva el nombre del material,su tipo y 
        /// clasificación.
        /// </summary>
        [Test]
        public void AddMaterialTest()
        {
            this.company.AddMaterial("Pallet","Madera","Residuo");
            Assert.IsTrue(this.company.ProducedMaterials.Exists(material => material.Name == "Pallet" && material.Type == "Madera" && material.Classification == "Residuo"));
        }
    }
}