using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IMongoCollection<ToDoItem> _todosCollection;

        public TodosController(MongoDbContext context)
        {
            _todosCollection = context.Todos;
        }

        // GET /todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllTodos()
        {
            var todos = await _todosCollection.Find(_ => true).ToListAsync();
            return Ok(todos);
        }

        // GET /todos/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ToDoItem>> GetTodoById(string id)
        {

            // Ensure the ID is a valid ObjectId
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            // Query MongoDB using ObjectId
            var filter = Builders<ToDoItem>.Filter.Eq("_id", objectId);
            
            // var todo = await _todosCollection.Find(filter).FirstOrDefaultAsync();
            var todo = await _todosCollection.Find(t => t.Id == id).FirstOrDefaultAsync();

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST /todos
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateTodo([FromBody] ToDoItem item)
        {
            try 
            {
                if (item == null || string.IsNullOrWhiteSpace(item.Title))
                {
                    return BadRequest("Invalid to-do item.");
                }

                // Set default values if necessary
                item.Status = item.Status ?? "Pending";
                item.Text = item.Text ?? "";

                // Insert the new item into MongoDB
                await _todosCollection.InsertOneAsync(item);
                
                return CreatedAtAction(nameof(GetTodoById), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT /todos/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateTodoById(string id, [FromBody] ToDoItem item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Title))
            {
                return BadRequest("Invalid to-do item.");
            }

            // Ensure the ID is a valid ObjectId
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            // Set the Id of the item to the provided id
            item.Id = id;

            // Query MongoDB using ObjectId
            var filter = Builders<ToDoItem>.Filter.Eq("_id", objectId);
            var result = await _todosCollection.ReplaceOneAsync(filter, item);
            // var result = await _todosCollection.ReplaceOneAsync(t => t.Id == id, item);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE /todos/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteTodoById(string id)
        {
            // Ensure the ID is a valid ObjectId
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            // Query MongoDB using ObjectId
            var filter = Builders<ToDoItem>.Filter.Eq("_id", objectId);
            var result = await _todosCollection.DeleteOneAsync(filter);
            // var result = await _todosCollection.DeleteOneAsync(t => t.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}