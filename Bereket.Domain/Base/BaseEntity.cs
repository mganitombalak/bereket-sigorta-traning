using System.ComponentModel.DataAnnotations;
using Bereket.Domain.Interfaces;

namespace Bereket.Domain.Base{
    public abstract class BaseEntity<TId> : IBaseEntity<TId>
    {
        [Key]
        public TId Id { get; set; }
    }
}