using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_Management_System.Dtos
{
    public class BoardDto
    {
        public string Name { get; set; }
        public int NumOfLists { get; set; }
        public int UserId { get; set; }
        public string Background { get; set; }

    }
}