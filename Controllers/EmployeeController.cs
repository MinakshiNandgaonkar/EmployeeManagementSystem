using EmployeeInfoManagement.Data;
using EmployeeInfoManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeInfoManagement.Controllers
{  

    public class EmployeeController : Controller
    {
        private readonly EmployeeDBContext _employeeDBContext;

        public EmployeeController(EmployeeDBContext employeeDBContext)
        {
            this._employeeDBContext = employeeDBContext;
        }

        public async Task<IActionResult> EmployeeIndex()
        {
            try
            {
                var employees = await _employeeDBContext.Employees.ToListAsync();
                return View(employees);
            }
            catch (Exception ex)
            {             
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        public IActionResult InsertEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee(Employee addEmployeeModel)
        {
            try
            {
                bool isPhoneExists = await _employeeDBContext.Employees.AnyAsync(e => e.Phone == addEmployeeModel.Phone);
                bool isEmailExists = await _employeeDBContext.Employees.AnyAsync(e => e.Email == addEmployeeModel.Email);

                if (isPhoneExists)
                {
                    ModelState.AddModelError("Phone", "Phone number already exists.");
                }

                if (isEmailExists)
                {
                    ModelState.AddModelError("Email", "Email ID already exists.");
                }

                if (!ModelState.IsValid)
                {
                    return View(addEmployeeModel);
                }

                var employee = new Employee
                {
                    FirstName = addEmployeeModel.FirstName,
                    LastName = addEmployeeModel.LastName,
                    Email = addEmployeeModel.Email,
                    Phone = addEmployeeModel.Phone,
                    Department = addEmployeeModel.Department,
                    HireDate = addEmployeeModel.HireDate
                };

                await _employeeDBContext.Employees.AddAsync(employee);
                await _employeeDBContext.SaveChangesAsync();

                return RedirectToAction("EmployeeIndex");
            }
            catch (Exception ex)
            {               
                ModelState.AddModelError("", "An error occurred while inserting the employee. Error :" + ex.Message);
                return View(addEmployeeModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            try
            {
                var editEmployeeModel = await _employeeDBContext.Employees.FindAsync(id);
                if (editEmployeeModel != null)
                {
                    return View(editEmployeeModel);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {                
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee model)
        {
            try
            {
                var editEmployeeModel = await _employeeDBContext.Employees.FindAsync(model.EmployeeId);
                if (editEmployeeModel != null)
                {
                    editEmployeeModel.FirstName = model.FirstName;
                    editEmployeeModel.LastName = model.LastName;
                    editEmployeeModel.Email = model.Email;
                    editEmployeeModel.Phone = model.Phone;
                    editEmployeeModel.Department = model.Department;
                    editEmployeeModel.HireDate = model.HireDate;

                    _employeeDBContext.Employees.Update(editEmployeeModel);
                    await _employeeDBContext.SaveChangesAsync();

                    return RedirectToAction("EmployeeIndex");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {               
                ModelState.AddModelError("", "An error occurred while updating the employee. Error :" + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var editEmployeeModel = await _employeeDBContext.Employees.FindAsync(id);
                if (editEmployeeModel != null)
                {
                    return View(editEmployeeModel);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {              
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Employee model)
        {
            try
            {
                var employee = await _employeeDBContext.Employees.FindAsync(model.EmployeeId);
                if (employee != null)
                {
                    _employeeDBContext.Employees.Remove(employee);
                    await _employeeDBContext.SaveChangesAsync();

                    return RedirectToAction("EmployeeIndex");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the employee. Error :" + ex.Message);
                return View(model);
            }
        }
    }

}
