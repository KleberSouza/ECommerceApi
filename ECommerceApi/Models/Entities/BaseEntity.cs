using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}