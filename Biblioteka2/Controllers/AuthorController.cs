using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Biblioteka2.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _uow;

        public AuthorController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            var authors = _uow.Authors.GetAll();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId", "FirstName", "LastName")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _uow.Authors.CreateAsync(author);
                if (await _uow.SaveAsync() > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(author);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await _uow.Authors.GetAsync((int)id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("AuthorId", "FirstName", "LastName")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }
            _uow.Authors.Update(author);
            if (await _uow.SaveAsync()>0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _uow.Authors.GetAsync((int)id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            _uow.Authors.Delete(await _uow.Authors.GetAsync(id));
            await _uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

