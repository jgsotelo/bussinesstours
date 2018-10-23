using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessTours.Model.Entity;
using BusinessTours.Service;
using Microsoft.AspNetCore.Mvc;

namespace BusinessTours.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        CompanyService service = new CompanyService();

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<Company>>> List()
        {
            try
            {
                return Ok(await service.List());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Account>> Cuenta(int id)
        {
            try
            {
                return Ok(await service.GetCuenta(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Collaborator>> Colaboradores(int id)
        {
            try
            {
                return Ok(await service.GetColaboradores(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> Registrar([FromBody] Company obj, int id)
        {
            try
            {
                return Ok(await service.Registrar(obj, id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> Update([FromBody] Company obj, int id)
        {
            try
            {
                return Ok(await service.Update(obj, id));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> ChangeState(int id)
        {
            try
            {
                return Ok(await service.ChangeState(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
