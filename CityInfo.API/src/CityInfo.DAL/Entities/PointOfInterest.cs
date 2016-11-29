using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.DAL.Entities
{
	public class PointOfInterest : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[ForeignKey("CityId")]
		public virtual City City { get; set; }
		public int CityId { get; set; }
	}
}
