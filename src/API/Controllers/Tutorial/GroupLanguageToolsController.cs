using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.GroupLanguageTools.Queries;
using Application.GroupLanguageTools.Commands;

namespace API.Controllers
{
    [Authorize]
    [Route("api/LanguageTools/Groups")]
    public class GroupLanguageToolsController : ApiController
    {
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var output = await Mediator.Send(new GroupLangToolQuery());
            return Ok(output);
        }

        [HttpGet("SelectList")]
        public async Task<IActionResult> SelectList()
        {
            return Ok(await Mediator.Send(new SelectListGLTQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var output = await Mediator.Send(new DetailsGLTQuery { Id = id });
            return Ok(output);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGLTCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateGLTCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await Mediator.Send(new DeleteGLTCommand { Id = id });
        }
    }
}
