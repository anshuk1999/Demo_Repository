using AutoMapper;
using DEMO.Dto;
using DEMO.Services;
using Microsoft.AspNetCore.Mvc;


namespace DEMO.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleService _vehicleService;

        public VehicleController(IMapper mapper, IVehicleService vehicleService)
        {
            _mapper = mapper;
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task <IActionResult> Add(int? id, string section = "details")       
        {
            ViewBag.Section = section;
            AddVehicleDto model = new AddVehicleDto();
            if (id.HasValue && id.Value > 0)
            {
                var entity = await _vehicleService.GetVehicleByIdAsync(id.Value);
                if (entity == null)
                {
                    TempData["PopupMessage"] = "Vehicle not found!";
                    TempData["PopupType"] = "error";
                    return RedirectToAction("List");
                }

                model = _mapper.Map<AddVehicleDto>(entity);
                model.ExistingFileUploadPan = entity.FileUploadPan;
                model.ExistingFileUploadRc = entity.FileUploadRc;
                model.ExistingFileUploadAlt = entity.FileUploadAlt;
                // optional: assign existing file names for preview
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleDto dto,string section)
        {
            ViewBag.Section = section;

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            bool result;

            if (dto.Id > 0)
            {
                // It's an edit — fetch existing vehicle from DB
                var existing = await _vehicleService.GetVehicleByIdAsync(dto.Id);

                if (existing == null)
                {
                    TempData["PopupMessage"] = "Vehicle not found!";
                    TempData["PopupType"] = "error";
                    return RedirectToAction("List");
                }

                // Merge the unchanged fields from existing data
                if (section == "documents")
                {
                    dto.VehicleNumber = existing.VehicleNumber;
                    dto.OwnerName = existing.OwnerName;
                    dto.ChasisNumber = existing.ChasisNumber;
                    dto.EngineNumber = existing.EngineNumber;
                    dto.RegistrationDate = existing.RegistrationDate;
                    dto.Maker = existing.Maker;
                    dto.VehicleModel = existing.VehicleModel;
                    dto.ManufacturingYear = existing.ManufacturingYear;
                    dto.Financier = existing.Financier;
                    dto.Capacity = existing.Capacity;
                }
                if (section == "details")
                {
                    // Preserve document-related data when details section is posted
                    dto.AuthorizedNamePan = existing.AuthorizedNamePan;
                    dto.AuthorizedNumberPan = existing.AuthorizedNumberPan;
                    dto.ExpiryDatePan = existing.ExpiryDatePan;
                    dto.ReminderDayPan = existing.ReminderDayPan;
                    dto.FileUploadPan = null; // will stay as existing

                    dto.AuthorizedNameRc = existing.AuthorizedNameRc;
                    dto.RcNumber = existing.RcNumber;
                    dto.ExpiryDateRc = existing.ExpiryDateRc;
                    dto.ReminderDayRc = existing.ReminderDayRc;
                    dto.FileUploadRc = null;

                    dto.AuthorizedNameAlt = existing.AuthorizedNameAlt;
                    dto.AuthorizedNumberAlt = existing.AuthorizedNumberAlt;
                    dto.ExpiryDateAlt = existing.ExpiryDateAlt;
                    dto.ReminderDayAlt = existing.ReminderDayAlt;
                    dto.FileUploadAlt = null;
                }
                result = await _vehicleService.UpdateVehicleAsync(dto);
                TempData["PopupMessage"] = result ? "Vehicle updated successfully!" : "Failed to update vehicle!";
            }
            else
            {
                // Adding new vehicle
                result = await _vehicleService.AddVehicleAsync(dto);
                TempData["PopupMessage"] = result ? "Vehicle added successfully!" : "Failed to add vehicle!";
            }

            TempData["PopupType"] = result ? "success" : "error";

            return result ? RedirectToAction("List") : View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> List(string searchTerm, string sortColumn = "CreatedAt", string sortDirection = "desc", int page = 1, int pageSize = 10, string fromDate = null, string toDate = null)       
        {
            DateTime? from = string.IsNullOrEmpty(fromDate) ? null : DateTime.ParseExact(fromDate, "yyyy-MM-dd", null);
            DateTime? to = string.IsNullOrEmpty(toDate) ? null : DateTime.ParseExact(toDate, "yyyy-MM-dd", null);

            var filter = new VehiclePaginationFilterDto
            {
                SearchTerm = searchTerm,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                PageNumber = page,
                PageSize = pageSize,
                FromDate = from, 
                ToDate = to
            };

            var result = await _vehicleService.GetVehicleListAsync(filter);

            ViewBag.TotalCount = result.TotalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = page;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return View(result.Items);
        }
        [HttpPost]
        public async Task<IActionResult> GetVehicleData()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Convert.ToInt32(Request.Form["start"]);
            var length = Convert.ToInt32(Request.Form["length"]);
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();
            var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var fromDateStr = Request.Form["fromDate"].FirstOrDefault();
            var toDateStr = Request.Form["toDate"].FirstOrDefault();

            DateTime? fromDate = !string.IsNullOrEmpty(fromDateStr) ? DateTime.Parse(fromDateStr) : null;
            DateTime? toDate = !string.IsNullOrEmpty(toDateStr) ? DateTime.Parse(toDateStr) : null;

            int page = (start / length) + 1;

            var filter = new VehiclePaginationFilterDto
            {
                SearchTerm = searchValue,
                SortColumn = sortColumn ?? "CreatedAt",
                SortDirection = sortDirection ?? "desc",
                PageNumber = page,
                PageSize = length,
                FromDate = fromDate,
                ToDate = toDate
            };

            var result = await _vehicleService.GetVehicleListAsync(filter);

            return Json(new
            {
                draw = draw,
                recordsTotal = result.TotalCount,
                recordsFiltered = result.TotalCount,
                data = result.Items
            });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var newStatus = await _vehicleService.ToggleStatusAsync(id);
            if (newStatus == null)
                return Json(new { success = false, message = "Vehicle not found" });

            return Json(new { success = true, newStatus = newStatus });
        }
        [HttpGet]
        public async Task<IActionResult> FirmDocument(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                TempData["PopupMessage"] = "Vehicle not found!";
                TempData["PopupType"] = "error";
                return RedirectToAction("List");
            }

            return View(vehicle);
        }
        
    }
}

//this.RestoreModelState(); for add method

//AddVehicleDto model = new AddVehicleDto();

//if (TempData["VehicleFormData"] != null)
//{
//    model = JsonConvert.DeserializeObject<AddVehicleDto>(TempData["VehicleFormData"].ToString());
//}

//if (TempData["Error"] != null)
//{
//    ViewBag.Error = TempData["Error"];
//}