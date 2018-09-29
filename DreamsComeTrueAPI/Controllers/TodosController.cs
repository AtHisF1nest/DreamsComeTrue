using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DreamsComeTrueAPI.Dtos;
using DreamsComeTrueAPI.Models;
using DreamsComeTrueAPI.Models.Enums;
using DreamsComeTrueAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamsComeTrueAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private string _actualUserLogin;
        private readonly IDCTRepository _dCTRepository;

        public TodosController(ITodoRepository todoRepository, IMapper mapper, IDCTRepository dCTRepository)
        {
            _dCTRepository = dCTRepository;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItemDto>> GetItems()
        {
            var todoItems = await _todoRepository.GetTodoItems();

            var res = _mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

            return res;
        }

        [HttpPost("GetTodosByCategories")]
        public async Task<IEnumerable<TodoItemDto>> GetTodosByCategories(CategoryDto[] categories)
        {
            var todoItems = await _todoRepository.GetTodoItems(CategoryType.NaDzis, categories.Select(x => x.Id).ToList());

            var res = _mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

            return res;
        }

        [HttpGet("{id}")]
        public async Task<TodoItemDto> GetItem(int id)
        {
            _actualUserLogin = HttpContext.User.Identity.Name;

            var todoItem = await _todoRepository.GetTodoItem(id);

            if(todoItem.UsersPair.User?.Login != _actualUserLogin && todoItem.UsersPair.User2?.Login != _actualUserLogin)
                return null;

            var res = _mapper.Map<TodoItemDto>(todoItem);

            return res;
        }

        [HttpGet("GetCategories")]
        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _todoRepository.GetCategories();

            var res = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            
            foreach (var item in res)
                item.CountOfItems = (await _todoRepository.GetConnectedTodoItems(item.Id)).Count();

            return res;
        }

        [HttpGet("GetDreams")]
        public async Task<IEnumerable<TodoItemDto>> GetDreams()
        {
            var todoItems = await _todoRepository.GetTodoItems(CategoryType.Marzenia);

            var res = _mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

            return res;
        }

        [HttpGet("GetDream/{id}")]
        public async Task<TodoItemDto> GetDream(int id)
        {
            _actualUserLogin = HttpContext.User.Identity.Name;

            var todoItem = await _todoRepository.GetTodoItem(id, CategoryType.Marzenia);

            if(todoItem.UsersPair.User?.Login != _actualUserLogin && todoItem.UsersPair.User2?.Login != _actualUserLogin)
                return null;

            var res = _mapper.Map<TodoItemDto>(todoItem);

            return res;
        }

        [HttpGet("GetDreamsCategories")]
        public async Task<IEnumerable<CategoryDto>> GetDreamsCategories()
        {
            var categories = await _todoRepository.GetCategories(CategoryType.Marzenia);

            var res = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return res;
        }

        [HttpGet("GetTodosConnections/{id}")]
        public async Task<IEnumerable<TodoItemDto>> GetTodosConnections(int id)
        {
            var todos = await _todoRepository.GetConnectedTodoItems(id);

            var res = _mapper.Map<IEnumerable<TodoItemDto>>(todos);

            return res;
        }

        [HttpGet("GetTodosDreamsConnections/{id}")]
        public async Task<IEnumerable<TodoItemDto>> GetTodosDreamsConnections(int id)
        {
            var dreams = await _todoRepository.GetConnectedTodoItems(id, CategoryType.Marzenia);

            var res = _mapper.Map<IEnumerable<TodoItemDto>>(dreams);

            return res;
        }

        [HttpGet("GetNotConnectedItems/{id}")]
        public async Task<IEnumerable<TodoItemDto>> GetNotConnectedItems(int id)
        {
            var todos = await _todoRepository.GetNotConnectedTodoItems(id);

            var res = _mapper.Map<IEnumerable<TodoItemDto>>(todos);

            return res;
        }

        [HttpGet("GetNotConnectedDreams/{id}")]
        public async Task<IEnumerable<TodoItemDto>> GetNotConnectedDreams(int id)
        {
            var dreams = await _todoRepository.GetNotConnectedTodoItems(id, CategoryType.Marzenia);

            var res = _mapper.Map<IEnumerable<TodoItemDto>>(dreams);

            return res;
        }

        [HttpGet("GetHistoryOfTodo/{id}")]
        public async Task<IEnumerable<HistoryDto>> GetHistoryOfTodo(int id)
        {
            var histories = await _todoRepository.GetHistoryOfTodo(id);

            var res = _mapper.Map<IEnumerable<HistoryDto>>(histories);

            return res;
        }

    }
}
