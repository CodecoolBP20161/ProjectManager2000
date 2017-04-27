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

namespace ProjectManager2000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Media.SoundPlayer buttonMusic = new System.Media.SoundPlayer(@"C:\Windows\Media\tada.wav");
            buttonMusic.PlayLooping();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] myList = new string[4];

            myList[0] = "One";
            myList[1] = "Two";
            myList[2] = "Three";
            myList[3] = "Four";

            foreach (String element in myList)
            {
                projekts.Items.Add(element);
            }          
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
