using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Streetcode;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetAll;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetById;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetByTransliterationUrl;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetAllShort;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetAllCatalog;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetCount;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetByFilter;
using Streetcode.BLL.Dto.AdditionalContent.Filter;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetShortById;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.GetAllStreetcodesMainPage;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;

namespace Streetcode.WebApi.Controllers.Streetcode;

/// <summary>
/// Represents the controller for managing streetcodes.
/// </summary>
public class StreetcodeController : BaseApiController
{
    /// <summary>
    /// Retrieves all streetcodes.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <returns>The list of streetcodes.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllStreetcodesRequestDto request)
    {
        return HandleResult(await Mediator.Send(new GetAllStreetcodesQuery(request)));
    }

    /// <summary>
    /// Retrieves all streetcodes in a short format.
    /// </summary>
    /// <returns>The list of streetcodes in a short format.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllShort()
    {
        return HandleResult(await Mediator.Send(new GetAllStreetcodesShortQuery()));
    }

    /// <summary>
    /// Retrieves all streetcodes for the main page.
    /// </summary>
    /// <returns>The list of streetcodes for the main page.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllMainPage()
    {
        return HandleResult(await Mediator.Send(new GetAllStreetcodesMainPageQuery()));
    }

    /// <summary>
    /// Retrieves a streetcode in a short format by its ID.
    /// </summary>
    /// <param name="id">The ID of the streetcode.</param>
    /// <returns>The streetcode in a short format.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetShortById(int id)
    {
        return HandleResult(await Mediator.Send(new GetStreetcodeShortByIdQuery(id)));
    }

    /// <summary>
    /// Retrieves streetcodes based on the specified filter.
    /// </summary>
    /// <param name="request">The filter parameters.</param>
    /// <returns>The list of streetcodes that match the filter.</returns>
    [HttpGet]
    public async Task<IActionResult> GetByFilter([FromQuery] StreetcodeFilterRequestDto request)
    {
        return HandleResult(await Mediator.Send(new GetStreetcodeByFilterQuery(request)));
    }

    /// <summary>
    /// Retrieves a page of streetcodes for catalog view.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="count">The number of streetcodes per page.</param>
    /// <returns>The page of streetcodes for catalog view.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllCatalog([FromQuery] int page, [FromQuery] int count)
    {
        return HandleResult(await Mediator.Send(new GetAllStreetcodesCatalogQuery(page, count)));
    }

    /// <summary>
    /// Retrieves the total count of streetcodes.
    /// </summary>
    /// <returns>The total count of streetcodes.</returns>
    [HttpGet]
    public async Task<IActionResult> GetCount()
    {
        return HandleResult(await Mediator.Send(new GetStreetcodesCountQuery()));
    }

    /// <summary>
    /// Retrieves a streetcode by its transliteration URL.
    /// </summary>
    /// <param name="url">The transliteration URL of the streetcode.</param>
    /// <returns>The streetcode.</returns>
    [HttpGet("{url}")]
    public async Task<IActionResult> GetByTransliterationUrl([FromRoute] string url)
    {
        return HandleResult(await Mediator.Send(new GetStreetcodeByTransliterationUrlQuery(url)));
    }

    /// <summary>
    /// Retrieves a streetcode by its ID.
    /// </summary>
    /// <param name="id">The ID of the streetcode.</param>
    /// <returns>The streetcode.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetStreetcodeByIdQuery(id)));
    }

    /// <summary>
    /// Creates a streetcode.
    /// </summary>
    /// <param name="streetcodeDto">Thedto of the streetcode to add.</param>
    /// <returns>The created streetcode.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BaseStreetcodeDto streetcodeDto)
    {
        return HandleResult(await Mediator.Send(new CreateStreetcodeCommand(streetcodeDto)));
    }
}
