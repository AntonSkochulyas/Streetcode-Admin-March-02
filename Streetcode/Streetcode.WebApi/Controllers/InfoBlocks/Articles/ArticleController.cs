using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Create;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Delete;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Update;

namespace Streetcode.WebApi.Controllers.InfoBlocks.Articles
{
    /// <summary>
    /// Controller for managing articles.
    /// </summary>
    public class ArticleController : BaseApiController
    {
        /// <summary>
        /// Creates a new article.
        /// </summary>
        /// <param name="article">The article data.</param>
        /// <returns>The created article.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArticleCreateDto article)
        {
            return HandleResult(await Mediator.Send(new CreateArticleCommand(article)));
        }

        /// <summary>
        /// Deletes an article by ID.
        /// </summary>
        /// <param name="id">The ID of the article to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [Authorize("Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteArticleCommand(id)));
        }

        /// <summary>
        /// Gets an article by ID.
        /// </summary>
        /// <param name="id">The ID of the article to retrieve.</param>
        /// <returns>The retrieved article.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetArticleByIdQuery(id)));
        }

        /// <summary>
        /// Gets all articles.
        /// </summary>
        /// <returns>The list of all articles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllArticlesQuery()));
        }

        /// <summary>
        /// Updates an existing article.
        /// </summary>
        /// <param name="article">The updated article data.</param>
        /// <returns>The updated article.</returns>
        [Authorize("Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ArticleDto article)
        {
            return HandleResult(await Mediator.Send(new UpdateArticleCommand(article)));
        }
    }
}
