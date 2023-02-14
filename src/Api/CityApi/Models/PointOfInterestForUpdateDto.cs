using System.ComponentModel.DataAnnotations;

namespace CityApi.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "Name Ra  Vared Konid")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
