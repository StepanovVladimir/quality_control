using System;
using System.Configuration;
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

            ResponseData data = apiWorker.AddProduct(product);

            product.alias = ConfigurationManager.AppSettings["alias1"];
            AssertProducts(product, apiWorker.GetProduct(data.id));

            apiWorker.DeleteProduct(data.id);
        }

        [TestMethod]
        public void DeleteProduct_SomeProduct_ProductHasDeleted()
        {
            Product product = CreateSomeProduct();
            ResponseData data = apiWorker.AddProduct(product);

            apiWorker.DeleteProduct(data.id);

            Action action = () => apiWorker.GetProduct(data.id);
            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void EditProduct_ChangeTitle_TitleHasChanged()
        {
            Product product = CreateSomeProduct();
            ResponseData data = apiWorker.AddProduct(product);
            product.id = data.id.ToString();
            product.title = ConfigurationManager.AppSettings["title2"];

            apiWorker.EditProduct(product);

            product.alias = ConfigurationManager.AppSettings["alias2"];
            AssertProducts(product, apiWorker.GetProduct(data.id));

            apiWorker.DeleteProduct(data.id);
        }



        private ApiWorker apiWorker = new ApiWorker();

        private Product CreateSomeProduct()
        {
            return new Product
            {
                category_id = ConfigurationManager.AppSettings["category_id"],
                title = ConfigurationManager.AppSettings["title1"],
                content = ConfigurationManager.AppSettings["content"],
                price = ConfigurationManager.AppSettings["price"],
                old_price = ConfigurationManager.AppSettings["old_price"],
                status = ConfigurationManager.AppSettings["status"],
                keywords = ConfigurationManager.AppSettings["keywords"],
                description = ConfigurationManager.AppSettings["description"],
                hit = ConfigurationManager.AppSettings["hit"]
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
