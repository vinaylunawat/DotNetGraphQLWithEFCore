using Framework.Service.Utilities.Criteria;
using Geography.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Geography.Serverless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public ValuesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            FilterCriteria<Entity.Entities.Country> filterCriteria = new FilterCriteria<Entity.Entities.Country>();
            filterCriteria.Includes.Add(item => item.States);
            var countries = await _countryRepository.FetchByCriteriaAsync(filterCriteria).ConfigureAwait(false);

            return new string[] { "value1", "value21" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
