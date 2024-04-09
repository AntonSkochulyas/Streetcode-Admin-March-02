using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.MediatR.Media.Image.Create;

namespace Streetcode.WebApi.Controllers.Media.Images;

public class ImageMainController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ImageFileBaseCreateDto image)
    {
        return HandleResult(await Mediator.Send(new CreateImageCommand(image)));
    }
}
