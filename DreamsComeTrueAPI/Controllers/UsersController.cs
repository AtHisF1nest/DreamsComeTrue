using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DreamsComeTrueAPI.Dtos;
using DreamsComeTrueAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAcc;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UsersController(IDCTRepository dctRepository, IMapper mapper, IHttpContextAccessor httpContextAcc, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAcc = httpContextAcc;
            _mapper = mapper;
            _dctRepository = dctRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _dctRepository.GetUsersForInvite(null);

            var usersToReturn = _mapper.Map<IEnumerable<UserForPreviewDto>>(users);
            foreach (var user in usersToReturn)
            {
                user.IsInvited = await _dctRepository.IsInvited(user.Id);
                user.InvitedYou = await _dctRepository.Inviting(user.Id);
            }

            return Ok(usersToReturn);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUsers(string name)
        {
            var users = await _dctRepository.GetUsersForInvite(name);

            var usersToReturn = _mapper.Map<IEnumerable<UserForPreviewDto>>(users);
            foreach (var user in usersToReturn)
            {
                user.IsInvited = await _dctRepository.IsInvited(user.Id);
                user.InvitedYou = await _dctRepository.Inviting(user.Id);
            }

            return Ok(usersToReturn);
        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(_mapper.Map<UserForPreviewDto>(await _dctRepository.GetCurrentUser()));
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
            if (!await _dctRepository.IsInvited(userForPreviewDto.Id) && await _dctRepository.InviteUser(userForPreviewDto.Id))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("UnInviteUser/{id}")]
        public async Task<IActionResult> UnInviteUser(int id)
        {
            if (await _dctRepository.UnInviteUser(id))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("AcceptInvite/{id}")]
        public async Task<IActionResult> AcceptInvite(int id)
        {
            if (await _dctRepository.AcceptInvite(id))
                return Ok();
            else
                return BadRequest();
        }

        // [HttpPost("EditAvatar")]
        // public async Task<IActionResult> EditAvatar()
        // {
        //     var userFromRepo = await _dctRepository.GetCurrentUser();

        //     var file = _httpContextAcc.HttpContext.Request.Form.Files[0];
        //     if (file.Length > 0)
        //     {
        //         var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "uploads");
        //         if (!Directory.Exists(uploads))
        //             Directory.CreateDirectory(uploads);
                    
        //         var fileName = file.FileName.Replace(".", $"__{ userFromRepo.Id }.");
        //         var filePath = Path.Combine(uploads, fileName);
        //         using (var fileStream = new FileStream(filePath, FileMode.Create)) {
        //             await file.CopyToAsync(fileStream);
        //         }
        //         var photo = await _dctRepository.UploadPhoto(fileName, userFromRepo.Id);
        //         userFromRepo.Photo = photo;
        //         return Ok(_mapper.Map<UserForPreviewDto>(userFromRepo));
        //     }

        //     return BadRequest("No file uploaded.");
        // }

        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(UserForEditDto user)
        {
            var userFromRepo = await _dctRepository.GetCurrentUser();

            if (!string.IsNullOrWhiteSpace(user.Name) && userFromRepo.Name != user.Name)
            {
                if (user.Name.Length > 4)
                {
                    userFromRepo.Name = user.Name;
                }
                else
                    return BadRequest("Taka nazwa jest zbyt krótka!");
            }

            if (await _dctRepository.SaveAll())
                return Ok(userFromRepo);
            else
                return BadRequest("Coś poszło nie tak..");
        }
    }
}