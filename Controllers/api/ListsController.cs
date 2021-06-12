using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using AutoMapper;
using Final_Project_Management_System.Dtos;
using Final_Project_Management_System.Models;

namespace Final_Project_Management_System.Controllers.api
{
    public class ListsController : ApiController
    {
        private ApplicationDbContext _context;

        public ListsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetLists()
        {
            return Ok(_context.Lists.ToList().Select(Mapper.Map<List, ListDto>));
        }

        public IHttpActionResult GetList(int id)
        {
            var list = _context.Lists.SingleOrDefault(l => l.Id == id);
            if (list == null)
                return NotFound();
            return Ok(Mapper.Map<List, ListDto>(list));
        }
        [HttpPost]
        public IHttpActionResult CreateList(ListDto listDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var list = Mapper.Map<ListDto, List>(listDto);
            _context.Lists.Add(list);
            _context.SaveChanges();

            return Json(new{Id = list.Id});
        }
       

        [HttpPut]
        public void UpdateList(ListDto listDto, int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var listInDb = _context.Lists.SingleOrDefault(l => l.Id == id);
            if(listInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(listDto, listInDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void deleteList(int id)
        {
            var listInDb = _context.Lists.SingleOrDefault(l => l.Id == id);
            if(listInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var events = _context.Events.Where(e => e.ListId == id);

            _context.Lists.Remove(listInDb);
            foreach (var obj in events)
            {
                _context.Events.Remove(obj);
            }
            _context.SaveChanges();
        }
    }
}
