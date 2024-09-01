using Microsoft.AspNetCore.Mvc;
using taskmvc.Models;
using taskmvc.Models.Data;
using System;
using System.Linq;

namespace taskmvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var checkUser = _context.Users
                .FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (checkUser != null)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(user);
        }

        public IActionResult Index()
        {
          
            var activeUsers = _context.Users.Where(u => u.IsActive).ToList();
            return View(activeUsers);
        }

        public IActionResult ToggleActive(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                
                if (!user.IsActive)
                {
                    _context.Users.Remove(user);
                }
                else
                {
                    
                    user.IsActive = false;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}


