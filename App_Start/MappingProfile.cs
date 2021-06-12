using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Final_Project_Management_System.Dtos;
using Final_Project_Management_System.Models;

namespace Final_Project_Management_System.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Event, EventDto>();
            Mapper.CreateMap<EventDto, Event>();

            Mapper.CreateMap<ListDto, List>();
            Mapper.CreateMap<List, ListDto>();

            Mapper.CreateMap<Board, BoardDto>();
            Mapper.CreateMap<BoardDto, Board>();
        }
    }
}