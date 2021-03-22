using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Biblioteka2.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _uow;

        public BookController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            var books = _uow.Books.GetAll();
            return View(books);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book book;
            if (User != null && User.IsInRole("emp"))
            {
                book = await _uow.Books.GetBookAndItsReservationsAsync((int)id);
            }
            else
            {
                book = await _uow.Books.GetAsync((int)id);
            }

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "emp")]
        public IActionResult Create()
        {
            ViewData["Authors"] = new SelectList(_uow.Authors.GetAll(), "AuthorId", "FirstAndLastName");
            return View();
        }

        [Authorize(Roles = "emp")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId", "Name", "ReleaseDate", "AuthorId", "Description", "Quantity")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _uow.Books.CreateAsync(book);
                if(await _uow.SaveAsync() > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Authors"] = new SelectList(_uow.Authors.GetAll(), "AuthorId", "FirstAndLastName");
            return View(book);
        }

        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _uow.Books.GetAsync((int)id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["Authors"] = new SelectList(_uow.Authors.GetAll(), "AuthorId", "FirstAndLastName");
            return View(book);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("BookId", "Name", "ReleaseDate", "AuthorId", "Description", "Quantity")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            ViewData["Authors"] = new SelectList(_uow.Authors.GetAll(), "AuthorId", "FirstAndLastName");
            _uow.Books.Update(book);
            if (await _uow.SaveAsync() > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [Authorize(Roles = "emp")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _uow.Books.GetAsync((int)id);
            if (id == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "emp")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            _uow.Books.Delete(await _uow.Books.GetAsync(id));
            await _uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
