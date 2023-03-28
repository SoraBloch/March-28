using March_27_adding_multiple_people.Web.Models;
using March_27_library.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace March_27_adding_multiple_people.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connnectionString = "Data Source=.\\sqlexpress;Initial Catalog=People;Integrated Security=True";
        public IActionResult Index()
        {
            PeopleManager manager = new PeopleManager(_connnectionString);
            PeopleViewModel vm = new();
            vm.People = manager.GetAllPeople();
            return View(vm);
        }
        public IActionResult AddView()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToDB(List<Person> people)
        {
            PeopleManager manager = new PeopleManager(_connnectionString);
            manager.AddListOfPeople(people);
            return Redirect("/home/index");
        }
    }
}