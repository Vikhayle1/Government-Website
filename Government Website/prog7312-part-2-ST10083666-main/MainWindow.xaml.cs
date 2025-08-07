using System.Windows;

namespace PROG7312_POEPART1_ST10083666
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_localeventandannounce(object sender, RoutedEventArgs e)
        {
           LocalEventsPage localEventsPage = new LocalEventsPage();
            this.Visibility = Visibility.Hidden;
            localEventsPage.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReportIssuePage reportIssuePage = new ReportIssuePage();
            this.Visibility= Visibility.Hidden;
            reportIssuePage.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("System Offline");
        }
    }
}
