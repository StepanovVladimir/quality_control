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

            Assert.AreEqual(apiWorker.GetProduct(data.id).title, product.title);

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
        public void AddProduct_SomeProduct_AliasHasCreated()
        {
            Product product = CreateSomeProduct();

            ResponseData data = apiWorker.AddProduct(product);

            Assert.AreEqual(apiWorker.GetProduct(data.id).alias, ConfigurationManager.AppSettings["alias"]);

            apiWorker.DeleteProduct(data.id);
        }

        [TestMethod]
        public void EditProduct_ChangeTitle_TitleHasChanged()
        {
            Product product = CreateSomeProduct();
            ResponseData data = apiWorker.AddProduct(product);
            product.id = data.id.ToString();
            product.title = ConfigurationManager.AppSettings["title2"];

            apiWorker.EditProduct(product);

            Assert.AreEqual(apiWorker.GetProduct(data.id).title, product.title);

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
    }
}