using Microsoft.AspNetCore.Mvc.Rendering;
using SecondAssignment.Application.Contracts;
using SecondAssignment.Application.Core;
using SecondAssignment.Application.Models;
using SecondAssignment.Application.Services;

namespace SecondAssignment.WepApp.Utils
{
    public class GenereteSelectLists
    {

        public GenereteSelectLists()
        {

        }

        public Dictionary<int,List<SelectListItem>> GenereteSelectList(IProducerService producerService, IGenreService genreService)
        {
            List < SelectListItem>? AllProducerAvaileble = producerService.GetAll().Result.Data?.Select(p => new SelectListItem
            {
                Value = p.ProducersId.ToString(),
                Text = p.Name.ToString(),
            }).ToList();


            List<SelectListItem>? AllGenreAvaileble = genreService.GetAll().Result.Data?.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.Name,
            }).ToList();

            List<SelectListItem>? AllGenreAvaileble2 = genreService.GetAll().Result.Data?.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.Name,
            }).ToList();

            Dictionary<int, List<SelectListItem>> SelectList = new()
            {
                {1, AllProducerAvaileble! },
                {2,AllGenreAvaileble! },
                {3,AllGenreAvaileble2! },
            };
            return  SelectList;
        }


    }
}
