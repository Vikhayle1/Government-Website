using System.Threading.Tasks;
using System.Windows;

namespace PROG7312_POEPART1_ST10083666
{
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }


        //The following code was adapted from Dev Leader youtube
        //https://www.youtube.com/watch?v=QuczbW66ejw
        //DevLeader
        //https://www.youtube.com/@DevLeader
        public async Task ProgressBarAsync()
        {

            ProgressBar.Value = 0;
            for (int i = 0; i <= 100; i += 10)
            {
                
                await Task.Delay(300);  
                ProgressBar.Value = i;
            }

            
            MessageBox.Show("Thank you for making a difference 👌!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
