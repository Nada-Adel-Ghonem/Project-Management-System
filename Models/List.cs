using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_Management_System.Models
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfEvents { get; set; }
        public Board Board { get; set; }
        public int BoardId { get; set; }

    }
}