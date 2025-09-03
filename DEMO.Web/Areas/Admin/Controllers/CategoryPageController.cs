using AutoMapper;
using DEMO.Dto;
//using DEMO.Dto.Listing;
using DEMO.Services;
using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

namespace DEMO.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryPageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryPageService _categoryPageService;

        public CategoryPageController(IMapper mapper, ICategoryPageService categoryPageService)
        {
            _mapper = mapper;
            _categoryPageService = categoryPageService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var dto = new CategoryPageDto
            {
                CreatedDate = DateTime.Now, // Pre-fill with today's date
                Status = true               // Optional: default to active
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryPageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _categoryPageService.AddCategoryPageAsync(dto);

            if (result)
            {
                TempData["PopupMessage"] = "Category added successfully!";
                TempData["PopupType"] = "success";

                // Redirect to admin home to avoid showing popup on reload
                return RedirectToAction("Add", "SubCategoryPage", new { area = "Admin" });
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
