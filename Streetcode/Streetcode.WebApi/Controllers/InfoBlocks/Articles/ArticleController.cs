using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Create;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Delete;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Update;

namespace Streetcode.WebApi.Controllers.InfoBlocks.Articles
{
    public class ArticleController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArticleDto article)
        {
            return HandleResult(await Mediator.Send(new CreateArticleCommand(article)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteArticleCommand(id)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetArticleByIdQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllArticlesQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ArticleDto article)
        {
            return HandleResult(await Mediator.Send(new UpdateArticleCommand(article)));
        }
    }
}
