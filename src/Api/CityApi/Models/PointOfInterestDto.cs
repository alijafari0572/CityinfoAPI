using System.ComponentModel.DataAnnotations;

namespace CityinfoAPI.Models
{
    public class PointOfInterestDto
    {
        [Required]
        [MaxLength(100)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

    }
}
