using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_Management_System.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfLists { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Background { get; set; }

        public Board()
        {
            Background = "url('../../images/2.jpg')";
        }

    }
}