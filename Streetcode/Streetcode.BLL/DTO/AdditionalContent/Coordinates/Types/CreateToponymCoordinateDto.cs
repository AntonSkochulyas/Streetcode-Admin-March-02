using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types
{
    public class CreateToponymCoordinateDto : CreateCoordinateDto
    {
        public int? ToponymId { get; set; }
    }
}
