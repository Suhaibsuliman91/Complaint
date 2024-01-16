using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using UI.Client;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        readonly IClientContainer _Client;
        readonly IHttpContextAccessor _Context;
        public UserController(IClientContainer client, IHttpContextAccessor httpContextAccessor)
        {
            _Client = client;
            _Context = httpContextAccessor;

        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register_Post(DTO.User user)
        {
            var Response = await _Client.User.Insert(user);
            if (Response.IsSuccess)
            {
                return RedirectToAction("Login", "User");
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
                return View("Register", user);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login_Post(DTO.User user)
        {
            _Context.HttpContext.Request.Cookies.TryGetValue("ID", out var ID);
            _Context.HttpContext.Request.Cookies.TryGetValue("Type", out var Type);
            if(ID != null)
            {
                _Context.HttpContext.Response.Cookies.Delete("ID");
            }
            if(Type != null)
            {
                _Context.HttpContext.Response.Cookies.Delete("Type");
            }


            DTO.User User = await _Client.User.Login(user);
            if (User != null)
            {
                _Context.HttpContext.Response.Cookies.Append("ID", User.ID.ToString());
                _Context.HttpContext.Response.Cookies.Append("Type", User.UserType.ID.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login", user);
            }
            return View(User);
        }

        public async Task<IActionResult> Profile(int UserID)
        {
            var User = await _Client.User.GetByID(UserID);
            if(User != null)
            {
                return View(User);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(DTO.User User)
        {
            var Response = await _Client.User.Update(User);
            if (Response.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
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
                return View("Profile", User);
            }
        }

    }
}
