using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POEPART1_ST10083666
{
    public class Event
    {
        public string Name { get; set; }
        public string Category { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; }


        public override string ToString()
        {
            return $"{Name} ({Category}) {Description} on {Date.ToShortDateString()}";
        }
    }
}