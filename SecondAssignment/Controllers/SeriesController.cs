using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                    ViewBag.message = result.Message;
                    return View();
                }
                Dictionary<int, List<CheckBoxOption>> SelctedLists = genereteSelect.GenereteCheckBoxList(_producerService, _genreService);
                ViewBag.CheckBoxGenre = SelctedLists;
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
                
                GenereteSelectLists SelctedLists = genereteSelect;
                if (!ModelState.IsValid)
                {
                   
                    ViewBag.message = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;
                    return View(SelctedLists.GenereteSelectList(_producerService, _genreService));
                }


                var result = await _seriesService.Save(saveSeriesDto);

         
                if (!result.IsSucces)
                { 
                    ViewBag.message = result.Message;
                    return View(SelctedLists.GenereteSelectList(_producerService, _genreService));
                }
                return RedirectToAction(nameof(ManteningSeries));
            }
            catch
            {
                return Redirect("ManteningSeries");
            }
        }

        // GET: SeriesController/Edit/5
        public async Task<IActionResult> EditSeries(Guid id)
        {
            Result<SeriesModel> result = await _seriesService.Get(id);
            
            try
            { GenereteSelectLists SelctedLists = genereteSelect;
                if (!result.IsSucces)
                {

                    return View(SelctedLists.GenereteSelectList( _producerService, _genreService));
                }
                SelctedLists.GenereteSelectList(result.Data, _producerService, _genreService);
                return View(new EditSeriesModel { Series = result.Data, Selectedlists = SelctedLists.GenereteSelectList(result.Data, _producerService, _genreService) });
            }

            catch
            {
                return Redirect("ManteningSeries");
            }
            
        }

        // POST: SeriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSeries(UpdateSeriesDto updateSeriesDto)
        {
            try
            {             
                
                if (!ModelState.IsValid)
                {
                   
                    Result<SeriesModel> resultInner = await _seriesService.Get(updateSeriesDto.SeriesId);
                    GenereteSelectLists SelctedLists = genereteSelect;
                    ViewBag.message = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;
                    return View(new EditSeriesModel { Series = resultInner.Data, Selectedlists = SelctedLists.GenereteSelectList(resultInner.Data, _producerService, _genreService) });
                }
                
                var result = await _seriesService.Update(updateSeriesDto);
   
                if (!result.IsSucces)
                {
                    Result<SeriesModel> resultInner = await _seriesService.Get(updateSeriesDto.SeriesId);
                    GenereteSelectLists SelctedLists = genereteSelect;
                    ViewBag.message = result.Message;
                    return View(new EditSeriesModel { Series = resultInner.Data, Selectedlists = SelctedLists.GenereteSelectList(resultInner.Data, _producerService, _genreService) });
                }

            }
            catch
            {
                return Redirect("ManteningSeries");
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
                return Redirect("ManteningSeries");
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
                    return Redirect("ManteningSeries");
                }

                return RedirectToAction(nameof(ManteningSeries));
            }
            catch
            {
                return Redirect("ManteningSeries");
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

                Dictionary<int, List<CheckBoxOption>> SelctedLists = genereteSelect.GenereteCheckBoxList(_producerService, _genreService);
                ViewBag.CheckBoxGenre = SelctedLists;
                if (!result.IsSucces)
                {
                    ViewBag.error = result.Message;
                    return Redirect("Index");
                }
                return View("Index", newList);
            }
            catch
            {
                return View("Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterSeriesByGenre(Guid genre)
        {

            try
            {
                var result = await _seriesService.GetByGenreId(genre);
                Dictionary<int, List<CheckBoxOption>> SelctedLists = genereteSelect.GenereteCheckBoxList(_producerService, _genreService);
                ViewBag.CheckBoxGenre = SelctedLists;

                if (!result.IsSucces)
                {
                    ViewBag.error = result.Message;
                    return Redirect("Index");
                }
                return View("Index", result.Data);
            }
            catch
            {
                return View("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterSeriesByProducer(Guid producer)
        {
            try
            {
                var result = await _seriesService.GetByProducerId(producer);
                Dictionary<int, List<CheckBoxOption>> SelctedLists = genereteSelect.GenereteCheckBoxList(_producerService, _genreService);
                ViewBag.CheckBoxGenre = SelctedLists;

                if (!result.IsSucces)
                {
                    ViewBag.error = result.Message;
                    return Redirect("Index");
                }
                return View("Index", result.Data);
            }
            catch
            {
                return View("Index");
            }

        }
    }
}
