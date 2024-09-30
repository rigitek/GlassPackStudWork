using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassPack.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Shelf { get; set; }
        public string? Unit { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public int ArticleNum { get; set; }

        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
