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
    public class UserTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll()
        {
            var EntityUserType = _unitOfWork.UserType.GetAll();
            var DTOUserType = _mapper.Map<List<DTO.UserType>>(EntityUserType);
            return Ok(DTOUserType);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetByID(int ID)
        {
            var EntityUserType = _unitOfWork.UserType.GetByID(ID);
            var DTOUserType = _mapper.Map<DTO.UserType>(EntityUserType);
            return Ok(DTOUserType);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(DTO.UserType UserType)
        {
            var EntityUserType = _mapper.Map<Data.UserType>(UserType);
            _unitOfWork.UserType.Create(EntityUserType);
            _unitOfWork.Complete();
            UserType.ID = EntityUserType.ID;
            return Ok(UserType);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Update(DTO.UserType UserType)
        {
            var EntityUserType = _mapper.Map<Data.UserType>(UserType);
            _unitOfWork.UserType.Update(EntityUserType);
            _unitOfWork.Complete();
            return Ok(UserType);
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Delete(DTO.UserType UserType) 
        {
            var EntityUserType = _mapper.Map<Data.UserType>(UserType);
            _unitOfWork.UserType.Update(EntityUserType);
            _unitOfWork.Complete();
            return Ok(UserType);
        }
    }
}
