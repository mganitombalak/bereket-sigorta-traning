using System.ComponentModel.DataAnnotations;
using Bereket.Domain.Base;

namespace Bereket.Domain.Entity
{
    public class Product:BaseEntity<int> {
        [Required]
        public string Name{get;set;}
    }
}