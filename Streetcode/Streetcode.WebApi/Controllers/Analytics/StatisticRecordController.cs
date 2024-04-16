// Necessary usings
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create;
using Streetcode.BLL.MediatR.Analytics.StatisticRecords.Delete;
using Streetcode.BLL.MediatR.Analytics.StatisticRecords.GetAll;

namespace Streetcode.WebApi.Controllers.Analytics
{
    /// <summary>
    /// Controller that handles endpoints for:
    /// - Create statistic record;
    /// - Delete statistic record;
    /// - Get all statistic records.
    /// </summary>
    public class StatisticRecordController : BaseApiController
    {
        // Create statistic record
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatisticRecordDto statisticRecordDto)
        {
            return HandleResult(await Mediator.Send(new CreateStatisticRecordCommand(statisticRecordDto)));
        }

        // Get all statistic records
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllStatisticRecordsQuery()));
        }

        // Delete statistic record by id
        [Authorize("Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteStatisticRecordCommand(id)));
        }
    }
}
