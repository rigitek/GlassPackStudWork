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
        public int Amount { get; set; }
        public double Price { get; set; }

        public int ArticleNum { get; set; }

        public Brand? Brand { get; set; }
        public Provider? Provider { get; set; }
    }
}
