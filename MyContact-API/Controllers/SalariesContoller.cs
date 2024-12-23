using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyContact_API.Controllers
{
    public class SalariesContoller : Controller
    {
        // GET: SalariesContoller
        public ActionResult Index()
        {
            return View();
        }

        // GET: SalariesContoller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalariesContoller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalariesContoller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalariesContoller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalariesContoller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalariesContoller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalariesContoller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
