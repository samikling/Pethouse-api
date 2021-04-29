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
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public bool PostStatus(LoginModel input)
        {
            try
            {
            var userName = input.userName;
            var passWord = input.passWord;
            pethouseContext context = new pethouseContext();
                Users user = (from u in context.Users
                where (u.Username == userName) && (u.Password == passWord)
                select u).FirstOrDefault();
                
                if (user == null)
                {
                    context.Dispose();
                    return false;
                }

                context.Dispose();
                return true;
            }
            catch (System.Exception)
            {
                
                throw;
            }

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
