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
          newArtist.Save(); //must save to database  for getAll method to grab it

            Genre thisGenre = Genre.Find(Int32.Parse(Request.Form["artist-genre"]));

             //this will add the newArtist created from the form to the genres_artists table so Genre.GetArtists can grab its artists
             thisGenre.AddArtist(newArtist);

             //returns name of page and variable
             return RedirectToAction("AllArtists");

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
