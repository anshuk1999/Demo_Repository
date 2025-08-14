using AutoMapper;
using DEMO.Dto;
using DEMO.Services;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoanCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILoanCategoryService _loanCategoryService;

        public LoanCategoryController(ILoanCategoryService loanCategoryService, IMapper mapper)
        {
            _loanCategoryService = loanCategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, string? categoryName)      
        {
            LoanCategoryDto dto;

            if (id.HasValue && id > 0)
            {
                var entity = await _loanCategoryService.GetLoanCategoryByIdAsync(id.Value);
                dto = _mapper.Map<LoanCategoryDto>(entity);
                dto.ExistingLogoPath = entity.LogoPath;
            }
            else
            {
                // ✨ This ensures blank form when just filtering or viewing the page
                dto = new LoanCategoryDto();
            }

            // This part is ONLY for the dropdown now
            var listResult = await _loanCategoryService.GetLoanCategoryListAsync(new LoanCategoryPaginationFilterDto
            {
                PageNumber = 1,
                PageSize = 100, // or whatever fits your design
                CategoryName = categoryName // 🔥 This line now works
            });

            // 🔥 Add this line
            ViewBag.AllCategories = listResult.Items
                .Select(x => x.CategoryName)
                .Distinct()
                .ToList();

            ViewBag.SelectedCategory = categoryName;
            ModelState.Clear();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoanCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            bool result;
            if (dto.Id > 0)
                result = await _loanCategoryService.UpdateLoanCategoryAsync(dto);
            else
                result = await _loanCategoryService.AddLoanCategoryAsync(dto);

            TempData["PopupMessage"] = result ? "Saved successfully!" : "Failed to save!";
            TempData["PopupType"] = result ? "success" : "error";

            return RedirectToAction("Index", new { id = (int?)null });
        }

        [HttpPost]
        public async Task<JsonResult> ToggleStatus(int id)
        {
            var newStatus = await _loanCategoryService.ToggleStatusAsync(id);
            if (newStatus == null)
                return Json(new { success = false });

            return Json(new { success = true, newStatus });
        }

        [HttpGet]
        public async Task<JsonResult> GetLoanCategoryData(string? categoryName)
        {
            var result = await _loanCategoryService.GetLoanCategoryListAsync(new LoanCategoryPaginationFilterDto
            {
                PageNumber = 1,
                PageSize = 1000, // Or whatever you want
                CategoryName = categoryName
            });

            return Json(new { data = result.Items }); // this is for DataTables
        }
    }
}
