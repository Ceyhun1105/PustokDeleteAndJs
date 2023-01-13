using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokImagesUploadTask.Models;
using PustokImagesUploadTask.ViewModels;

namespace PustokImagesUploadTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                FeaturedBooks = _context.Books.Where(x=>x.IsFeatured==true).Include(x=>x.Author).Include(x=>x.Category).Include(x=>x.BookImages).ToList(),
                NewBooks = _context.Books.Where(x=>x.IsNew==true).Include(x=>x.Author).Include(x=>x.Category).Include(x=>x.BookImages).ToList(),
                DiscountedBooks = _context.Books.Where(x=>x.DiscountPrice>0).Include(x=>x.Author).Include(x=>x.Category).Include(x=>x.BookImages).ToList(),
                Sliders= _context.Sliders.ToList(),
                Features= _context.Features.ToList()
            };
            return View(homeViewModel);
        }
        public IActionResult Detail(int id)
        {
            Book book = _context.Books.Include(x=>x.Author).Include(x=>x.Category).Include(x=>x.BookImages).FirstOrDefault(x => x.Id == id);
            return View(book);
        }










        public IActionResult SetSession(int id)
        {
            HttpContext.Session.SetString("Id", id.ToString());
            return Content("Added");
        }
        public IActionResult GetSession()
        {
           string userid= HttpContext.Session.GetString("Id");
            return Content(userid);
        }
        public IActionResult RemoveSession()
        {
            HttpContext.Session.Remove("Id");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult SetCookie(string phonenum)
        {
            HttpContext.Response.Cookies.Append("phone", phonenum);
            return Content("Added");
        }
        public IActionResult GetCookie()
        {
            string number = HttpContext.Request.Cookies["phone"];
            return Content(number);
        }



    }
}