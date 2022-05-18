using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string IssuedForGuid { get; set; }
        public string IssuedByGuid { get; set; }
        public virtual IList<InvoiceItem> Items { get; set; }
        [ForeignKey("IssuedForGuid")]
        public virtual Customer IssuedFor { get; set; }
        [ForeignKey("IssuedByGuid")]
        public virtual Employee IssuedBy { get; set; }
    }
}