using FTN.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkModelClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ClientGDA clientGDA = new ClientGDA();
        private ResourceDescription _selectedResource;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public ResourceDescription SelectedResource
        {
            get { return _selectedResource; }
            set
            {
                _selectedResource = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedResource)));
            }
        }

        #region EventHandlers

        private void BtnGetAllResources_Click(object sender, RoutedEventArgs e)
        {
            GetAllResources();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string propertyValue = ((TextBlock)sender).Text;
            long resourceId = long.Parse(propertyValue);

            GetResource(resourceId);
        }

        #endregion EventHandlers

        #region GDAQueryService

        private void GetAllResources()
        {
            try
            {
                List<ResourceDescription> resources = clientGDA.GetAllResources();
                SelectedResource = resources.Count > 0 ? resources[0] : null;
                listViewResources.ItemsSource = resources;
            }
            catch (Exception ex)
            {
                string message = string.Format("GetAllResources failed. {0}", ex.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
        }

        private void GetResource(long resourceId)
        {
            try
            {
                ResourceDescription resource = clientGDA.GetResource(resourceId);
                SelectedResource = resource;
            }
            catch (Exception ex)
            {
                string message = string.Format("GetResource failed. {0}", ex.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
        }

        #endregion GDAQueryService
    }
}
