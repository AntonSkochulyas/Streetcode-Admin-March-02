using Streetcode.DAL.Enums;

namespace Streetcode.BLL.Util
{
    public class DateToStringConverter
    {
        public static string FromDateToString(DateTime date, DateViewPattern pattern)
        {
            return pattern switch
            {
                DateViewPattern.Year => date.ToString("yyyy"),
                DateViewPattern.MonthYear => date.ToString("yyyy, MMMM"),
                DateViewPattern.SeasonYear => $"{GetSeason(date)} {date.Year}",
                DateViewPattern.DateMonthYear => date.ToString("yyyy, d MMMM"),
                _ =>""
            };
        }

        private static string GetSeason(DateTime dateTime)
        {
            int month = dateTime.Month;

            switch (month)
            {
                case 12:
                case 1:
                case 2:
                    return "зима";
                case 3:
                case 4:
                case 5:
                    return "весна";
                case 6:
                case 7:
                case 8:
                    return "літо";
                default:
                    return "осінь";
            }
        }
    }
}
