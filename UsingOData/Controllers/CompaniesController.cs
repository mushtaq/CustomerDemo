using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace UsingOData.Controllers
{
    public class CompaniesController(ICompanyRepo repo) : ODataController
    {
        [EnableQuery(PageSize = 3)]
        public IQueryable<Company> Get()
        {
            return repo.GetAll();
        }

        [EnableQuery]
        public SingleResult<Company> Get([FromODataUri] int key)
        {
            return SingleResult.Create(repo.GetById(key));
        }

        public IActionResult Post([FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            repo.Create(company);

            return Created("companies", company);
        }

        public IActionResult Put([FromODataUri] int key, [FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != company.ID)
            {
                return BadRequest();
            }

            repo.Update(company);

            return NoContent();
        }

        public IActionResult Delete([FromODataUri] int key)
        {
            var company = repo.GetById(key);
            if (company is null)
            {
                return BadRequest();
            }

            repo.Delete(company.First());

            return NoContent();
        }

    }
}
