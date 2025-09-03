using AutoMapper;
using DEMO.Dto;
using DEMO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEMO.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryPageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryPageService _subcategoryPageService;
        private readonly ICategoryPageService _categoryPageService;

        public SubCategoryPageController(IMapper mapper,ISubCategoryPageService subcategoryPageService,ICategoryPageService categoryPageService)
        {
            _mapper = mapper;
            _subcategoryPageService = subcategoryPageService;
            _categoryPageService = categoryPageService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var dto = new SubCategoryPageDto
            {
                CreatedDate = DateTime.Now, // Pre-fill with today's date
                Status = true               // Optional: default to active
            };

            var categories = await _categoryPageService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubCategoryPageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _subcategoryPageService.AddCategoryPageAsync(dto);

            if (result)
            {
                TempData["PopupMessage"] = "Category added successfully!";
                TempData["PopupType"] = "success";

                // Redirect to admin home to avoid showing popup on reload
                return RedirectToAction("Add", "Registration", new { area = "Admin" });
            }
            else
            {
                TempData["PopupMessage"] = "Failed to add category!";
                TempData["PopupType"] = "error";
                return View(dto);
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            // later you can load the categories from DB and pass to the view
            return View();
        }
    }
}
