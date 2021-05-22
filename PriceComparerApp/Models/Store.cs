using System;
using System.Collections.Generic;
using System.Text;

namespace PriceComparerApp.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Catalog> catalog { get; set; } = new List<Catalog>();
    }
}
