using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
   
    public partial class ReportIssuePage : Window
    {
        public LinkedList linkedList;
        public string mediaPath;
        public ReportIssuePage()
        {
            InitializeComponent();
            linkedList = new LinkedList();

            //Checks inputs in these fields  so the % of the progress bar for submit fills
            LocationInput.TextChanged += CheckFormCompletion;
            CategorySelection.SelectionChanged += CheckFormCompletion;
            DescriptionBox.TextChanged += CheckFormCompletion;

        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            //input values taken from the form
            string location = LocationInput.Text;
            string category = (CategorySelection.SelectedItem as ComboBoxItem)?.Content.ToString();
            string description = new TextRange(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd).Text.Trim();

            //Checks if fields are filled 
            if (string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(description) )
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            linkedList.AddIssue(location, category, description, mediaPath);

            MessageBox.Show("Issue reported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

           
            LocationInput.Clear();
            DescriptionBox.Document.Blocks.Clear();
            mediaPath = string.Empty;

            //When the user successfully enters all information it will open the loading screen for the report to be sent.
            ProgressWindow progressWindow = new ProgressWindow();
            progressWindow.Show();
            await progressWindow.ProgressBarAsync();
            progressWindow.Close();
           
            //Resets the validation after submitting
            CheckFormCompletion(null, null);
        }
        private void ShowReportedIssues_Click(object sender, RoutedEventArgs e)
        {
            List<ReportIssue> allIssues = linkedList.GetAllIssues();

            if (allIssues.Count == 0)
            {
                MessageBox.Show("No issues reported yet.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DisplayReport displayreport = new DisplayReport(allIssues);
            displayreport.Show();
        }

        //The following code was adapted from c-sharpcorner 
        //https://www.c-sharpcorner.com/UploadFile/mahesh/media-player-in-wpf/
        //Mahesh Chand
        //https://www.c-sharpcorner.com/members/mahesh-chand
        private void AttachMedia_Click(object sender, RoutedEventArgs e)
        {
            //Opens file explorer to select images
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                mediaPath = openFileDialog.FileName;
                MessageBox.Show("Media attached successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }



        //The following code was adapted from sachintha81 github
        //https://gist.github.com/sachintha81/8b441d138f0b2540cc65f088fe005851
        //sachintha81
        //https://gist.github.com/sachintha81
     
        private void CheckFormCompletion(object sender, EventArgs e)
        {
           
            string location = LocationInput.Text;
            string category = (CategorySelection.SelectedItem as ComboBoxItem)?.Content.ToString();
            string description = new TextRange(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd).Text.Trim();

           
            //Counts the number of fields that are filled
            int filledFields = 0;
            if (!string.IsNullOrWhiteSpace(location)) filledFields++;
            if (!string.IsNullOrWhiteSpace(category)) filledFields++;
            if (!string.IsNullOrWhiteSpace(description)) filledFields++;
            

           //Calculates the % on the fields filled
            int progress = Convert.ToInt32((filledFields / 3.0) * 100);

            //If form is bellow 100 submit button will be red
            if (progress < 100)
            {
                SubmitButton.Background = new SolidColorBrush(Colors.Red);
                SubmitButton.IsEnabled = true;
                byte redIntensity = (byte)(255 * (progress / 100));
                SubmitButton.Content = $"Progress: {progress}%";
            }
            //If form is filled completly submit button turns green
            if (progress == 100)
            {
                
                SubmitButton.IsEnabled = true;
                SubmitButton.Background = new SolidColorBrush(Colors.Green);
                SubmitButton.Content = "Submit";
            }
            
        }


    }
}
