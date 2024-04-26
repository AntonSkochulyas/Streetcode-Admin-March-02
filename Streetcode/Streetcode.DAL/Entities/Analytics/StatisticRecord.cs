using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;

namespace Streetcode.DAL.Entities.Analytics
{
    [Table("qr_coordinates", Schema = "coordinates")]
    public class StatisticRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QrId { get; set; }
        public string? Address { get; set; }
        public int StreetcodeCoordinateId { get; set; }
        public StreetcodeCoordinate? StreetcodeCoordinate { get; set; }
     }
}
