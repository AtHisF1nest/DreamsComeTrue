using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DreamsComeTrueAPI.Dtos;
using DreamsComeTrueAPI.Models;
using DreamsComeTrueAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        public TodosController(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        [HttpGet("GetItems")]
        public async Task<IEnumerable<TodoItemDto>> GetItems()
        {
            var todoItems = await _todoRepository.GetTodoItems();

            var res = _mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

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
