using DB_Task.Models;
using DB_Task.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DB_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext applicationdb = new();
        private TovarsViewModel? tovarsView;

        [HttpGet]
        public IActionResult Index(int? id)
        {
            using (ApplicationContext db = new())
            {
                if (db.Category.ToList().Count == 0)
                {
                    List<CategoryModel> categoryList = new()
                    {
                        new CategoryModel {Tittle = "Ноутбуки"},
                        new CategoryModel {Tittle = "Смартфоны"},
                        new CategoryModel {Tittle = "Телевизоры"},
                    };
                    db.Category.AddRange(categoryList);
                    db.SaveChanges();
                }
                tovarsView = new()
                {
                    TovarsModels = db.Tovars.Include(u => u.Category).Where(u => u.CategoryId == id).ToList(),
                    CategoryModels = db.Category.ToList(),
                    CategoryId = id
                };
            }
            return View(tovarsView);
        }

        [HttpGet]
        public ActionResult AddCategory() => View();

        [HttpPost]
        public ActionResult AddCategory(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                CategoryModel? categoryModel = applicationdb.Category.Where(m => m.Tittle.Contains(category.Tittle)).FirstOrDefault();
                if (categoryModel != null)
                {
                    ModelState.AddModelError("Tittle", "Категория существует с таким названием");
                    return View(category);
                }
                applicationdb.Category.Add(category);
                applicationdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult AddTovar()
        {
            SelectList category = new(applicationdb.Category, "Id", "Tittle");
            ViewBag.category = category;
            return View();
        }

        [HttpPost]
        public ActionResult AddTovar(TovarsModel tovars)
        {
            SelectList category = new(applicationdb.Category, "Id", "Tittle", tovars.CategoryId);
            ViewBag.category = category;
            if (ModelState.IsValid)
            {
                applicationdb.Tovars.Add(tovars);
                applicationdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tovars);
        }

        [HttpGet]
        public ActionResult DeleteTovar(int tovarId) 
        {
            TovarsModel tovars = applicationdb.Tovars.Find(tovarId);
            applicationdb.Tovars.Remove(tovars);
            applicationdb.SaveChanges();
            return RedirectToAction("Index", new { id = tovars.CategoryId});
        }
    }
}
