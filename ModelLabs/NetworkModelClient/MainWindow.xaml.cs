using FTN.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkModelClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientGDA clientGDA = new ClientGDA();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGetAllResources_Click(object sender, RoutedEventArgs e)
        {
            GetAllResources();
        }

        private void GetAllResources()
        {
            try
            {
                List<ResourceDescription> resources = clientGDA.GetAllResources();
                listViewResources.ItemsSource = resources;
            }
            catch (Exception ex)
            {
                string message = string.Format("GetAllResources failed. {0}", ex.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
        }
    }
}
