using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoryById;
using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoriesByStreetcodeId;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetCategoryContentByStreetcodeId;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Update;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Delete;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create;

namespace Streetcode.WebApi.Controllers.Source
{
    public class SourcesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllNames()
        {
            return HandleResult(await Mediator.Send(new GetAllCategoryNamesQuery()));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return HandleResult(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetCategoryByIdQuery(id)));
        }

        [HttpGet("{categoryId:int}&{streetcodeId:int}")]
        public async Task<IActionResult> GetCategoryContentByStreetcodeId([FromRoute] int streetcodeId, [FromRoute] int categoryId)
        {
            return HandleResult(await Mediator.Send(new GetCategoryContentByStreetcodeIdQuery(streetcodeId, categoryId)));
        }

        [HttpGet("{streetcodeId:int}")]
        public async Task<IActionResult> GetCategoriesByStreetcodeId([FromRoute] int streetcodeId)
        {
            return HandleResult(await Mediator.Send(new GetCategoriesByStreetcodeIdQuery(streetcodeId)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSourceLink([FromBody] SourceLinkCategoryDto sourceLinkCategoryDto)
        {
            return HandleResult(await Mediator.Send(new CreateSourceLinkCategoryCommand(sourceLinkCategoryDto)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSourceLink([FromBody] SourceLinkCategoryDto sourceLinkCategoryDto)
        {
            return HandleResult(await Mediator.Send(new UpdateSourceLinkCategoryCommand(sourceLinkCategoryDto)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSourceLink([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteSourceLinkCategoryCommand(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryContent([FromBody] StreetcodeCategoryContentDto streetcodeCategoryContentDto)
        {
            return HandleResult(await Mediator.Send(new CreateStreetcodeCategoryContentCommand(streetcodeCategoryContentDto)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoryContent([FromBody] StreetcodeCategoryContentDto streetcodeCategoryContentDto)
        {
            return HandleResult(await Mediator.Send(new UpdateStreetcodeCategoryContentCommand(streetcodeCategoryContentDto)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryContent([FromRoute] int streetcodeId, [FromRoute] int sourceLinkCategoryId)
        {
            return HandleResult(await Mediator.Send(new DeleteStreetcodeCategoryContentCommand(sourceLinkCategoryId, streetcodeId)));
        }
    }
}
