using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using BusinessLogic.Data;
using System.Net.WebSockets;

namespace BusinessLogic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateUser (UsersInformation Information)
        {
            if(Information.Id == 0)
            {
                _context.Users.Add(Information);
            } else
            {
                var UserInDb = _context.Users.Find(Information.Id);

                if (UserInDb == null)
                    return new JsonResult(NotFound());

                UserInDb = Information;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(Information));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Users.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Users.ToList();

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Users.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Users.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
