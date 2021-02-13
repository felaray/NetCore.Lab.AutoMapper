using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore.Lab.AutoMapper.Data;
using NetCore.Lab.AutoMapper.Models;
using NetCore.Lab.AutoMapper.ViewModels;

namespace NetCore.Lab.AutoMapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NetCoreLabAutoMapperContext _context;
        private IMapper _mapper;


        public UsersController(NetCoreLabAutoMapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            return await _context.User.Include(c => c.Todos).ToListAsync();
        }


        //TODO:希望更新的時候也可以找出要刪除的目標
        [HttpPost]
        public async Task<IActionResult> PostUser(UserViewModel user)
        {

            var result = _context.User.Persist(_mapper).InsertOrUpdate(user);

            var trace = _context.ChangeTracker.Entries();
            foreach (var item in trace)
            {

                switch (item.State)
                {
                    case EntityState.Modified:
                        break;
                    case EntityState.Added:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        break;
                }
            }


            await _context.SaveChangesAsync();

            return Ok(result);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
