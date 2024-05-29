using Microsoft.AspNetCore.Mvc;
using SecondAssignment.Application.Contracts;
using SecondAssignment.Application.Dtos;
using SecondAssignment.Application.Services;

namespace SecondAssignment.WepApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        // GET: GenreController
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _genreService.GetAll();
                if (!result.IsSucces)
                {

                }
                return View(result.Data);
            }
            catch
            {
                return View();
            }

        }



        // GET: GenreController/Create
        public async Task<IActionResult> SaveGenre()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveGenre(SaveGenresDtos saveGenresDtos)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.message = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;
                    return View();
                }
                var result = await _genreService.Save(saveGenresDtos);
                if (!result.IsSucces)
                {
                    ViewBag.message = result.Message;
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: GenreController/Edit/5
        public async Task<IActionResult> EditGenre(Guid id)
        {
            try
            {
                var result = await _genreService.Get(id);
                if (!result.IsSucces)
                {
                    return View(result.Message);
                }
                return View(result.Data);
            }
            catch
            {
                return View("Index");
            }
           
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(UpdateGenresDtos updateGenresDtos)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var resultInner = await _genreService.Get(updateGenresDtos.GenreId);
                    ViewBag.message = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;
                    return View(resultInner.Data);
                }
                var result = await _genreService.Update(updateGenresDtos);
                if (!result.IsSucces)
                {
                    var resultInner = await _genreService.Get(updateGenresDtos.GenreId);
                    ViewBag.message = result.Message;
                    return View(resultInner.Data);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: GenreController/Delete/5
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            try
            {
                var result = await _genreService.Get(id);
                if (!result.IsSucces)
                {
                    return View("Index");
                }
                return View(result.Data);
            }
            catch
            {
                return View("Index");
            }
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGenre(Guid id, IFormCollection collection)
        {
            try
            {
                var result = await _genreService.Delete(id);
                if (!result.IsSucces)
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }
    }
}
