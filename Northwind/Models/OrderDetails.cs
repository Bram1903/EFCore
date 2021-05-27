using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models
{
    [Table("Order Details")]
    public class OrderDetail
    {
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}
