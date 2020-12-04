using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Technologies.Queries;
using Application.Technologies.Commands;

namespace API.Controllers
{
    [Authorize]
    public class TechnologiesController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var output = await Mediator.Send(new GetTechologiesQuery());
            return Ok(output);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            return Ok(await Mediator.Send(new DetailsTechQuery { Id = id }));
        }

        [HttpGet("SelectList")]
        public async Task<IActionResult> SelectList()
        {
            return Ok(await Mediator.Send(new SelectListTechQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateTechnologyCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateTechnologyCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await Mediator.Send(new DeleteTechnologyCommand { Id = id });
        }
    }
}
