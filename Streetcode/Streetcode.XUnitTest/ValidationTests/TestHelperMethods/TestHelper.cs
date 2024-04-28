using Streetcode.BLL.Dto.AdditionalContent.Tag;
using Streetcode.DAL.Entities.Streetcode.TextContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.XUnitTest.ValidationTests.TestHelperMethods
{
    public static class TestHelper
    {
        public static string CreateStringWithSpecificLength(int length)
        {
            string character = "A";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(character);
            }

            return result.ToString();
        }

        public static IEnumerable<StreetcodeTagDto> CreateStreetcodeTagDtoCollectionWithSpecificLength(int length)
        {
            var list = new List<StreetcodeTagDto>(length);

            for (int i = 1; i <= length; i++)
            {
                list.Add(new StreetcodeTagDto { Id = i });
            }

            return list;
        }
    }
}
