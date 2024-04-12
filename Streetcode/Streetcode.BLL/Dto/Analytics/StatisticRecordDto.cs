﻿namespace Streetcode.BLL.Dto.Analytics
{
    public class StatisticRecordDto
    {
        public int Id { get; set; }
        public int QrId { get; set; }
        public int StreetcodeId { get; set; }
        public int StreetcodeCoordinateId { get; set; }
        public string? Address { get; set; }
    }
}
