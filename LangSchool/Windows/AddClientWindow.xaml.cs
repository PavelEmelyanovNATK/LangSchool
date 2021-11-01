using LangSchool.AppDataBase;
using Classes;
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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {

        public AddClientWindow()
        {
            InitializeComponent();
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
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
                Client newClietn = new Client()
                {
                    FirstName = tbFirstName.Text
                    ,
                    LastName = tbLastName.Text
                    ,
                    Patronymic = tbPatronymic.Text
                    ,
                    GenderCode = ((Gender)cbGender.SelectedItem).Code
                    ,
                    Email = tbEmail.Text
                    ,
                    Phone = tbPhNumber.Text
                    ,
                    Birthday = dpBirthday.SelectedDate
                    ,
                    RegistrationDate = DateTime.Now
                    , 
                    PhotoPath = null
                };

                ConnectionDB.ObjectDB.Client.Add(newClietn);
                ConnectionDB.ObjectDB.SaveChanges();
                MessageBox.Show("Данные добавлены", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
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
            e.Handled = !Char.IsLetter(e.Text,0);
        }

        private void tbPhNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text, 0);
        }
    }
}
