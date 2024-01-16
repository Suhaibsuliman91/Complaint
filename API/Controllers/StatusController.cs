using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Service.InterFace;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StatusController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll()
        {
            var EntityStatus = _unitOfWork.Status.GetAll();
            var DTOStatus = _mapper.Map<List<DTO.Status>>(EntityStatus);
            return Ok(DTOStatus);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetByID(int ID)
        {
            var EntityStatus = _unitOfWork.Status.GetByID(ID);
            var DTOStatus = _mapper.Map<DTO.Status>(EntityStatus);
            return Ok(DTOStatus);
        }

    }
}
