using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_Management_System.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List List { get; set; }
        public int ListId { get; set; }

    }
}