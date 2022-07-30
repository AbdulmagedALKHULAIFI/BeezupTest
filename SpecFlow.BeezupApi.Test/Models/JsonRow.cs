using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.BeezupApi.Test.Models
{
    public class JsonRow
    {
        public string sku { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string price { get; set; }

        public JsonRow(string sku, string title, string description, string price)
        {
            this.sku = sku;
            this.title = title;
            this.description = description;
            this.price = price;
        }

        public override bool Equals(Object obj)
        {
            if (obj is JsonRow)
            {
                var that = obj as JsonRow;
                return this.sku == that.sku && this.title == that.title && this.description == that.description && this.price == that.price;
            }

            return false;
        }

    }
}
