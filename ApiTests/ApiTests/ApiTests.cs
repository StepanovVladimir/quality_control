using System;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests
{
    [TestClass]
    public class ApiTests
    {
        [TestMethod]
        public void AddProduct_SomeProduct_ProductHasAdded()
        {
            Product product = CreateSomeProduct();

            id = apiWorker.AddProduct(product).id;

            product.alias = "stepanov-s-title-1";
            AssertProducts(product, apiWorker.GetProduct(id));
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "../../positiveTestData.csv", "positiveTestData#csv", DataAccessMethod.Sequential)]
        public void EditProduct_PositiveData_ProductHasChanged()
        {
            Product product = CreateProductFromContext();

            apiWorker.EditProduct(product);

            product.alias = Convert.ToString(TestContext.DataRow[9]);
            AssertProducts(product, apiWorker.GetProduct(id));
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "../../negativeTestData.csv", "negativeTestData#csv", DataAccessMethod.Sequential)]
        public void EditProduct_NegativeData_ThrowsExceptions()
        {
            Product product = CreateProductFromContext();

            Action action = () => apiWorker.EditProduct(product);

            Assert.ThrowsException<JsonException>(action, Convert.ToString(TestContext.DataRow[9]));
        }

        [TestMethod]
        public void DeleteProduct_SomeProduct_ProductHasDeleted()
        {
            apiWorker.DeleteProduct(id);

            Action action = () => apiWorker.GetProduct(id);
            Assert.ThrowsException<Exception>(action);
        }

        public TestContext TestContext { get; set; }

        private static int id;

        private ApiWorker apiWorker = new ApiWorker();

        private Product CreateSomeProduct()
        {
            return new Product
            {
                category_id = "1",
                title = "Stepanov's title 1",
                content = "Content 1",
                price = "1",
                old_price = "1",
                status = "1",
                keywords = "Keywords 1",
                description = "Description 1",
                hit = "1"
            };
        }

        private Product CreateProductFromContext()
        {
            return new Product
            {
                id = id.ToString(),
                category_id = Convert.ToString(TestContext.DataRow[0]),
                title = Convert.ToString(TestContext.DataRow[1]),
                content = Convert.ToString(TestContext.DataRow[2]),
                price = Convert.ToString(TestContext.DataRow[3]),
                old_price = Convert.ToString(TestContext.DataRow[4]),
                status = Convert.ToString(TestContext.DataRow[5]),
                keywords = Convert.ToString(TestContext.DataRow[6]),
                description = Convert.ToString(TestContext.DataRow[7]),
                hit = Convert.ToString(TestContext.DataRow[8])
            };
        }

        private void AssertProducts(Product product1, Product product2)
        {
            Assert.AreEqual(product1.category_id, product2.category_id);
            Assert.AreEqual(product1.title, product2.title);
            Assert.AreEqual(product1.alias, product2.alias);
            Assert.AreEqual(product1.content, product2.content);
            Assert.AreEqual(product1.price, product2.price);
            Assert.AreEqual(product1.old_price, product2.old_price);
            Assert.AreEqual(product1.status, product2.status);
            Assert.AreEqual(product1.keywords, product2.keywords);
            Assert.AreEqual(product1.description, product2.description);
            Assert.AreEqual(product1.hit, product2.hit);
        }
    }
}
