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

namespace lvledit
{
    /// <summary>
    /// Interaktionslogik für FileOperationWindow.xaml
    /// </summary>
    public partial class FileOperationWindow : Window
    {
        private string Path;

        public string path 
        {
            get { return Path; }
            set { Path = value; }
        }

        public FileOperationWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            path = LoadTextBox.Text;
            Hide();
        }
    }
}
