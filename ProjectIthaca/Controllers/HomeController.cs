using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectIthaca.Models;
using ProjectIthaca;
using System;

namespace ProjectIthaca.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View(Genre.GetAll());
        }

        //Genre Controllers

        [HttpGet("/genres/new")]
        public ActionResult NewGenreForm()
        {
            return View();
        }

        [HttpPost("/genres")]
        public ActionResult CreateGenre()
        {
            Genre newGenre = new Genre
            (Request.Form["genre-name"],
             Request.Form["genre-description"],
             Request.Form["genre-era"]);

             newGenre.Save(); //must save to database for getAll method to grab it

             List<Genre> allGenres = Genre.GetAll();

            return View("Index", allGenres);
        }

        [HttpPost("/genres/delete")]
        public ActionResult RemoveAllGenres()
        {
            Genre.DeleteAll();
            return View(); //if not defined, attempts to map method name to a page w/ the same name
        }

        [HttpGet("/genres/{id}")]
        public ActionResult GenreDetails(int id)
        {
            Genre genre = Genre.Find(id);

            return View(genre);
        }

        //Artist Controllers

        [HttpGet("/artists/new")]
        public ActionResult NewArtistForm()
        {

            List<Genre> allGenres = Genre.GetAll(); //NEW LINE!

            return View(allGenres);
        }

        [HttpPost("/artists")]
        public ActionResult CreateArtist()
        {
          Artist tempArtist = new Artist("", "", "", false);
          bool artistActivity = tempArtist.IsActive(Request.Form["artist-active"]);

          Artist newArtist = new Artist
          (Request.Form["artist-name"], Request.Form["artist-debut"], Request.Form["artist-bio"], artistActivity);
          newArtist.Save(); //must save to database for getAll method to grab it

          //saves all checked values into a string
          IEnumerable<String> checkedGenres = Request.Form["artist-genre"];

          foreach(var genre in checkedGenres)
          {
            Genre thisGenre = Genre.Find(Int32.Parse(genre));
            thisGenre.AddArtist(newArtist);
          }

          return RedirectToAction("AllArtists", Artist.GetAll());
          /*Because Artist.GetAll() is a list, the model that the AllArtists page now has
          access to must have .Count method directly called on it (See AllArtists for reference)*/
        }

        [HttpPost("/artists/delete")]
        public ActionResult RemoveAllArtists()
        {
            Artist.DeleteAll();
            return View(); //if not defined, attempts to map method name to a page w/ the same name
        }

        [HttpGet("/artists/{id}")]
        public ActionResult ArtistDetails(int id)
        {
            Artist artist = Artist.Find(id);
            return View(artist);
        }

        [HttpGet("/artists")]
        public ActionResult AllArtists()
        {
            return View(Artist.GetAll());
        }


    }
}
