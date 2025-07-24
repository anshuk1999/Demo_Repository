using DEMO.Dto;
using DEMO.Services;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IAdminAuthService _authService;

        public AuthController(IAdminAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                // This will return the form with validation error messages
                return View(model);
            }

            var result = await _authService.AuthenticateAsync(model);
            if (result == null)
            {
                TempData["Error"] = "Invalid email or password.";
                return RedirectToAction("Login");
            }

            if (model.RememberMe)
            {
                // Store in cookies (valid for 30 days)
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append("Token", result.Token, cookieOptions);
                Response.Cookies.Append("AdminName", result.Name, cookieOptions);
                Response.Cookies.Append("Expiry", result.ExpiryTime.ToString(), cookieOptions);
            }
            else
            {
                // Store in session
                HttpContext.Session.SetString("Token", result.Token);
                HttpContext.Session.SetString("AdminName", result.Name);
                HttpContext.Session.SetString("Expiry", result.ExpiryTime.ToString());
            }

            return RedirectToAction("Home", "Dashboard", new { area = "Admin" });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("AdminName");
            Response.Cookies.Delete("Expiry");

            return RedirectToAction("Login");
        }
    }
}
