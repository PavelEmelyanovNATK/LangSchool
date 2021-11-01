using LangSchool.AppDataBase;
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

namespace LangSchool.Windows
{
    /// <summary>
    /// Логика взаимодействия для ClientVisitsWindow.xaml
    /// </summary>
    public partial class ClientVisitsWindow : Window
    {
        int clientID;
        public ClientVisitsWindow()
        {
            InitializeComponent();
        }

        public ClientVisitsWindow(int ClientID) : this()
        {
            clientID = ClientID;

            var Client = ConnectionDB.ObjectDB.Client.ToList().Find(x => x.ID == clientID);
            tbcFIO.Text = Client.LastName + " " + Client.FirstName + " " + Client.Patronymic;
            lvVisits.ItemsSource = Client.ClientService.ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
