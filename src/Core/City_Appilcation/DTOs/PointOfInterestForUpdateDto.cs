using System.ComponentModel.DataAnnotations;

namespace City_Appilcation.DTOs
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
