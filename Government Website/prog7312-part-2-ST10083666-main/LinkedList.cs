using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POEPART1_ST10083666
{
    public class LinkedList
    {

        //The following code was adapted from stackoverflow
        //https://stackoverflow.com/questions/3823848/creating-a-very-simple-linked-list
        //nan
        //https://stackoverflow.com/users/428789/nan

    public ReportIssue Head { get; set; }
        public ReportIssue Tail { get; set; }

        public LinkedList()
        {
            Head = null;
            Tail = null;
        }

        
        public void AddIssue(string location, string category, string description, string mediaPath)
        {
            ReportIssue newNode = new ReportIssue(location, category, description, mediaPath);

            if (Head == null) 
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Previous = Tail;
                Tail = newNode;
            }
        }

        
        public List<ReportIssue> GetAllIssues()
        {
            List<ReportIssue> issues = new List<ReportIssue>();
            ReportIssue current = Head;
            while (current != null)
            {
                issues.Add(current);
                current = current.Next;
            }
            return issues;
        }
    }

}
