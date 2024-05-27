using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SecondAssignment.Application.Contracts;
using SecondAssignment.Application.Core;
using SecondAssignment.Application.Dtos;
using SecondAssignment.Application.Models;
using SecondAssignment.WepApp.Models;
using SecondAssignment.WepApp.Utils;

namespace SecondAssignment.WepApp.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ISeriesService _seriesService;
        private readonly IProducerService _producerService;
        private readonly IGenreService _genreService;
        private readonly GenereteSelectLists genereteSelect;

        public SeriesController(ISeriesService seriesService, IProducerService producerService, IGenreService genreService, GenereteSelectLists genereteSelect)
        {
            _seriesService = seriesService;
            _producerService = producerService;
            _genreService = genreService;
            this.genereteSelect = genereteSelect;
        }
        // GET: SeriesController
        public async Task<IActionResult> Index()
        {
            Result<List<SeriesModel>> result = await _seriesService.GetAll();

            try
            {
                if (!result.IsSucces)
                {
                    return View();
                }

            }
            catch
            {

            }
            return View(result.Data);

        }

        public async Task<IActionResult> ManteningSeries()
        {
            Result<List<SeriesModel>> result = await _seriesService.GetAll();

            try
            {
                if (!result.IsSucces)
                {
                    return View();
                }

            }
            catch
            {

            }
            return View(result.Data);

        }

        // GET: SeriesController/Details/5
        public async Task<IActionResult> SpecificSeries(Guid id)
        {
            Result<SeriesModel> result = await _seriesService.Get(id);

            try
            {

                if (!result.IsSucces || result.Data is null)
                {
                    return View();
                }

            }
            catch
            {

            }
            return View(result.Data);

        }


        // GET: SeriesController/Create
        public async Task<IActionResult> SaveSeries()
        {
            Dictionary<int, List<SelectListItem>> SelctedLists = genereteSelect.GenereteSelectList(_producerService, _genreService);
            return View(SelctedLists);
        }

        // POST: SeriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSeries(SaveSeriesDto saveSeriesDto)
        {
            try
            {
                var result = await _seriesService.Save(saveSeriesDto);
                if (!result.IsSucces)
                {
                    return View();
                }
                return RedirectToAction(nameof(ManteningSeries));
            }
            catch
            {
                return View();
            }
        }

        // GET: SeriesController/Edit/5
        public async Task<IActionResult> EditSeries(Guid id)
        {
            Result<SeriesModel> result = await _seriesService.Get(id);
            Dictionary<int, List<SelectListItem>> SelctedLists = genereteSelect.GenereteSelectList(_producerService, _genreService);
            try
            {
                if (!result.IsSucces)
                {
                    return View();
                }

            }
            catch
            {

            }
            return View(new EditSeriesModel { Series = result.Data, Selectedlists = SelctedLists });
        }

        // POST: SeriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSeries(UpdateSeriesDto updateSeriesDto)
        {
            try
            {
                var result = await _seriesService.Update(updateSeriesDto);
                if (!result.IsSucces)
                {
                    ViewBag.error = result.Message;
                    return Redirect("EditSeries");
                }

            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(ManteningSeries));
        }

        // GET: SeriesController/Delete/5
        public async Task<IActionResult> DeleteSeries(Guid id)
        {


            try
            {
                Result<SeriesModel> result = await _seriesService.Get(id);
                if (!result.IsSucces)
                {
                    return View();
                }
                return View(result.Data);
            }
            catch
            {
                return View();
            }

        }

        // POST: SeriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSeries(Guid id, IFormCollection collection)
        {
            try
            {
                var result = await _seriesService.Delete(id);

                if (!result.IsSucces)
                {
                    ViewBag.error = result.Message;
                    return Redirect("EditSeries");
                }

                return RedirectToAction(nameof(ManteningSeries));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterSeriesByName(string name)
        {
            try
            {
                var result = await _seriesService.GetSeriesByName(name);
                List<SeriesModel> newList = new()
                {
                    result.Data,
                };
                if (!result.IsSucces)
                {
                    ViewBag.error = result.Message;
                    return Redirect("Index");
                }
               return View("Index",newList);
            }
            catch
            {
                return View();
            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterSeriesByGenre(string name)
        {
            try
            {
                var result = await _seriesService.GetSeriesByName(name);
                List<SeriesModel> newList = new()
                {
                    result.Data,
                };
                if (!result.IsSucces)
                {
                    ViewBag.error = result.Message;
                    return Redirect("Index");
                }
                return View("Index", newList);
            }
            catch
            {
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterSeriesByProducer(string name)
        {
            try
            {
                var result = await _seriesService.GetSeriesByName(name);
                List<SeriesModel> newList = new()
                {
                    result.Data,
                };
                if (!result.IsSucces)
                {
                    ViewBag.error = result.Message;
                    return Redirect("Index");
                }
                return View("Index", newList);
            }
            catch
            {
                return View();
            }

        }
    }
}
