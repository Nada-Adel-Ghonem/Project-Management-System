using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Final_Project_Management_System.Dtos;
using Final_Project_Management_System.Models;
using Microsoft.Ajax.Utilities;

namespace Final_Project_Management_System.Controllers.api
{
    public class EventsController : ApiController
    {
        private ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetEvents()
        {
            return Ok(_context.Events.ToList().Select(Mapper.Map<Event, EventDto>));
        }

        public IHttpActionResult GetEvents(int id)
        {
            var eventt = _context.Events.SingleOrDefault(e => e.Id == id);
            if (eventt == null)
                return NotFound();
            return Ok(Mapper.Map<Event, EventDto>(eventt));
        }

        [HttpPost]
        public IHttpActionResult CreateEvent(EventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var eventt = Mapper.Map<EventDto, Event>(eventDto);
            _context.Events.Add(eventt);
            _context.SaveChanges();

            return Json(new{Id = eventt.Id});
        }

        [HttpPut]
        public void UpdateEvent(EventDto eventDto, int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var eventInDb = _context.Events.SingleOrDefault(e => e.Id == id);

            if (eventInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(eventDto, eventInDb);

            _context.SaveChanges();
        }
        [HttpDelete]
        public void deleteEvent(int id)
        {
            var eventInDb = _context.Events.SingleOrDefault(e => e.Id == id);
            if(eventInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Events.Remove(eventInDb);
            _context.SaveChanges();
        }
    }
}
