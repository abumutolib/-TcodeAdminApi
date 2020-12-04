using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Tutorials.Queries;
using Application.Tutorials.Commands;

namespace API.Controllers
{
    [Authorize]
    public class TutorialsController : ApiController
    {
        /// <summary>
        /// Get all tutorials
        /// </summary>
        /// <returns>All tutorials</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var output = await Mediator.Send(new GetTutorialsQuery());
            return Ok(output);
        }

        /// <summary>
        /// Get single tutorial
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single tutroial by DetailTutorialVm model</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var output = await Mediator.Send(new DetailTutorialQuery { Id = id });
            return Ok(output);
        }

        /// <summary>
        /// Create new tutorial from admin page
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id created tutrial</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateTutorialCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Edit Tutorial
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>Id updated tutorial</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]UpdateTutorialCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete Tutorial
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete([FromRoute]int id)
        {
            await Mediator.Send(new DeleteTutorialCommand { Id = id });
        }
    }
}
