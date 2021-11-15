using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="Material"/>.
    /// </summary>
    [TestFixture]
    public class MaterialTest
    {
        private string name;
        private string type;
        private string classification;

        /// <summary>
        /// Se crean variables con los parametro para crear un objeto material.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // this.client = new LocationApiClient();
            this.type = "madera";
            this.classification = "organico";
            this.name = "nombre";
        }

        /// <summary>
        /// Prueba para crear un material.
        /// </summary>
        [Test]
        public void CreateMaterialTest()
        {
            Material materialCreado = new Material(this.name, this.type, this.classification);
            Assert.AreEqual(this.name, materialCreado.Name);
            Assert.AreEqual(this.type, materialCreado.Type);
            Assert.AreEqual(this.classification, materialCreado.Classification);
        }
    }
}