using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project_Management_System.Models;

namespace Final_Project_Management_System.ViewModels
{
    public class BoardBoards
    {
        public IEnumerable<Board> Boards { get; set; }
        public Board Board { get; set; }
        public User User { get; set; }

    }
}