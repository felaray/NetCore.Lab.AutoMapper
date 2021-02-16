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
    public class TodoController : ControllerBase
    {
        private readonly NetCoreLabAutoMapperContext _context;
        private IMapper _mapper;

        public TodoController(NetCoreLabAutoMapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Todoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            return await _context.Todo.ToListAsync();
        }

        // GET: api/Todoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            var todo = await _context.Todo.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/Todoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(Guid id, TodoViewModel todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            var data = _mapper.Map<Todo>(todo);
            _context.Entry(data).State = EntityState.Modified;

            try
            {
                _context.Todo.Persist(_mapper).InsertOrUpdate(todo);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// update list
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutTodoList(List<UpdateTodoViewModel.Request> todo)
        {

            var data = _mapper.Map<List<Todo>>(todo);
            //_context.Entry(data).State = EntityState.Modified;

            try
            {
                foreach (var item in todo)
                {
                    _context.Todo.Persist(_mapper).InsertOrUpdate(item);
                }


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Todoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(TodoViewModel todo)
        {

            var data = _mapper.Map<Todo>(todo);

            _context.Todo.Persist(_mapper).InsertOrUpdate(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        // DELETE: api/Todoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(Guid id)
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        private bool TodoExists(Guid id)
        {
            return _context.Todo.Any(e => e.Id == id);
        }
    }
}
