using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public AdminController(IApplicationUserService applicationUserService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetPatient(string lastName)
        {
            var patients = await _applicationUserService.GetPatientByLastNameAsync(lastName);

            if (patients == null || !patients.Any())
            {
                ViewBag.error = "error";
            }

            var viewModel = _mapper.Map<IEnumerable<ApplicationUserViewModel>>(patients);
            return View(viewModel);
        }

        public async Task<IActionResult> Patients()
        {
            var patients = await _applicationUserService.GetAllPatientsAsync();

            if (patients == null || patients.Count() == 0)
            {
                ViewBag.error = "error";
            }

            var viewModel = _mapper.Map<IEnumerable<ApplicationUserViewModel>>(patients);
            return View(viewModel);
        }

        public async Task<IActionResult> PatientDetails(string id)
        {
            var patient = await _applicationUserService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                ViewBag.error = "error";
            }
            var viewModel = _mapper.Map<ApplicationUserViewModel>(patient);
            return View(viewModel);
        }

        public async Task<IActionResult> PatientEdit(string id)
        {
            var patient = await _applicationUserService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                ViewBag.error = "error";
            }

            var viewModel = _mapper.Map<ApplicationUserViewModel>(patient);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PatientEdit(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<ApplicationUserDto>(model);
                await _applicationUserService.UpdatePatientAsync(userDto);
                return RedirectToAction("PatientDetails", new { id = model.Id });
            }

            return View(model);
        }

        public async Task<IActionResult> PatientConfirmDelete(string id)
        {
            var patient = await _applicationUserService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                ViewBag.error = "error";
            }

            var viewModel = _mapper.Map<ApplicationUserViewModel>(patient);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientDeleteConfirmed(string id)
        {
            await _applicationUserService.DeletePatientAsync(id);
            TempData["SuccessMessage"] = "Patient successfully deleted.";
            return RedirectToAction("Patients");
        }
    }
}
