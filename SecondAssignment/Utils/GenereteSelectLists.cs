using Microsoft.AspNetCore.Mvc.Rendering;
using SecondAssignment.Application.Contracts;
using SecondAssignment.Application.Core;
using SecondAssignment.Application.Models;
using SecondAssignment.Application.Services;
using SecondAssignment.WepApp.Models;

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

            AllProducerAvaileble.Add(new SelectListItem { Value = null, Selected = true, Text = "Select producer" });

            List<SelectListItem>? AllGenreAvaileble = genreService.GetAll().Result.Data?.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.Name,
            }).ToList();

            AllGenreAvaileble.Add(new SelectListItem { Value = null, Selected = true, Text = "Select genre" });

            List<SelectListItem>? AllGenreAvaileble2 = genreService.GetAll().Result.Data?.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.Name,
            }).ToList();


            AllGenreAvaileble2.Add(new SelectListItem { Value = null, Selected = true, Text = "Select genre" });
            Dictionary<int, List<SelectListItem>> SelectList = new()
            {
                {1, AllProducerAvaileble },
                {2,AllGenreAvaileble },
                {3,AllGenreAvaileble2},
            };
            return  SelectList;
        }

        public Dictionary<int, List<SelectListItem>> GenereteSelectList(SeriesModel seriesModel, IProducerService producerService, IGenreService genreService)
        {
            List<SelectListItem>? AllProducerAvaileble = producerService.GetAll().Result.Data?.Select(p => new SelectListItem
            {
                Value = p.ProducersId.ToString(),
                Text = p.Name.ToString(),
            }).ToList();

           if(AllProducerAvaileble.Any()) AllProducerAvaileble.Find(p => p.Value == seriesModel.Producer.ProducersId.ToString()).Selected = true;

  
            List<SelectListItem>? AllGenreAvaileble = genreService.GetAll().Result.Data?.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.Name,
            }).ToList();

            if(AllGenreAvaileble.Any()) AllGenreAvaileble.Find(p => p.Value == seriesModel.PrimaryGenre.GenreId.ToString()).Selected = true;
         
            List<SelectListItem>? AllGenreAvaileble2 = genreService.GetAll().Result.Data?.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.Name,
            }).ToList();

            if (AllGenreAvaileble2.Any()) AllGenreAvaileble2.Find(p => p.Value == seriesModel.SecundaryGenre.GenreId.ToString()).Selected = true;

        
            Dictionary<int, List<SelectListItem>> SelectList = new()
            {
                {1, AllProducerAvaileble! },
                {2,AllGenreAvaileble! },
                {3,AllGenreAvaileble2! },
            };
            return SelectList;
        }


        public Dictionary<int, List<CheckBoxOption>> GenereteCheckBoxList(IProducerService producerService, IGenreService genreService)
        {
            List<CheckBoxOption>? AllProducerAvaileble = producerService.GetAll().Result.Data?.Select(p => new CheckBoxOption
            {
                Id = p.ProducersId.ToString(),
                IsSelected = false,
                Name = p.Name,  
           
            }).ToList();


            List<CheckBoxOption>? AllGenreAvaileble = genreService.GetAll().Result.Data?.Select(g => new CheckBoxOption
            {
                Id = g.GenreId.ToString(),
                IsSelected = false,
                Name = g.Name,

            }).ToList();

       

            Dictionary<int, List<CheckBoxOption>> SelectList = new()
            {
                {1, AllProducerAvaileble! },
                {2,AllGenreAvaileble! },
                
            };
            return SelectList;
        }

    }
}
