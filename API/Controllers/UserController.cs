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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult GetAll() 
        {
            var DataUser = _unitOfWork.User.GetAll();
            var DTOUser = _mapper.Map<List<DTO.User>>(DataUser);
            return Ok(DTOUser);
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult GetByID(int id) 
        {
            var DataUser = _unitOfWork.User.GetByID(id);
            var DTOUser = _mapper.Map<DTO.User>(DataUser);
            return Ok(DTOUser);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(DTO.User User)
        {
            var DataUser = _mapper.Map<Data.User>(User);
            DataUser.UserTypeID = 1;
            _unitOfWork.User.Create(DataUser);
            _unitOfWork.Complete();
            User.ID = DataUser.ID;

            return Ok(User);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Update(DTO.User User)
        {
            var DataUser = _mapper.Map<Data.User>(User);
            _unitOfWork.User.Update(DataUser);
            _unitOfWork.Complete();
            return Ok(User);
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Delete(DTO.User User)
        {
            var DataUser = _mapper.Map<Data.User>(User);
            _unitOfWork.User.Delete(DataUser);
            _unitOfWork.Complete();
            return Ok(User);
        }
    }
}
