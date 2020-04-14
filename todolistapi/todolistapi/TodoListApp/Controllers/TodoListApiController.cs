using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Models;

namespace TodoListApp.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class TodoListApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoListApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TodoListApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoListTbl>>> GetTodoLists()
        {
            return await _context.TodoLists.ToListAsync();
        }

        // GET: api/TodoListApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListTbl>> GetTodoListTbl(int id)
        {
            var todoListTbl = await _context.TodoLists.FindAsync(id);

            if (todoListTbl == null)
            {
                return NotFound();
            }

            return todoListTbl;
        }

        // PUT: api/TodoListApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoListTbl(int id, TodoListTbl todoListTbl)
        {
            if (id != todoListTbl.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoListTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoListTblExists(id))
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

        // POST: api/TodoListApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TodoListTbl>> PostTodoListTbl(TodoListTbl todoListTbl)
        {

            todoListTbl.TaskDate = DateTime.Now;
            _context.TodoLists.Add(todoListTbl);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoListTbl", new { id = todoListTbl.Id }, todoListTbl);
        }

        // DELETE: api/TodoListApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoListTbl>> DeleteTodoListTbl(int id)
        {
            var todoListTbl = await _context.TodoLists.FindAsync(id);
            if (todoListTbl == null)
            {
                return NotFound();
            }

            _context.TodoLists.Remove(todoListTbl);
            await _context.SaveChangesAsync();

            return todoListTbl;
        }

        private bool TodoListTblExists(int id)
        {
            return _context.TodoLists.Any(e => e.Id == id);
        }
    }
}
