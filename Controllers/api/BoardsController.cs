using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Final_Project_Management_System.Dtos;
using Final_Project_Management_System.Models;

namespace Final_Project_Management_System.Controllers.api
{
    public class BoardsController : ApiController
    {
        private ApplicationDbContext _context;

        public BoardsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetBoards()
        {
            return Ok(_context.Boards.ToList().Select(Mapper.Map<Board, BoardDto>));
        }

        public IHttpActionResult GetBoard(int id)
        {
            var board = _context.Boards.SingleOrDefault(b => b.Id == id);
            if (board == null)
                return NotFound();
            return Ok(Mapper.Map<Board, BoardDto>(board));
        }
        [HttpPost]
        public IHttpActionResult CreateBoard(BoardDto boardDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var board = Mapper.Map<BoardDto, Board>(boardDto);
            _context.Boards.Add(board);
            _context.SaveChanges();

            return Json(new { Id = board.Id });
        }

        [HttpPut]
        public void UpdateBoard(BoardDto boardDto, int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var boardInDb = _context.Boards.SingleOrDefault(b => b.Id == id);
            if (boardInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(boardDto, boardInDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void deleteBoard(int id)
        {
            var boardInDb = _context.Boards.SingleOrDefault(b => b.Id == id);
            if (boardInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var lists = _context.Lists.Where(l => l.BoardId == id);

            _context.Boards.Remove(boardInDb);
            foreach (var obj in lists)
            {
                _context.Lists.Remove(obj);
                var events = _context.Events.Where(e => e.ListId == obj.Id);
                foreach (var obj2 in events)
                {
                    _context.Events.Remove(obj2);
                }
            }
            _context.SaveChanges();
        }
    }
}
