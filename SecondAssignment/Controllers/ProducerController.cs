using Microsoft.AspNetCore.Mvc;
using SecondAssignment.Application.Contracts;

using SecondAssignment.Application.Dtos;

namespace SecondAssignment.WepApp.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducerController(IProducerService producerService)
        {
            _producerService = producerService;
        }
        // GET: ProducerController
        public async Task<IActionResult> Index()
        {
            try
            {
           var result = await _producerService.GetAll();
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


        // GET: ProducerController/Create
        public async Task<IActionResult> SaveProducer()
        {
            return View();
        }

        // POST: ProducerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProducer(SaveProducersDto saveProducersDto)
        {
            try
            {
                var result = await _producerService.Save(saveProducersDto);

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

        // GET: ProducerController/Edit/5
        public async Task<IActionResult> EditProducer(Guid id)
        {
            try
            {
                var result = await _producerService.Get(id);

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

        // POST: ProducerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProducer(UpdateProducersDto updateProducersDto)
        {
            try
            {
                var result = await _producerService.Update(updateProducersDto);

                if (!result.IsSucces)
                {
                    return View("EditProducer", result.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducerController/Delete/5
        public async Task<IActionResult> DeleteProducer(Guid id)
        {
            try
            {
                var result = await _producerService.Get(id);

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

        // POST: ProducerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProducer(Guid id, IFormCollection collection)
        {
            try
            {
                var result = await _producerService.Delete(id);
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
