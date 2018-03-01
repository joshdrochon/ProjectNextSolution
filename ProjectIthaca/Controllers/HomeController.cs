// using System.Collections.Generic;
// using Microsoft.AspNetCore.Mvc;
// using ProjectIthaca.Models;
// using ProjectIthaca;
// using System;
//
// namespace ProjectIthaca.Controllers
// {
//     public class HomeController : Controller
//     {
//         [HttpGet("/")]
//         public ActionResult Index()
//         {
//             return View(Stylist.GetAll());
//         }
//
//         //Genre Controllers
//
//         [HttpGet("/stylists/new")]
//         public ActionResult NewStylistForm()
//         {
//             return View();
//         }
//
//         [HttpPost("/stylists")]
//         public ActionResult CreateStylist()
//         {
//             Stylist newStylist = new Stylist
//             (Request.Form["stylist-name"],
//              Request.Form["stylist-email"],
//              Request.Form["stylist-startdate"]);
//
//              newStylist.Save(); //must save to database  for getAll method to grab it
//
//              List<Stylist> allStylists = Stylist.GetAll();
//
//             return View("Index", allStylists);
//         }
//
//         [HttpPost("/stylists/delete")]
//         public ActionResult DeleteAllStylists()
//         {
//             Stylist.DeleteAll();
//             return View(); //if not defined, attempts to map method name to a page w/ the same name
//         }
//
//         [HttpGet("/stylists/{id}")]
//         public ActionResult StylistDetails(int id)
//         {
//             Stylist stylist = Stylist.Find(id);
//
//             return View(stylist);
//         }
//
//         //Artist Controllers
//
//         [HttpGet("/clients/new")]
//         public ActionResult NewClientForm()
//         {
//             List<Stylist> allStylists = Stylist.GetAll();
//
//             return View(allStylists);
//         }
//
//         [HttpPost("/clients")]
//         public ActionResult CreateClient()
//         {
//             Client newClient = new Client
//             (Request.Form["client-name"],
//              Request.Form["client-email"],
//              Request.Form["client-first-appt"],
//              int.Parse(Request.Form["client-stylist"]));
//
//              newClient.Save(); //must save to database  for getAll method to grab it
//
//              List<Client> allClients = Client.GetAll();
//
//              return View("AllClients", allClients);
//         }
//
//         [HttpPost("/clients/delete")]
//         public ActionResult DeleteAllClients()
//         {
//             Client.DeleteAll();
//             return View(); //if not defined, attempts to map method name to a page w/ the same name
//         }
//
//         [HttpGet("/clients/{id}")]
//         public ActionResult ClientDetails(int id)
//         {
//             Client client = Client.Find(id);
//             return View(client);
//         }
//
//         [HttpGet("/clients")]
//         public ActionResult AllClients()
//         {
//             return View(Client.GetAll());
//         }
//
//
//     }
// }
