using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Application.LanguageTools.Queries;
using Application.LanguageTools.Commands;

namespace API.Controllers
{
    [Authorize]
    public class LanguageToolsController : ApiController
    {
        private readonly IConfiguration _config;

        public LanguageToolsController(IConfiguration config)
        {
            _config = config;
        }
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var output = await Mediator.Send(new GetLanguageToolsQuery());
            return Ok(output);
        }

        [HttpGet("SelectList")]
        public async Task<IActionResult> SelectList()
        {
            return Ok(await Mediator.Send(new SelectListLangToolsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DetailsLangToolQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateLanguageToolCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateLanguageToolCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteLanguageToolCommand { Id = id });
        }
    }
}
