using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<MvcEmployeeModel> employeeList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("employee").Result;
            employeeList = response.Content.ReadAsAsync<IEnumerable<MvcEmployeeModel>>().Result;

            return View(employeeList);
        }

        public ActionResult ManagementEmployee(int id = 0)
        {
            if (id == 0)
            {
                return View(new MvcEmployeeModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync($"Employee/{id}").Result;
                return View(response.Content.ReadAsAsync<MvcEmployeeModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult ManagementEmployee(MvcEmployeeModel employee)
        {
            if (employee.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee", employee).Result;
                TempData["SuccessMessage"] = "Salvo com Sucesso!";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync($"Employee/{employee.Id}", employee).Result;
                TempData["SuccessMessage"] = "Alterado com Sucesso!";
            }

            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync($"Employee/{id}").Result;
            TempData["SuccessMessage"] = "Excluido com Sucesso!";
            
            return RedirectToAction("index");

        }
    }
}