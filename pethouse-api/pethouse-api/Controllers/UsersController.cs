using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pethouse_api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pethouse_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "nothing", "here" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Nothing here";
        }


        [HttpPost]
        public Users PostStatus(LoginModel input)
        {
            try
            {
                var userName = input.userName;
                var passWord = input.passWord;
                pethouseContext context = new pethouseContext();
                var user = (from u in context.Users
                              where (u.Username == userName) && (u.Password == passWord)
                              select u).FirstOrDefault();

                if (user == null)
                {
                    context.Dispose();
                    return null;
                }

                context.Dispose();
                return user;
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
