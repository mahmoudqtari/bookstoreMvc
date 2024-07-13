using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project1.Data;
using project1.Models;
using project1.ViewModels;
using System;
using static System.Reflection.Metadata.BlobBuilder;

namespace project1.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var books = context.Books
                .Include(book => book.Author)
                .Include(book => book.Categories)
                .ThenInclude(book => book.category) 
                .ToList();

            var bookVms = books.Select(book => new BookVm
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                Publisher = book.Publisher,
                PublishDate = book.PublishDate,
                imageUrl = book.imageUrl,
                Categories = book.Categories.Select(book => book.category.Name).ToList(),

            }).ToList();
            return View(bookVms);
        }
       
        [HttpGet]
        public IActionResult Create()
        {
            var authors = context.authors.OrderBy(authors => authors.Name).ToList();
            var categories = context.categories.OrderBy(authors => authors.Name).ToList();
            var authorList = new List<SelectListItem>();

            foreach (var author in authors)
            {
                authorList.Add(new SelectListItem
                {
                    Value = author.Id.ToString(),
                    Text = author.Name,
                });
            }

            var CategoryList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                CategoryList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                });
            }

            var viewModel = new BookFormVM
            {
                authors = authorList,
                categories = CategoryList 
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(BookFormVM bookFormVM)
        {
            if (!ModelState.IsValid)
            {
                return View(bookFormVM);
            }
            string imageName = null;
            if (bookFormVM.imageUrl != null)
            {
                imageName = Path.GetFileName(bookFormVM.imageUrl.FileName);
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/img/book",imageName);
                var stream = System.IO.File.Create(path);
                bookFormVM.imageUrl.CopyTo(stream);
            }
            var book = new Book
            {
                Title = bookFormVM.Title,
                AuthorId = bookFormVM.AuthorId,
                Publisher = bookFormVM.Publisher,
                PublishDate = bookFormVM.PublishDate,
                Description = bookFormVM.Description,
                imageUrl = imageName,
                Categories = bookFormVM.SelectedCategory.Select(id => new BookCategory
                {
                    CategoryId = id
                }).ToList(),

            };
            context.Books.Add(book);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var book = context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            
            context.Books.Remove(book);
            var path = Path.Combine(webHostEnvironment.WebRootPath,"/img/book", book.imageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = context.Books.
                Include(book => book.Author).
                Include(book => book.Categories).
                ThenInclude(book => book.category).
                First(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }
            
            var bookFormVms = new BookFormVM
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Publisher = book.Publisher,
                PublishDate = book.PublishDate,
            };
           
            return View("Create", bookFormVms);
        }
        [HttpPost]
        public IActionResult Edit(BookFormVM bookFormVms)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", bookFormVms);
            }

            var book = context.Books.
                Include(book => book.Author).
                Include(book => book.Categories).
                ThenInclude(book => book.category).
                First(b => b.Id == bookFormVms.Id);

            if (book is null)
            {
                return NotFound();
            }

            book.Title = bookFormVms.Title;
            book.Description = bookFormVms.Description;
            book.UpdatedOn = DateTime.Now;
            string imageName = null;
            if (bookFormVms.imageUrl != null)
            {
                imageName = Path.GetFileName(bookFormVms.imageUrl.FileName);
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/img/book", imageName);
                var stream = System.IO.File.Create(path);
                bookFormVms.imageUrl.CopyTo(stream);
            }
            book.imageUrl = imageName;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {


            var book = context.Books.
                Include(book => book.Author).
                Include(book => book.Categories).
                ThenInclude(book => book.category).
                First(b => b.Id == id);

            var bookVms = new BookVm
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                Publisher = book.Publisher,
                PublishDate = book.PublishDate,
                imageUrl = book.imageUrl,
                Categories = book.Categories.Select(b => b.category.Name).ToList(),

            };

            if (book == null)
            {
                return NotFound();
            }

            return View(bookVms);
        }


    }

}
