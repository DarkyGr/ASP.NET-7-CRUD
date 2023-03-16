using ASP.NET_Crud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using ASP.NET_Crud.Controllers;
using ASP.NET_Crud.Repositories.Contract;

namespace ASP.NET_Crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Department> _departmentRepo;
        private readonly IGenericRepository<Employee> _employeeRepo;

        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<Department> departmentRepository,
            IGenericRepository<Employee> employeeRepository)
        {
            _logger = logger;
            _departmentRepo = departmentRepository;
            _employeeRepo = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Method httpget to get list of departments
        [HttpGet]
        public async Task<IActionResult> GetListDepartments()
        {
            List<Department> _list = await _departmentRepo.List();

            return StatusCode(StatusCodes.Status200OK, _list);  // Return successful answer          
        }
        
        // Method httpget to get list of employees
        [HttpGet]
        public async Task<IActionResult> GetListEmployees()
        {
            List<Employee> _list = await _employeeRepo.List();

            return StatusCode(StatusCodes.Status200OK, _list);  // Return successful answer          
        }

        // Method httppost to save employee
        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] Employee model)
        {

            bool _result = await _employeeRepo.Save(model);

            if (_result) {
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
            }            
        }

        // Method httpput to edit employee
        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody] Employee model)
        {

            bool _result = await _employeeRepo.Edit(model);

            if (_result)
            {
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
            }
        }

        // Method httpdelete to delete employee
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int idEmployee)
        {

            bool _result = await _employeeRepo.Delete(idEmployee);

            if (_result)
            {
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}