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
    public partial class FileNewWindow : Window
    {
        public int terrainxsize { get; set; }
        public int terrainysize { get; set; }
        public byte blockId { get; set; }
        public bool correctvalues { get; set; } = false;

        public FileNewWindow()
        {
            InitializeComponent();
            Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
        }

        private void Okbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int terrainxsizetemp = int.Parse(xdatatextbox.Text);
                int terrainysizetemp = int.Parse(ydatatextbox.Text);
                byte tempblockId = byte.Parse(BlockID.Text);

                terrainxsize = terrainxsizetemp;
                terrainysize = terrainysizetemp;
                blockId = tempblockId;
                

                correctvalues = true;

                xdatatextbox.Text = "";
                ydatatextbox.Text = "";
                BlockID.Text = "";

                Hide();
            }

            catch
            {
                MessageBox.Show("Incorrect Values!\nThe maximal values are 256", "Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                correctvalues = false;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            correctvalues = false;
            xdatatextbox.Text = "";
            ydatatextbox.Text = "";
            BlockID.Text = "";
            Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            xdatatextbox.Text = "";
            ydatatextbox.Text = "";
            BlockID.Text = "";
            Hide();
        }
    }
}
