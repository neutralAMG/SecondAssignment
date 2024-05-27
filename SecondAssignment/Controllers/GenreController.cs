using Microsoft.AspNetCore.Mvc;
using SecondAssignment.Application.Contracts;
using SecondAssignment.Application.Dtos;

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
              var result = await _genreService.Save(saveGenresDtos);
                if (!result.IsSucces)
                {
                    return View(result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
                return View();
            }
           
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(UpdateGenresDtos updateGenresDtos)
        {
            try
            {
                var result = await _genreService.Update(updateGenresDtos);
                if (!result.IsSucces)
                {
                    return View(result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
                    return View(result.Message);
                }
                return View(result.Data);
            }
            catch
            {
                return View();
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
                    return View(result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
