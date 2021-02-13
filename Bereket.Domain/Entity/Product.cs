using System.ComponentModel.DataAnnotations;

namespace Bereket.Domain.Entity
{
    public class Product {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Name{get;set;}
    }
}