using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodosController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }




        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody]TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]TodoItem item)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
