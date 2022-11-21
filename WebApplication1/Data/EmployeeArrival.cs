﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    public class EmployeeArrival
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
