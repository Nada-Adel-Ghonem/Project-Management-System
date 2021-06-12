using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Final_Project_Management_System.Models;
using Final_Project_Management_System.ViewModels;

namespace Final_Project_Management_System.Controllers
{
    public class RegistrationController : Controller
    {
        private ApplicationDbContext _context;

        public RegistrationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult LogIn(User user)
        {

            foreach (var person in _context.Users)
            {
                if (user.E_mail == person.E_mail && user.Password == person.Password)
                {
                    return RedirectToAction("GetBoards", "Registration",new{ id = person.Id});
                }
            }

            return View("Index");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("GetBoards", "Registration", new { id = user.Id });
        }
        public ActionResult UpdateUser(int userId, int BoardId)
        {
            var viewmodel = new UserBoard
            {
                User = _context.Users.Single(u => u.Id == userId),
                BoardId = BoardId
            };
            return View(viewmodel);
        }

        public ActionResult SaveUserAfterUpdate(UserBoard userBoard)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == userBoard.User.Id);

            userInDb.Name = userBoard.User.Name;
            userInDb.E_mail = userBoard.User.E_mail;
            userInDb.Password = userBoard.User.Password;
            userInDb.ConfirmPassword = userBoard.User.ConfirmPassword;
            userInDb.NumOfBoards = userBoard.User.NumOfBoards;

            _context.SaveChanges();

            return RedirectToAction("BoardView", "Board", new { id = userBoard.BoardId });
        }

        public ActionResult registerView()
        {
            return View();
        }

        public ActionResult GetBoards(int id)
        {
            var BoardBoards = new BoardBoards
            {
                Boards = _context.Boards.Where(b => b.UserId == id),
                Board = new Board() { UserId = id },
                User = _context.Users.SingleOrDefault(u => u.Id == id)
            };
            return View(BoardBoards);
        }
    }
}