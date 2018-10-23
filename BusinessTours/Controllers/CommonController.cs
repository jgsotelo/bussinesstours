using System;
using System.Threading.Tasks;
using BusinessTours.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessTours.Controllers
{
    [Route("[controller]/[action]")]
    public class CommonController : Controller
    {
        CommonService service = new CommonService();

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> constant(string group)
        {
            try
            {
                return Ok(await service.constant(group));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> departament()
        {
            try
            {
                return Ok(await service.departament());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> province(string depart)
        {
            try
            {
                return Ok(await service.province(depart));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> district(string provincia)
        {
            try
            {
                return Ok(await service.district(provincia));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
