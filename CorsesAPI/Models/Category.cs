using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CorsesAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // This solution for API don't read 
        [JsonIgnore]
        public List<Course>? Courses { get; set; }
    }
}
