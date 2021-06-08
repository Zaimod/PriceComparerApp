using System;
using System.Collections.Generic;
using System.Text;

namespace PriceComparerApp.Models.DataTransferObjects
{
    public class CatalogDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double NewPrice { get; set; }
        public string Url { get; set; }
        public string UrlImageShop { get; set; }
        public bool FreeDelivery { get; set; }
        public bool Discount { get; set; }
        public int NumbReviews { get; set; }
        public DateTime LastModified { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }

        public string NewPriceStr { get; set; }
    }
}
