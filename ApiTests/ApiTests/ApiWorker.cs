using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiTests
{
    class ApiWorker
    {
        public Product GetProduct(int id)
        {
            WebClient client = new WebClient();
            string source = client.DownloadString("http://52.136.215.164:9000/api/products");
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(source);

            string strId = id.ToString();
            for (int i = products.Count - 1; i >= 0; --i)
            {
                if (products[i].id == strId)
                {
                    return products[i];
                }
            }
            throw new Exception("No such id");
        }

        public ResponseData AddProduct(Product product)
        {
            return PostProduct("http://52.136.215.164:9000/api/addproduct", product);
        }

        public ResponseData EditProduct(Product product)
        {
            return PostProduct("http://52.136.215.164:9000/api/editproduct", product);
        }

        public ResponseData DeleteProduct(int id)
        {
            WebRequest request = WebRequest.Create("http://52.136.215.164:9000/api/deleteproduct?id=" + id.ToString());
            WebResponse response = request.GetResponse();
            ResponseData data;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    data = JsonSerializer.Deserialize<ResponseData>(reader.ReadToEnd());
                }
            }

            response.Close();
            request.Abort();

            return data;
        }

        private ResponseData PostProduct(string uri, Product product)
        {
            string json = JsonSerializer.Serialize(product);

            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";
            using (Stream stream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                }
            }

            WebResponse response = request.GetResponse();
            ResponseData data;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    data = JsonSerializer.Deserialize<ResponseData>(reader.ReadToEnd());
                }
            }

            response.Close();
            request.Abort();

            return data;
        }
    }
}
