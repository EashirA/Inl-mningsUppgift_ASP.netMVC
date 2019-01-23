﻿using E_ShopMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_ShopMVC.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public RolesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        private readonly ApplicationDbContext _context;
        public RolesController() => _context = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            var role = new IdentityRole();
            return View(role);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            if (role == null) return RedirectToAction("Create");
            _context.Roles.Add(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}