using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DreamsComeTrueAPI.Models;
using DreamsComeTrueAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DreamsComeTrueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoItemsRepository _todoItemsRepository;
        public TodosController(ITodoItemsRepository todoItemsRepository)
        {
            _todoItemsRepository = todoItemsRepository;

        }

        [HttpGet("GetItems")]
        public async Task<IEnumerable<TodoItem>> GetItems()
        {
            return await _todoItemsRepository.GetTodoItems();
        }

    }
}
