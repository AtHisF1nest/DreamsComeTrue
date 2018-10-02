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
            var users = await _dctRepository.GetUsers(null);

            var usersToReturn = _mapper.Map<IEnumerable<UserForPreviewDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUsers(string name)
        {
            var users = await _dctRepository.GetUsers(name);

            var usersToReturn = _mapper.Map<IEnumerable<UserForPreviewDto>>(users);
            foreach (var user in usersToReturn)
                user.IsInvited = await _dctRepository.IsInvited(user.Id);

            return Ok(usersToReturn);
        }

        [HttpGet("getuser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _dctRepository.GetUser(id);

            var userToReturn = _mapper.Map<UserForPreviewDto>(user);

            return Ok(userToReturn);
        }

        [HttpPost("InviteUser")]
        public async Task<IActionResult> InviteUser(UserForPreviewDto userForPreviewDto)
        {
            if(await _dctRepository.InviteUser(userForPreviewDto.Id))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("UnInviteUser/{id}")]
        public async Task<IActionResult> UnInviteUser(int id)
        {
            if(await _dctRepository.UnInviteUser(id))
                return Ok();
            else 
                return BadRequest();
        }
    }
}