using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private string _actualUserLogin;
        private readonly IDCTRepository _dCTRepository;

        public ManagementController(ITodoRepository todoRepository, IMapper mapper, IDCTRepository dCTRepository)
        {
            _dCTRepository = dCTRepository;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ManagementType>> GetManagementTypes()
        {
            return await _dCTRepository.GetManagementTypes();
        }

        [HttpPost("AddTodo")]
        public async Task<IActionResult> AddTodo(TodoItemDto todoItemDto)
        {
            var item = new TodoItem 
            {
                Objective = todoItemDto.Objective,
                Cost = todoItemDto.Cost
            };

            if(await _todoRepository.AddTodoItem(item) == null)
                return BadRequest();

            return StatusCode(201);
        }

        [HttpDelete("DeleteTodo/{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            if(!await _todoRepository.DeleteTodoItem(id))
                return BadRequest();

            return Ok();
        }

        [HttpPost("AddDream")]
        public async Task<IActionResult> AddDream(TodoItemDto todoItemDto)
        {
            var item = new TodoItem 
            {
                Objective = todoItemDto.Objective,
                Cost = todoItemDto.Cost
            };

            if(await _todoRepository.AddTodoItem(item, CategoryType.Marzenia) == null)
                return BadRequest();

            return StatusCode(201);
        }

        [HttpDelete("DeleteDream/{id}")]
        public async Task<IActionResult> DeleteDream(int id)
        {
            if(!await _todoRepository.DeleteTodoItem(id))
                return BadRequest();

            return Ok();
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            var item = new Category 
            {
                Color = categoryDto.Color,
                BackgroundColor = categoryDto.BackgroundColor,
                Name = categoryDto.Name
            };

            if(await _todoRepository.AddCategory(item) == null)
                return BadRequest();

            return StatusCode(201);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if(!await _todoRepository.DeleteCategory(id))
                return BadRequest();

            return Ok();
        }

        [HttpPost("AddDreamCategory")]
        public async Task<IActionResult> AddDreamCategory(CategoryDto categoryDto)
        {
            var item = new Category 
            {
                Color = categoryDto.Color,
                BackgroundColor = categoryDto.BackgroundColor,
                Name = categoryDto.Name
            };

            if(await _todoRepository.AddCategory(item, CategoryType.Marzenia) == null)
                return BadRequest();

            return StatusCode(201);
        }

        [HttpDelete("DeleteDreamCategory/{id}")]
        public async Task<IActionResult> DeleteDreamCategory(int id)
        {
            if(!await _todoRepository.DeleteCategory(id))
                return BadRequest();

            return Ok();
        }

        [HttpPost("ConnectItems")]
        public async Task<IActionResult> ConnectItems(TodoConnectionDto todoConnectionDto)
        {
            if(await _todoRepository.ConnectItems(todoConnectionDto.CategoryId, todoConnectionDto.ItemId))
                return Ok();

            return BadRequest();
        }

        [HttpPost("UnConnectItems")]
        public async Task<IActionResult> UnConnectItems(TodoConnectionDto todoConnectionDto)
        {
            if(await _todoRepository.UnConnectItems(todoConnectionDto.CategoryId, todoConnectionDto.ItemId))
                return Ok();

            return BadRequest();
        }

        [HttpPost("RealizeTodo")]
        public async Task<IActionResult> RealizeTodo(TodoItemDto todoItemDto)
        {
            if(await _todoRepository.RealizeTodo(todoItemDto.Id, todoItemDto.LastDone))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("DeleteHistory/{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            if(!await _todoRepository.DeleteHistory(id))
                return BadRequest();

            return Ok();
        }

    }
}
