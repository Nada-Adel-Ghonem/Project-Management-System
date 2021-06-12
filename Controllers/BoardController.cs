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
    public class BoardController : Controller
    {
        private ApplicationDbContext _context;

        public BoardController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public ActionResult AddBoard(Board board)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Boards.Add(board);
            _context.SaveChanges();

            return RedirectToAction("BoardView", "Board",new {id = board.Id});
        }
        
        public ActionResult BoardView(int id)
        {
            var boardvar = _context.Boards.SingleOrDefault(b => b.Id == id);
            var uservar = _context.Users.SingleOrDefault(u => u.Id == boardvar.UserId);
            var ListEventsModel = new UserBoardsListsEvents
            {   
                board = boardvar,
                user = uservar,
                SideMenuBoards = _context.Boards.Where(b=>b.UserId == uservar.Id),
                Lists = _context.Lists.Where(l => l.BoardId == id),
                Events = _context.Events.ToList()
            };
            return View(ListEventsModel);
        }
    }
}