using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project_Management_System.Models;

namespace Final_Project_Management_System.ViewModels
{
    public class UserBoardsListsEvents
    {
        public User user { get; set; }
        public IEnumerable<Board> SideMenuBoards { get; set; }
        public Board board { get; set; }
        public IEnumerable<List> Lists { get; set; }
        public IEnumerable<Event> Events { get; set; }

    }
}