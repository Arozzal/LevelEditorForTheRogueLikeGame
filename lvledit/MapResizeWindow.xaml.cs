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
using System.Windows.Shapes;

namespace lvledit
{

    public partial class MapResizeWindow : Window
    {

        public int xsize { get; set; } = 0;
        public int ysize { get; set; } = 0;

        public int Xvalue { get; set; }
        public int Yvalue { get; set; }

        public bool isGood { get; set; } = false;

        public MapResizeWindow()
        {
            InitializeComponent();
            Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Okbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tempxsize = int.Parse(mapxsize.Text);
                int tempysize = int.Parse(mapysize.Text);

                xsize = tempxsize;
                ysize = tempysize;
                isGood = true;

                Hide();
            }
            catch
            {
                MessageBox.Show("Incorrect Values!\nThe maximal Values are 256!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isGood = false;
            }
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            isGood = false;
            Hide();
        }

        private void Window_update(object sender, DependencyPropertyChangedEventArgs e)
        {
            xvalue.Content = Xvalue;
            yvalue.Content = Yvalue;
        }

    }
}
