using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.GroupTutorials.Queries;
using Application.GroupTutorials.Commands;

namespace API.Controllers
{
    [Authorize]
    [Route("api/Tutorials/Groups")]
    public class GroupTutorialsController : ApiController
    {
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var output = await Mediator.Send(new GetGroupTutorialQuery());
            return Ok(output);
        }

        [HttpGet("SelectList")]
        public async Task<IActionResult> SelectList()
        {
            return Ok(await Mediator.Send(new SelectListGroupTutorialQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var output = await Mediator.Send(new DetailsGroupTutorialQuery { Id = id });
            return Ok(output);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGroupTutorialCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateGroupTutorialCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteGroupTutorialCommand { Id = id });
        }
    }
}
