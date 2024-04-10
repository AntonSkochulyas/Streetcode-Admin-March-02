using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.Dto.Sources
{
    public class CreateSourceLinkCategoryContentDto
    {
        public string? Title { get; set; }
        public int ImageId { get; set; }
        public int StreetcodeId { get; set; }
        public string? Text { get; set; }
    }
}
