using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Repository;
using Service.InterFace;
using System.Collections.Generic;
using System.Net.Mail;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ComplaintController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _iWebHostEnvironment;
        public ComplaintController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment iWebHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iWebHostEnvironment = iWebHostEnvironment;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll()
        {
            var EntityComplaint = _unitOfWork.Complaint.GetAll();
            var DTOComplaint = _mapper.Map<List<DTO.Complaint>>(EntityComplaint);
            return Ok(DTOComplaint);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetByID(int ID)
        {
            var EntityComplaint = _unitOfWork.Complaint.GetByID(ID);
            var DTOComplaint = _mapper.Map<DTO.Complaint>(EntityComplaint);
            return Ok(DTOComplaint);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(DTO.Complaint Complaint)
        {
            Complaint.StatusID = 1;
            Complaint.Attachment = String.Empty;
            foreach (var item in Complaint.AttachmentInfo)
            {
                var SavedFileUrl = WriteAttachment(item, _iWebHostEnvironment.ContentRootPath + "\\Files\\");
                Complaint.Attachment += SavedFileUrl;
            }
            var EntityComplaint = _mapper.Map<Data.Complaint>(Complaint);
            _unitOfWork.Complaint.Create(EntityComplaint);
            _unitOfWork.Complete();
            Complaint.ID = EntityComplaint.ID;

            return Ok(Complaint);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Update(DTO.Complaint Complaint)
        {
            Complaint.Demands = RemoveExistenDemands(Complaint.Demands.ToList());
            var EntityComplaint = _mapper.Map<Data.Complaint>(Complaint);
            _unitOfWork.Complaint.Update(EntityComplaint);
            _unitOfWork.Complete();
            return Ok(Complaint);
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Delete(DTO.Complaint Complaint)
        {
            var EntityComplaint = _unitOfWork.Complaint.GetByID(Complaint.ID);
            EntityComplaint.IsDeleted = Complaint.IsDeleted;
            _unitOfWork.Complaint.Update(EntityComplaint);
            _unitOfWork.Complete();
            return Ok(Complaint);
        }


        private List<DTO.Demand> RemoveExistenDemands(List<DTO.Demand> demands) 
        {
            var AllDemands = _unitOfWork.Demand.GetAll().ToList();
            var DTOAllDemands = _mapper.Map<List<DTO.Demand>>(AllDemands);
            List <DTO.Demand> TempDemands = new List<DTO.Demand>();
            foreach (var demand in demands) 
            {
                var TempDemand = DTOAllDemands.Find(e => e.ID == demand.ID && e.DescriptionEn == demand.DescriptionEn) != null;
                if (!TempDemand)
                {
                    TempDemands.Add(demand);
                    //demands.Remove(demand);
                }
            }

            return TempDemands;

        }

        private string WriteAttachment(DTO.FileInfo FileInfo, string SavedFilePath)
        {
            var fileName = Guid.NewGuid() + FileInfo.FileName.Substring(FileInfo.FileName.IndexOf("."));
            byte[] Bytes = FileInfo.Bytes;
            FileStream fs = new FileStream(SavedFilePath + fileName, FileMode.Create);
            fs.Write(Bytes, 0, Bytes.Length);
            fs.Flush();
            fs.Close();

            return fileName;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult DownloadFile(/*[FromForm]*/ string fileName)
        {
            var filepath = Path.Combine( _iWebHostEnvironment.ContentRootPath + $"\\Files\\{fileName}");
            //var filepath = Path.Combine(_iWebHostEnvironment.WebRootPath, "Files", Splitted);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filepath);
            string ContentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out ContentType);
            var fsSource = fileInfo.OpenRead();

            byte[] bytes = new byte[fsSource.Length];
            int numBytesToRead = (int)fsSource.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            numBytesToRead = bytes.Length;

            DTO.FileInfo info = new DTO.FileInfo
            {
                ContentType = ContentType,
                Bytes = bytes,
                FileName = fileInfo.Name,
                FullFileName = fileInfo.FullName,
                Extension = fileInfo.Extension,

            };
            return Ok(info);
        }

    }
}
