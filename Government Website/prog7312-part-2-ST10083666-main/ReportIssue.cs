using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POEPART1_ST10083666
{
    public class ReportIssue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaPath { get; set; }
        public ReportIssue Next { get; set; }
        public ReportIssue Previous { get; set; }

        public ReportIssue(string location, string category, string description, string mediaPath)
        {
            Location = location;
            Category = category;
            Description = description;
            MediaPath = mediaPath;
            Next = null;
            Previous = null;
        }
    }
}
