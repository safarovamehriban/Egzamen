using Business.Exceptions;
using Business.Services.Abstract;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Egzamen.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {

        IPortfolioService _portfolioService;
        IWebHostEnvironment _env;
        public PortfolioController(IPortfolioService categoryService, IWebHostEnvironment env)
        {
            _portfolioService = categoryService;
            _env = env;
        }
        public IActionResult Index()
        {
            var categories = _portfolioService.GetAll();
            return View(categories);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _portfolioService.AddAsync(portfolio);
            }
            catch (DuplicateCategoryException ex)
            {

                ModelState.AddModelError("Title", ex.Message);
                return View();
            }
            return RedirectToAction("index");

        }


        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            try
            {
                _portfolioService.Delete(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();

            }


            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existCategory = _portfolioService.Get(x => x.Id == id);

            if (existCategory == null)
            {
                return NotFound();
            }

            return View(existCategory);
        }


        public IActionResult Update(int id)
        {
            var existCategory = _portfolioService.Get(x => x.Id == id);

            if (existCategory == null) return NotFound();

            return View(existCategory);
        }

        [HttpPost]
        public IActionResult Update(Portfolio newPortfolio)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _portfolioService.Update(newPortfolio.Id, newPortfolio);
            }
            catch (EntityNotFoundException ex)
            {

                return NotFound();
            }
            catch (DuplicateCategoryException ex)
            {
                ModelState.AddModelError("Title", ex.Message);
                return View();
            }

            return RedirectToAction("index");
        }


    }
}
