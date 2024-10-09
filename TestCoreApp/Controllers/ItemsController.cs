using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestCoreApp.Data;
using TestCoreApp.Models;

namespace TestCoreApp.Controllers
{
    public class ItemsController : Controller
    {

        private readonly AppDbContext _db;

		public ItemsController(AppDbContext db)
        {
            _db = db;
        }



        public IActionResult Index()
        {
            IEnumerable<Item> itemsList = _db.Items.Include(c => c.Category).ToList();
            return View(itemsList);
        }
        
        //get
        public IActionResult New()
        {
            CreateSelectedList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if (item.Name == "100")
                ModelState.AddModelError("Name", "Name Can't equal 100");

            if (ModelState.IsValid)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been added successfully";
                return RedirectToAction("Index");
            }
            return View();           
        }

        public void CreateSelectedList(int selectedId = 1)
        {
            //List<Category> categories = new List<Category>
            //{
            //    new Category(){Id = 0, Name = "Select Category"},
            //    new Category(){Id = 1, Name = "Computers"},
            //    new Category(){Id = 2, Name = "Mobiles"},
            //    new Category(){Id = 3, Name = "Electric Machines"}
            //};

            List<Category> categories = _db.Categories.ToList();
            SelectList listItems = new SelectList(categories,"Id" ,"Name", selectedId);
            ViewBag.CategoryList = listItems;
        }

        //get
        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
                return NotFound();

            var item = _db.Items.Find(Id);

            if (item == null)
                return NotFound();

            CreateSelectedList(item.CategoryId);
            
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (item.Name == "100")
                ModelState.AddModelError("Name", "Name Can't equal 100");

            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        //get
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();

            var item = _db.Items.Find(Id);

            if (item == null)
                return NotFound();

            CreateSelectedList(item.CategoryId);

            return View(item);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(Item item)
        //{
        //     _db.Items.Update(item);
        //     _db.SaveChanges();
        //     return RedirectToAction("Index");
        //}

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        {
            var item = _db.Items.Find(Id);
            if (item == null)
                return NotFound();
            _db.Items.Remove(item);
            _db.SaveChanges();
            TempData["successData"] = "Item has been deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
