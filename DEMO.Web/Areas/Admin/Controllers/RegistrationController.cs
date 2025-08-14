using DEMO.Dto;
using DEMO.Services;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        // GET: Registration/Add
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Load dropdown data
            ViewBag.Categories = await _registrationService.GetAllCategoriesAsync();
            ViewBag.SubCategories = await _registrationService.GetAllSubCategoriesAsync();

            return View(new RegistrationDto());
        }

        // POST: Registration/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RegistrationDto dto)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns when validation fails
                ViewBag.Categories = await _registrationService.GetAllCategoriesAsync();
                ViewBag.SubCategories = await _registrationService.GetAllSubCategoriesAsync();
                return View(dto);
            }

            bool isSaved = await _registrationService.AddRegistrationAsync(dto);

            if (!isSaved)
            {
                // SubCategory does not belong to selected Category
                ModelState.AddModelError("", "The selected SubCategory does not match the Category.");
                ViewBag.Categories = await _registrationService.GetAllCategoriesAsync();
                ViewBag.SubCategories = await _registrationService.GetAllSubCategoriesAsync();
                return View(dto);
            }

            // Only reach here if saved successfully
            TempData["Success"] = "Registration saved successfully!";
            return RedirectToAction(nameof(Add));
        }

        //[HttpGet]
        //public async Task<JsonResult> GetSubCategoriesByCategory(int categoryId)
        //{
        //    var allSubCategories = await _registrationService.GetAllSubCategoriesAsync();
        //    var filtered = allSubCategories
        //        .Where(sc => sc.CategoryId == categoryId)
        //        .ToList();

        //    return Json(filtered);
        //}

        [HttpGet]
        public async Task<JsonResult> GetSubCategories(int categoryId)
        {
            var subCategories = await _registrationService.GetSubCategoriesByCategoryIdAsync(categoryId);
            return Json(subCategories);
        }

    }
}

