using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PROG7312_POEPART1_ST10083666
{
    /// <summary>
    /// Interaction logic for DisplayReport.xaml
    /// </summary>
    public partial class DisplayReport : Window
    {
        //The following code was adapted from Kampa Plays on youtube
        //https://www.youtube.com/watch?v=2HP1Ig5HEgs
        //KampaPlays
        //https://www.youtube.com/@KampaPlays
        public DisplayReport(List<ReportIssue> issues)
        {
            InitializeComponent();
            //Displays the reports submitted 
            foreach (var issue in issues)
            {
                IssuesListBox.Items.Add($"Location: {issue.Location}, Category: {issue.Category}, Description: {issue.Description}, Media: {issue.MediaPath}");
            }
        }
    }
}
