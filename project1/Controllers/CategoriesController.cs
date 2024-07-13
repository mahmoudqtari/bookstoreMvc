using Microsoft.AspNetCore.Mvc;
using project1.Data;
using project1.Models;
using project1.ViewModels;

namespace project1.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;
        public CategoriesController( ApplicationDbContext context)
        {
            this.context = context; 

        }
        public IActionResult Index()
        {
            var categories = context.categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryVm categoryvm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categoryvm);
            }
            
            var category = new Category { Name = categoryvm.Name };

            try
            {
                context.categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name","name already exists");
                return View(categoryvm);
            }
            
        }
        public IActionResult CheckName(CategoryVm categoryVm)
        {
            var isExists = context.categories.Any(category => category.Name == categoryVm.Name);
            return Json(isExists);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = context.categories.Find(id);
            if(category is null)
            {
                return NotFound();
            }
            var categoryView = new CategoryVm
            {
                Id = id,
                Name = category.Name,
            };
            return View("Create",categoryView);
        }
        [HttpPost]
        public IActionResult Edit(CategoryVm categoryVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",categoryVm);
            }
            var category = context.categories.Find(categoryVm.Id);
            if( category is null)
            {
                return NotFound();
            }
            category.Name = categoryVm.Name;
            category.UpdatedOn = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var category = context.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            var viewModel = new CategoryVm
            {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
                UpdatedOn = category.UpdatedOn
            };
            return View(viewModel);
        }
        public IActionResult Delete(int id)
        {
            var category = context.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            context.categories.Remove(category);
            context.SaveChanges();
            return Ok();
        }

    }
}
