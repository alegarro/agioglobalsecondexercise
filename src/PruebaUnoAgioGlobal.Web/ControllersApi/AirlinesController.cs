using Ardalis.Filters;
using Microsoft.AspNetCore.Mvc;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.Exceptions.Entities;
using PruebaUnoAgioGlobal.Web.ModelsApi.Airlines.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ControllersApi
{
    /// <summary>
    /// Airlines Controller.
    /// </summary>
    [Route("api/airlines")]
    [ValidateModel]
    public class AirlinesController : Controller
    {
        private readonly IAirlinesService _airlinesService;

        /// <summary>
        /// Generates a new instance of the airlines controller.
        /// </summary>
        /// <param name="airlinesService">Airline Application Service.</param>
        public AirlinesController(IAirlinesService airlinesService)
        {
            _airlinesService = airlinesService;
        }

        // GET: api/airlines
        /// <summary>
        /// Method that gets the airlines list.
        /// </summary>
        /// <returns>Airlines list.</returns>        
        [HttpGet]
        [SwaggerOperation("Gets the airlines stored in the system.")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<AirlineDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(InternalServerExceptionInfo))]
        public async Task<IActionResult> GetAll()
        {
            var airlinesListDtos = await _airlinesService.GetAll();

            return new OkObjectResult(airlinesListDtos);
        }
    }
}
