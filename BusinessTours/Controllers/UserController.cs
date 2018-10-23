using System;
using System.Threading.Tasks;
using BusinessTours.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessTours.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        UserService service = new UserService();

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> logeo(string ptfm, string ident, string pswd)
        {
            try
            {
                return Ok(await service.logeo(ptfm,ident,pswd));
            }
            catch (Exception ex)
            { 
                return NotFound(ex.Message);
            }
        }
    }
}
