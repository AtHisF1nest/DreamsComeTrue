using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DreamsComeTrueAPI.Dtos;
using DreamsComeTrueAPI.Models;
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

            return res;
        }



    }
}
