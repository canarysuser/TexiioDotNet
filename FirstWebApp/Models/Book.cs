using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebApp.Models
{
    [Table(name:"Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [StringLength(50), Required]
        public string Title { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }
        public decimal Price { get; set; }

        [StringLength(50),Required]
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; } 

        public int PublisherID { get; set; }
        public short ItemsInStock { get;set; }

        public bool IsInPrint { get; set; }

    }
}
