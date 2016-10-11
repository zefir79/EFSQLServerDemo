using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Domain.Object
{
    public class OrderItem
    {
        public virtual int OrderItemId { get; set; }
        public virtual Order Order { get; set; }
        public virtual int OrderId { get; set; }
        public string ItemDescription { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
    }
}
