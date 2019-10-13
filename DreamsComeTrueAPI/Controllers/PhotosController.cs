using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DreamsComeTrueAPI.Dtos;
using DreamsComeTrueAPI.Models;
using DreamsComeTrueAPI.Repositories.Interfaces;
using DreamsComeTrueAPI.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DreamsComeTrueAPI.Controllers
{
    [Authorize]
    [Route("api/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDCTRepository _dCTRepository;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly Cloudinary _cloudinary;
        private readonly int _userId;
        private readonly IHttpContextAccessor _httpContextAcc;

        public PhotosController(IMapper mapper, IDCTRepository dCTRepository, IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAcc)
        {
            _httpContextAcc = httpContextAcc;
            _mapper = mapper;
            _dCTRepository = dCTRepository;
            _appSettings = appSettings;

            Account acc = new Account(
                appSettings.Value.CloudinarySettings.CloudName,
                appSettings.Value.CloudinarySettings.ApiKey,
                appSettings.Value.CloudinarySettings.ApiSecret);

            _cloudinary = new Cloudinary(acc);

            _userId = _dCTRepository.GetCurrentUser().Result.Id;
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser()
        {
            var userFromRepo = await _dCTRepository.GetUser(_userId);

            var file = _httpContextAcc.HttpContext.Request.Form.Files[0];
            var photo = new PhotoForCreationDto();

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(100).Height(100).Crop("fill")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photo.Url = uploadResult.Uri.ToString();
            photo.PublicId = uploadResult.PublicId;

            var photoFinal = _mapper.Map<Photo>(photo);

            if (userFromRepo.Photo != null)
                await _dCTRepository.DeletePhoto(userFromRepo.Photo.Id);

            userFromRepo.Photo = photoFinal;

            if (await _dCTRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photoFinal);

                return CreatedAtRoute("GetPhoto", new { id = photoFinal.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _dCTRepository.GetPhoto(id);

            var photoToReturn = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photoToReturn);
        }
    }
}