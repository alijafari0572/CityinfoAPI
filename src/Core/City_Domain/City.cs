using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace City_Domain
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }

        public City(string name)
        {
            this.Name = name;
        }
        #region  Relation
        public ICollection<PointOfInterest> PointOfInterest { get; set; }
        = new List<PointOfInterest>();
    }
    #endregion
}
