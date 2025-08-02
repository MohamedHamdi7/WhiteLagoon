using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entites;
using WhiteLagoon.Infrastructure.Data;

namespace HotelApplication.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public VillaController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }

        public IActionResult Index()
        {
            var villa = applicationDbContext.Villas.ToList();
            return View(villa);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa model)
        {
            if (model.Name == model.Description)  //Custom Model Validation
            {
                ModelState.AddModelError("Name", "The Name and Description cannot be the same.");
            }
            if (ModelState.IsValid) //Server Side Validation
            {
                applicationDbContext.Villas.Add(model);
                applicationDbContext.SaveChanges();
              return RedirectToAction("Index");
            }
            return View(model);
            
        }
    }
}
