using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DreamsComeTrueAPI.Dtos;
using DreamsComeTrueAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamsComeTrueAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDCTRepository _dctRepository;
        private readonly IMapper _mapper;
        public UsersController(IDCTRepository dctRepository, IMapper mapper)
        {
            _mapper = mapper;
            _dctRepository = dctRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _dctRepository.GetUsers();

            var usersToReturn = _mapper.Map<IEnumerable<UserForPreviewDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _dctRepository.GetUser(id);

            var userToReturn = _mapper.Map<UserForPreviewDto>(user);

            return Ok(userToReturn);
        }
    }
}