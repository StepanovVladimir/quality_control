using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests
{
    class Product
    {
        public string id { get; set; }
        public string category_id { get; set; }
        public string title { get; set; }
        public string alias { get; set; }
        public string content { get; set; }
        public string price { get; set; }
        public string old_price { get; set; }
        public string status { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public string hit { get; set; }
    }
}