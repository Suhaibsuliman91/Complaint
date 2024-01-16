using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using UI.Client;

namespace UI.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IClientContainer _Client;
        readonly IHttpContextAccessor _Context;
        public ComplaintController(IClientContainer client, IHttpContextAccessor context)
        {

            _Client = client;
            _Context = context;
        }
        public async Task<IActionResult> Index()
        {
            _Context.HttpContext.Request.Cookies.TryGetValue("ID", out var UserID);
            if(UserID != null)
            {
                _Context.HttpContext.Request.Cookies.TryGetValue("Type", out var Type);
                IEnumerable<DTO.Complaint> Complaints = new List<DTO.Complaint>();
                if (Type == "1")
                {
                    Complaints = await _Client.Complaint.GetAll();
                }else if(Type == "2")
                {
                    Complaints = await _Client.Complaint.GetAllForUser(int.Parse(UserID));
                }
                return View(Complaints);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public async Task<IActionResult> AddEdit(int ID)
        {
            _Context.HttpContext.Request.Cookies.TryGetValue("ID", out var UserID);
            _Context.HttpContext.Request.Cookies.TryGetValue("Type", out var Type);

            if (UserID != null && Type != null)
            {
                if (ID == 0)
                {
                    DTO.Status Status = await _Client.Status.GetByID(1);
                    DTO.Complaint Complaint = new DTO.Complaint() { status = Status, Demands = new List<DTO.Demand>() };
                    return View(Complaint);
                }
                else
                {
                    DTO.Complaint Complaint = await _Client.Complaint.GetByID(ID);
                    return View(Complaint);
                }
            }
            else
            {

                return RedirectToAction("Login", "User");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Save(DTO.Complaint Complaint)
        {

              _Context.HttpContext.Request.Cookies.TryGetValue("ID", out var UserID);

            Complaint.UserID = int.Parse(UserID);
            var file = Request.Form.Files;
            Complaint.AttachmentInfo = new List<DTO.FileInfo>();


            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                MemoryStream ms = new MemoryStream();
                Request.Form.Files[i].CopyTo(ms);

                Complaint.AttachmentInfo.Add(new DTO.FileInfo
                {
                    FileName = Request.Form.Files[i].FileName,
                    ContentType = Request.Form.Files[i].ContentType,
                    Bytes = ms.ToArray()
                });

                Complaint.Attachment += Request.Form.Files[i].FileName + ",";
            }
            var Response = await _Client.Complaint.Insert(Complaint);
            if (Response.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in Response.Result.Children())
                {
                    var x = item as JProperty;
                    var name = x.Name;
                    var Values = x.Values();
                    foreach (var item1 in Values)
                    {
                        var JValue = item1 as JValue;
                        var errorValue = item1.ToString();
                        ModelState.AddModelError(name, errorValue);
                    }
                }
                return View("AddEdit", Complaint);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int ID) 
        {
            DTO.Complaint Complaint = new DTO.Complaint
            {
                ID = ID,
                IsDeleted = true
            };
            var Response = await _Client.Complaint.Delete(Complaint);
            if(Response != null)
            {
                return Json(Response);
            }
            else
            {
                var result = new
                {
                    result = Response,
                    type = Response.GetType().Name,
                    errorReasonMessage = "Cannot Delete"
                };
                return Json(result);
            }
        }

        [HttpPost]
        public IActionResult Demand(DTO.Demand demand)
        {
            DTO.Demand TempDemand = new DTO.Demand
            {
                DescriptionEn = demand.DescriptionEn,
                DescriptionAR = demand.DescriptionAR
            };

            ViewBag.RowNum = demand.ID;

            return PartialView("_DemandDetails", demand);
        }

        [HttpGet]
        public async Task<FileResult> DownloadFile(string fileName)
        {
            var FileInfo = await _Client.Complaint.DownloadFile(fileName);
            return File(FileInfo.Bytes, FileInfo.ContentType, FileInfo.FileName);
        }

        public async Task<IActionResult> Display(int ID)
        {
            var Complaint = await _Client.Complaint.GetByID(ID);
            return View(Complaint);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int ID, int StatusID)
        {
            var Complaint = await _Client.Complaint.GetByID(ID);
            
            Complaint.status = null;
            Complaint.StatusID = StatusID;

            var Response = await _Client.Complaint.Update(Complaint);
            if(Response.IsSuccess)
            {
                return Json(Response);
            }
            else
            {
                return Json(null);
            }
        }

    }
}
