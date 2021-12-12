using LangSchool.AppDataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace LangSchool.controls
{
    /// <summary>
    /// Логика взаимодействия для TagControl.xaml
    /// </summary>
    public partial class TagControl : UserControl, INotifyPropertyChanged
    {
        string _cornerRarius = "8";

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(string), typeof(TagControl));
     
        public string CornerRadius
        {
            //get => _cornerRarius;
            //set
            //{
            //    _cornerRarius = value;
            //    OnPropertyChanged("CornerRadius");
            //}
            get
            {
                return this.GetValue(CornerRadiusProperty) as string;
            }
            set
            {
                this.SetValue(CornerRadiusProperty, value);
                OnPropertyChanged("CornerRadius");
            }
        }

        string _color = "#00FFFFFF";

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(string), typeof(TagControl));
        public string Color {

            //get => _color;
            //set
            //{
            //    _color = value;
            //    OnPropertyChanged("Color");
            //
            //}

            get
            {
                return this.GetValue(ColorProperty) as string;
            }
            set
            {
                this.SetValue(ColorProperty, value);
                OnPropertyChanged("Color");
            }
        }

        string _title;

        public static readonly DependencyProperty TitleProperty =
           DependencyProperty.Register("Title", typeof(string), typeof(TagControl));
        public string Title {
            //get => _title;
            //set
            //{
            //    _title = value;
            //    OnPropertyChanged("Title");
            //    
            //}

            get
            {
                return this.GetValue(TitleProperty) as string;
            }
            set
            {
                this.SetValue(TitleProperty, value);
                OnPropertyChanged("Title");
            }
        }

        public static readonly DependencyProperty TagIdProperty =
           DependencyProperty.Register("TagId", typeof(int), typeof(TagControl));
        public int TagId {
            get
            {
                return (int)this.GetValue(TagIdProperty);
            }
            set
            {
                this.SetValue(TagIdProperty, value);
                OnPropertyChanged("TagId");
            }
        }

        public static readonly DependencyProperty ClientIdProperty =
           DependencyProperty.Register("ClientId", typeof(int), typeof(TagControl));
        public int ClientId {
            get
            {
                return (int)this.GetValue(ClientIdProperty);
            }
            set
            {
                this.SetValue(ClientIdProperty, value);
                OnPropertyChanged("ClientId");
            }
        }
        
        public TagControl()
        {
            InitializeComponent();    
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ConnectionDB.ObjectDB.Client.Find(ClientId).Tag.Remove(ConnectionDB.ObjectDB.Tag.Find(TagId));
        }
    }
}
