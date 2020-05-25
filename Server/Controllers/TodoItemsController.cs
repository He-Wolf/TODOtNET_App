using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TodoApi.Server.Data;
using TodoApi.Server.Models;
using TodoApi.Shared.Models;
using AutoMapper;

namespace web_api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly UserManager<WebApiUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TodoItemsController(
            UserManager<WebApiUser> userManager,
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<TodoItemsController> logger)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodoItems()
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CurrentUser = await _userManager.Users
                                                .Include(u => u.TodoItems)
                                                .SingleAsync(u => u.Id == CurrentUserId);
            //_logger.LogInformation("Current user: {currentUserId}", CurrentUserId);
            //_logger.LogInformation("Current user: {currentUser.id}", CurrentUser.Id);
            
            var todoItems = CurrentUser.TodoItems.ToList();
            if(todoItems == null)
            {
                return NotFound(new MessageViewModel("ToDo items not found.", DateTime.Now));
            }
            return Ok(_mapper.Map<List<TodoItem>, List<TodoViewModel>>(todoItems));

            /*var CurrentUser = await _userManager.FindByIdAsync(CurrentUserId);
            return await _context.Entry(CurrentUser)
                                 .Collection(u => u.TodoItems)
                                 .Query()
                                 .ToListAsync();*/
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodoItem([FromRoute] long id)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var CurrentUser = await _userManager.Users
                                                .Include(u => u.TodoItems)
                                                .SingleAsync(u => u.Id == CurrentUserId);

            var todoItem = CurrentUser.TodoItems.Single(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound(new MessageViewModel("ToDo item not found.", DateTime.Now));
            }

            return Ok(_mapper.Map<TodoViewModel>(todoItem));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TodoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTodoItem([FromRoute] long id, [FromBody] TodoViewModel todoItemVM)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var CurrentUser = await _userManager.Users
                                                .Include(u => u.TodoItems)
                                                .SingleAsync(u => u.Id == CurrentUserId);

            if (id != todoItemVM.Id)
            {
                return BadRequest(new MessageViewModel("ToDo item not valid.", DateTime.Now));;
            }

            TodoItem todoItem = CurrentUser.TodoItems.Single(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound(new MessageViewModel("ToDo item not found.", DateTime.Now));;
            }
            
            todoItem.Name = todoItemVM.Name;
            todoItem.IsComplete = todoItemVM.IsComplete;

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<TodoViewModel>(todoItem));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoViewModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostTodoItem([FromBody] TodoViewModel todoItemVM)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //with Identity usermanager
            var CurrentUser = await _userManager.Users
                                                .Include(u => u.TodoItems)
                                                .SingleAsync(u => u.Id == CurrentUserId);
            
            TodoItem todoItem = _mapper.Map<TodoItem>(todoItemVM);

            CurrentUser.TodoItems.Add(todoItem);
            await _userManager.UpdateAsync(CurrentUser);
            
            //with Entity context
            /*var CurrentUser = await _context.Users
                                              .Include(u => u.TodoItems)
                                              .SingleAsync(u => u.Id == CurrentUserId);

            CurrentUser.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();*/

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, _mapper.Map<TodoViewModel>(todoItem));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem([FromRoute] long id)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var CurrentUser = await _userManager.Users
                                                .Include(u => u.TodoItems)
                                                .SingleAsync(u => u.Id == CurrentUserId);

            var todoItem = CurrentUser.TodoItems.Single(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound(new MessageViewModel("ToDo item not found.", DateTime.Now));
            }

            CurrentUser.TodoItems.Remove(todoItem);
            await _userManager.UpdateAsync(CurrentUser);

            return Ok(new MessageViewModel("ToDo item deleted successfully.", DateTime.Now));
        }
    }
}