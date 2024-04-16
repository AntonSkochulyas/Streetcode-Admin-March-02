using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoryById;
using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoriesByStreetcodeId;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetCategoryContentByStreetcodeId;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update;
using Microsoft.AspNetCore.Authorization;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Delete;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create;

namespace Streetcode.WebApi.Controllers.Source
{
    /// <summary>
    /// Controller for managing sources and categories.
    /// </summary>
    public class SourcesController : BaseApiController
    {
        /// <summary>
        /// Get all category names.
        /// </summary>
        /// <returns>The list of category names.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllNames()
        {
            return HandleResult(await Mediator.Send(new GetAllCategoryNamesQuery()));
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>The list of categories.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return HandleResult(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The category with the specified ID.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetCategoryByIdQuery(id)));
        }

        /// <summary>
        /// Get the content of a category by streetcode ID and category ID.
        /// </summary>
        /// <param name="streetcodeId">The ID of the streetcode.</param>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>The content of the category.</returns>
        [HttpGet("{categoryId:int}&{streetcodeId:int}")]
        public async Task<IActionResult> GetCategoryContentByStreetcodeId([FromRoute] int streetcodeId, [FromRoute] int categoryId)
        {
            return HandleResult(await Mediator.Send(new GetCategoryContentByStreetcodeIdQuery(streetcodeId, categoryId)));
        }

        /// <summary>
        /// Get all categories by streetcode ID.
        /// </summary>
        /// <param name="streetcodeId">The ID of the streetcode.</param>
        /// <returns>The list of categories.</returns>
        [HttpGet("{streetcodeId:int}")]
        public async Task<IActionResult> GetCategoriesByStreetcodeId([FromRoute] int streetcodeId)
        {
            return HandleResult(await Mediator.Send(new GetCategoriesByStreetcodeIdQuery(streetcodeId)));
        }

        /// <summary>
        /// Create a new source link category.
        /// </summary>
        /// <param name="sourceLinkCategoryContentDto">The DTO containing the source link category data.</param>
        /// <returns>The created source link category.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSourceLink([FromBody] CreateSourceLinkCategoryContentDto sourceLinkCategoryContentDto)
        {
            return HandleResult(await Mediator.Send(new CreateSourceLinkCategoryCommand(sourceLinkCategoryContentDto)));
        }

        /// <summary>
        /// Create a new streetcode category content.
        /// </summary>
        /// <param name="streetcodeCategoryContentDto">The DTO containing the streetcode category content data.</param>
        /// <returns>The created streetcode category content.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateStreetcodeCategoryContent([FromBody] StreetcodeCategoryContentDto streetcodeCategoryContentDto)
        {
            return HandleResult(await Mediator.Send(new CreateStreetcodeCategoryContentCommand(streetcodeCategoryContentDto)));
        }

        /// <summary>
        /// Update an existing source link category.
        /// </summary>
        /// <param name="sourceLinkCategoryContentDto">The DTO containing the updated source link category data.</param>
        /// <returns>The updated source link category.</returns>
        [Authorize("Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateSourceLink([FromBody] SourceLinkCategoryDto sourceLinkCategoryContentDto)
        {
            return HandleResult(await Mediator.Send(new UpdateSourceLinkCategoryCommand(sourceLinkCategoryContentDto)));
        }

        /// <summary>
        /// Update an existing streetcode category content.
        /// </summary>
        /// <param name="streetcodeCategoryContentDto">The DTO containing the updated streetcode category content data.</param>
        /// <returns>The updated streetcode category content.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateStreetcodeCategoryContent([FromBody] StreetcodeCategoryContentDto streetcodeCategoryContentDto)
        {
            return HandleResult(await Mediator.Send(new UpdateStreetcodeCategoryContentCommand(streetcodeCategoryContentDto)));
        }

        /// <summary>
        /// Delete a source link category by its ID.
        /// </summary>
        /// <param name="id">The ID of the source link category to delete.</param>
        /// <returns>The result of the deletion operation.</returns>
        [Authorize("Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSourceLink([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteSourceLinkCategoryCommand(id)));
        }

        /// <summary>
        /// Delete a streetcode category content by IDs.
        /// </summary>
        /// <param name="sourceLinkId">The ID of the source link category to delete.</param>
        /// /// <param name="streetcodeId">The ID of the streetcode to delete.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStreetcodeCategoryContent([FromRoute] int sourceLinkId, int streetcodeId)
        {
            return HandleResult(await Mediator.Send(new DeleteStreetcodeCategoryContentCommand(sourceLinkId, streetcodeId)));
        }
    }
}
