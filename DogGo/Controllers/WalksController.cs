using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalksRepository _walksRepo;

        public WalksController(IWalksRepository walksRepository)
        {
            
            _walksRepo = walksRepository;
            
        }
        // GET: WalksController
        public ActionResult Index()
        {
            List<Walks> walks = _walksRepo.GetAllWalks();

            WalksDeleteView vm = new WalksDeleteView()
            {
                Walks = walks
            };
            return View(vm);
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DogWalksForm walks)
        {
            try
            {
                foreach (var value in walks.SelectedValues)
                {
                
                    _walksRepo.DeleteWalk(value);
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
