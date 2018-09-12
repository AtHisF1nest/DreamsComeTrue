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

    }
}
