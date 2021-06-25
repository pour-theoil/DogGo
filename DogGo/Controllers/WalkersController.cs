using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalksRepository _walksRepo;
        private readonly IDogRepository _dogRepe;
        private readonly IOwnerRepository _ownerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IOwnerRepository ownerRepository, IWalkerRepository walkerRepository, IDogRepository dogRepository, IWalksRepository walksRepository)
        {
            _walkerRepo = walkerRepository;
            _walksRepo = walksRepository;
            _dogRepe = dogRepository;
            _ownerRepo = ownerRepository;
        }

        // GET: WalkersController
        // GET: Walkers
        public ActionResult Index()
        {
            List<Walker> walkers = new List<Walker>();
            try
            {
                int ownerId = GetCurrentUserId();
                Owner owner = _ownerRepo.GetOwnerById(ownerId);
                walkers = _walkerRepo.GetWalkersInNeighborhood(owner.Neighborhood.Id);
            }
            catch (Exception ex)
            {
                walkers = _walkerRepo.GetAllWalkers();
            }



            return View(walkers);
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walksRepo.GetAllWalksByWalkerId(id);
            int totalminutes = _walksRepo.GetWalkerTime(id);

            Totaltime totalwalks = new Totaltime()
            {
                Hours = totalminutes / 60,
                Minutes = totalminutes % 3600
            };
            WalkerViewModel vm = new WalkerViewModel()
            {
                Walker = walker,
                Walks = walks,
                Totaltime = totalwalks

            };

            if (walker == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {

            List<Walker> walkers = _walkerRepo.GetAllWalkers();
            List<Dog> dogs = _dogRepe.GetAll();

            DogWalksForm vm = new DogWalksForm()
            {
                Walk = new Walks(),
                Dogs = dogs,
                Walkers = walkers
            };
            return View(vm);
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DogWalksForm dogwalk)
        {
            try
            {
                foreach (var value in dogwalk.SelectedValues)
                {
                    dogwalk.Walk.DogId = value;
                    _walksRepo.AddWalks(dogwalk.Walk);
                }


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
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

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
