using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Store.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public float VAT { get; set; }
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
    }
}