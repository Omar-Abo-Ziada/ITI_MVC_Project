using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace InstitueProject.Controllers
{
    public class StateController : Controller
    {
        int count;

        public StateController()
        {
            count = 0;
        }
        public IActionResult Increament()
        {
            count++;
            return Content(count.ToString());
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("Name", "Omar");
            HttpContext.Session.SetInt32("Id", 1);

            return Content($"Session data saved ...");
        }

        public IActionResult GetSession()
        {
            string name = HttpContext.Session.GetString("Name");
            int id = HttpContext.Session.GetInt32("Id").Value;

            return Content($"Id = {id} , name : {name}");
        }
    }
}
