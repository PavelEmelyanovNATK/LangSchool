using LangSchool.AppDataBase;
using LangSchool.Models;
using LangSchool.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace LangSchool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer UpdateTimer;

        public MainWindow()
        {
            InitializeComponent();
            UpdateTimer = new DispatcherTimer();
            UpdateTimer.Tick += RefreshData;
            UpdateTimer.Interval = TimeSpan.FromSeconds(1);
            UpdateTimer.Start();
        }

        public void RefreshData(object sender, EventArgs e)
        {
            var AllItems = ConnectionDB.ObjectDB.Client.ToList();
            AllItems = AllItems.FindAll(x => (x.FirstName + " " + x.LastName + " " + x.Patronymic).Contains(tbFIOsearch.Text));
            AllItems = AllItems.FindAll(x => x.Email.Contains(tbEmailSearch.Text));
            AllItems = AllItems.FindAll(x => x.Phone.Contains(tbPhNumberSearch.Text));
            if(cbGender.SelectedIndex != 0) AllItems = AllItems.FindAll(x => x.Gender.Code == ((Gender)cbGender.SelectedItem).Code);

            ClientsListControl.AllClients = AllItems;

            int ElementsPerPage;
            if(int.TryParse(cbItemsOnPage.Text, out ElementsPerPage))
            {
                ClientsListControl.SetElemtsPerPage(ElementsPerPage);
            }
            else
            {
                ClientsListControl.SetElemtsPerPage(-1);
            }

            tbCurrentPage.Text = "" + (ClientsListControl.CurrentPageNumber + 1);
            tbPagesCount.Text = "" + ClientsListControl.PagesCount;
            AllIemsCount.Text = "" + ClientsListControl.AllClients.Count;

            lvClients.ItemsSource = ClientsListControl.CurrentPage;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ConnectionDB.ObjectDB = new LanguageSchoolEntities();
            ClientsListControl.Initialize();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void OpenAddClientWindow(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
        }

        

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditClientWindow editClientWindow = new EditClientWindow(((Client)lvClients.SelectedItem).ID);
            editClientWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Client selected = (Client)lvClients.SelectedItem;
            if (MessageBox.Show("Вы уверены, что хотите удалить данного клиента из системы?","Удаление клиента",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Client deltedClient = ConnectionDB.ObjectDB.Client.ToList().Find(x => x.ID == selected.ID);
                var clTosr = ConnectionDB.ObjectDB.ClientService.ToList().FindAll(x => x.ClientID == deltedClient.ID);
                if (clTosr.Count > 0)
                {
                    if (MessageBox.Show("В системе присутствует ифномация о посещениях данного клиента, вы хотите удалить эту информацию всместе с клиентом?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        ConnectionDB.ObjectDB.ClientService.RemoveRange(clTosr);
                    }
                    else
                        return;
                }
                ConnectionDB.ObjectDB.Client.Remove(deltedClient);
                ConnectionDB.ObjectDB.SaveChanges();
            }
            
        }

        private void btnVisits_Click(object sender, RoutedEventArgs e)
        {
            ClientVisitsWindow editClientWindow = new ClientVisitsWindow(((Client)lvClients.SelectedItem).ID);
            editClientWindow.ShowDialog();
        }

        private void nextClik(object sender, RoutedEventArgs e)
        {
            ClientsListControl.NextPage();
        }

        private void prevClick(object sender, RoutedEventArgs e)
        {
            ClientsListControl.PrevPage();
        }
    }
}
