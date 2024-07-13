using Microsoft.AspNetCore.Mvc;
using project1.Data;
using project1.Models;
using project1.ViewModels;

namespace project1.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext context;
        public AuthorsController(ApplicationDbContext context) {
            this.context = context;
        }
        public IActionResult Index()
        {
            var authors = context.authors.ToList();
            //var authorsVm = new List<AuthorVm>();

            var authorsVm = authors.Select(author => new AuthorVm
            {
                Id = author.Id,
                Name = author.Name,
                CreatedOn = author.CreatedOn,
                UpdatedOn = author.UpdatedOn

            }).ToList();

            /*foreach (var author in authors)
            {
                var authorVm = new AuthorVm()
                {
                    Id = author.Id,
                    Name = author.Name,
                    CreatedOn = author.CreatedOn,
                    UpdatedOn = author.UpdatedOn
                };
                authorsVm.Add(authorVm);
                
            }*/
            return View(authorsVm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Form");
        }
        [HttpPost]
        public IActionResult Create(AuthorVm authorVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", authorVm);
            }
            var author = new Author { Name = authorVm.Name };

            try
            {
                context.authors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "name already exists");
                return View(authorVm);
            }

        }
        public IActionResult Details(int id)
        {
            var author = context.authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            var authorVm = new AuthorVm
            {
                Id = author.Id,
                Name = author.Name,
                CreatedOn = author.CreatedOn,
                UpdatedOn = author.UpdatedOn
            };
            return View(authorVm);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = context.authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }
            var authorVm = new AuthorVm
            {
                Id = id,
                Name = author.Name,
                CreatedOn= author.CreatedOn,
                UpdatedOn = author.UpdatedOn
            };
            return View("Form", authorVm);
        }
        [HttpPost]
        public IActionResult Edit(AuthorVm authorVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", authorVm);
            }
            var author = context.authors.Find(authorVm.Id);
            if (author is null)
            {
                return NotFound();
            }
            author.Name = authorVm.Name;
            author.UpdatedOn = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var author = context.authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }
            context.authors.Remove(author);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
