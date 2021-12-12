using Classes;
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
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    /// 
   
    public partial class EditClientWindow : Window
    {
        class ClientToTag
        {
            int ClientID { get; set; }
            int TagID { get; set; }
            string Title { get; set; }
            string Color { get; set; }
            public ClientToTag(int clid, int tid, string title, string color)
            {
                ClientID = clid;
                TagID = tid;
                Title = title;
                Color = color;
            }
        }

        int clientID;

        public EditClientWindow()
        {
            InitializeComponent();
        }
        public EditClientWindow(int ClientID) : this()
        {
            clientID = ClientID;

            Client curClient = ConnectionDB.ObjectDB.Client.ToList().Find(x => x.ID == clientID);

            tbFirstName.Text = curClient.FirstName;

            tbLastName.Text = curClient.LastName;

            tbPatronymic.Text = curClient.Patronymic;

            if (curClient.Gender.Code == "м")
            {
                cbGender.SelectedIndex = 0;
            }
            else if (curClient.Gender.Code == "ж")
            {
                cbGender.SelectedIndex = 1;
            }

            tbEmail.Text = curClient.Email;

            tbPhNumber.Text = curClient.Phone;

            dpBirthday.SelectedDate = curClient.Birthday;

            List<ClientToTag> tags = new List<ClientToTag>();
            foreach(var tag in curClient.Tag)
            {
                tags.Add(new ClientToTag(curClient.ID, tag.ID, tag.Title, tag.Color));
            }

            Tags.ItemsSource = tags;
        }

        private bool checkForEmptyFields()
        {
            return
                tbFirstName.Text.Length > 0
                &&
                tbLastName.Text.Length > 0
                &&
                tbPhNumber.Text.Length > 0
                &&
                cbGender.SelectedItem != null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!checkForEmptyFields())
            {
                MessageBox.Show("Не все поля заполнены!", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Validators.IsEmail(tbEmail.Text))
            {
                MessageBox.Show("Неверный формат Email!", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {


                IEnumerable<Client> clients =  ConnectionDB.ObjectDB.Client.Where(x => x.ID == clientID).AsEnumerable().Select((x)=> 
                {
                    x.FirstName = tbFirstName.Text;

                    x.LastName = tbLastName.Text;

                    x.Patronymic = tbPatronymic.Text;

                    x.GenderCode = ((Gender)cbGender.SelectedItem).Code;

                    x.Email = tbEmail.Text;

                    x.Phone = tbPhNumber.Text;

                    x.Birthday = dpBirthday.SelectedDate;

                    x.RegistrationDate = DateTime.Now;

                    x.PhotoPath = null;
                   
                    return x;

                    
                });
                foreach (var client in clients)
                {
                    ConnectionDB.ObjectDB.Entry(client).State = System.Data.Entity.EntityState.Modified;
                }
                
                ConnectionDB.ObjectDB.SaveChanges();
                MessageBox.Show("Данные созранены", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void FIO_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsLetter(e.Text, 0);
        }

        private void tbPhNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text, 0);
        }
    }
}
