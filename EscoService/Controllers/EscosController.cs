using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace EscoService.Controllers
{
    public class EscosController(IEscoRepo repo) : ODataController
    {
        [EnableQuery(PageSize = 3)]
        public IQueryable<Esco> Get()
        {
            return repo.GetAll();
        }

        [EnableQuery]
        public SingleResult<Esco> Get([FromODataUri] int key)
        {
            return SingleResult.Create(repo.GetById(key));
        }

        public IActionResult Post([FromBody] Esco esco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            repo.Create(esco);

            return Created("companies", esco);
        }

        public IActionResult Put([FromODataUri] int key, [FromBody] Esco esco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != esco.ID)
            {
                return BadRequest();
            }

            repo.Update(esco);

            return NoContent();
        }

        public IActionResult Delete([FromODataUri] int key)
        {
            var esco = repo.GetById(key);
            if (esco is null)
            {
                return BadRequest();
            }

            repo.Delete(esco.First());

            return NoContent();
        }

    }
}
