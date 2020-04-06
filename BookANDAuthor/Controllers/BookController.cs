using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookANDAuthor.Models;
using BookANDAuthor.Models.Repository;
using BookANDAuthor.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookANDAuthor.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository<Book> bookRepository;
        private readonly IBookRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookRepository<Book> bookRepository,IBookRepository<Author> authorRepository ,
            IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: Book
        public ActionResult Index()
        {
            var book = bookRepository.List();
            return View(book);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FilSelectList()

            };
            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if (model.File !=null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                        fileName = model.File.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an author from the list";
                      
                        return View(GetAllAuthors());

                    }
                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageUrl=fileName
                    };
                    // TODO: Add insert logic here
                    bookRepository.Add(book);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
           ;
            ModelState.AddModelError("", "You Have Fill all the required feild");
            return View(GetAllAuthors());
          
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList(),
                ImageUrl=book.ImageUrl

            };
            return View(viewModel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookAuthorViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                string fileName = string.Empty;
                if (viewModel.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                    fileName = viewModel.File.FileName;
                    string fullPath = Path.Combine(uploads, fileName);
                    string oldFileName = viewModel.ImageUrl;
                    string fullOldPath = Path.Combine(uploads, oldFileName);
               
                    // save the new file
                    if(fullPath != fullOldPath)
                    {
                        System.IO.File.Delete(fullOldPath);
                        viewModel.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                  
                }
                var author = authorRepository.Find(viewModel.AuthorId);
                Book book = new Book
                {
                 
                    Id= viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImageUrl=fileName
                };
               
                bookRepository.Update(viewModel.BookId, book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FilSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "--please select an author--" });
            return authors;
        }
        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FilSelectList()

            };
            return vmodel;

        }
    }
}