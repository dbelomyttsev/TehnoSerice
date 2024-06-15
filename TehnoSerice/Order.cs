﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TehnoSerice
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int ProblemId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public int WorkerId { get; set; }
    }
}
