using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CorsesAPI.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; } // in hours

        [StringLength(100)]
        public string Instructor { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string ThumbnailUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Category? Category { get; set; }
    }
}
