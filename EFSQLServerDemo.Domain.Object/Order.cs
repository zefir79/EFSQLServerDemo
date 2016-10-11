using System;
using System.Collections.Generic;

namespace EFSQLServerDemo.Domain.Object
{
    public class Order
    {
        public virtual int OrderId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual DateTime? ProcessDate { get; set; }
        public virtual bool GiftPackaging { get; set; }
        public virtual int ShippingServiceId { get; set; }
        public virtual ShippingService ShippingService { get; set; }
        public virtual string FulfilledBy { get; set; }
        public virtual decimal TotalOrderCost { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderAddress> OrderAddresses { get; set; }
        public virtual ICollection<OrderPayment> OrderPayments { get; set; }

    }
}
