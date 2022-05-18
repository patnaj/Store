using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Store.Models
{
    public class CustomerVM
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        
    }
}