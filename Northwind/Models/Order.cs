using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
               
        [StringLength(5)]
        public string CustomerID { get; set; }

        public virtual Customer Customer { get; set; }
       
        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
