using System;
using System.Collections.Generic;
using System.Text;

namespace PriceComparerApp.Models.DataTransferObjects
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string img { get; set; }
        public string description { get; set; }
        public string exclude { get; set; }
        public int numbReviews { get; set; }
    }
}
