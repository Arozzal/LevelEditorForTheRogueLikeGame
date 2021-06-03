using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;



namespace lvledit
{

    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();
        byte colornum = 3;
        int blockposx = 1;
        int blockposy = 1;
        int blocksize = 32;
        string filepath = "";
        int Radius = 0;

        FileOperationWindow fileOperationWindow = new FileOperationWindow();
        FileNewWindow fileNewWindow = new FileNewWindow();
        MapResizeWindow mapResizeWindow = new MapResizeWindow();



        List<List<Image>> blockgrid = new List<List<Image>>();
        List<Image> transimglist = new List<Image>();

        Imagelist imglist = new Imagelist();
        Canvas cnvas = new Canvas();
        Canvas mousetrans = new Canvas();

        Microsoft.Win32.SaveFileDialog savefiledialog = new Microsoft.Win32.SaveFileDialog();
        Microsoft.Win32.OpenFileDialog openfiledialog = new Microsoft.Win32.OpenFileDialog();
        
        public MainWindow()
        {
            InitializeComponent();
            cnvas.MouseMove += (s, e) => {
                changeBlockClick(e);
            };
            cnvas.MouseDown += (s, e) => {
                changeBlockClick(e);
            };

            savefiledialog.FileName = "map.*.*.sean";
            savefiledialog.DefaultExt = ".sean";
            savefiledialog.Filter = "Sean Files (*.sean)|*.sean";

            openfiledialog.DefaultExt = ".sean";
            openfiledialog.Filter = "Sean Files (*.sean)|*.sean";
            
            Blockarea.Child = cnvas;
            TopBlockarea.Child = mousetrans;
            int diff = 0;
            for (byte i = 1; i < imglist.Lenght; i++)
            {
                if (imglist.isUsedId(i))
                {
                    Button tempimage = new Button();
                    tempimage.Width = 32;
                    tempimage.Height = 32;
                    tempimage.Content = new Image
                    {
                        Source = imglist[i],
                    };
                    tempimage.Tag = i;
                    tempimage.Click += UIBlockButtonClick;
                    UiCanvas.Children.Add(tempimage);
                    Canvas.SetTop(tempimage,2 * 32);
                    Canvas.SetLeft(tempimage, (i-diff) * 32);
                }

                else
                {
                   diff++;
                }
            }


        }
        private void changeBlockClick(MouseEventArgs e)
        {
            mousetrans.Children.Clear();
            int mouseposx = ((int)Mouse.GetPosition(cnvas).X - blockposx * blocksize - (int)Mouse.GetPosition(cnvas).X % blocksize) / blocksize;
            int mouseposy = ((int)Mouse.GetPosition(cnvas).Y - blockposy * blocksize - (int)Mouse.GetPosition(cnvas).Y % blocksize) / blocksize;
            Console.WriteLine(mouseposx + " " + mouseposy + "\n");

            for (int x = -Radius; x < Radius+1; x++)
            {
                for (int y = -Radius; y < Radius+1; y++)
                {
                    if (isInField(x, y, mouseposx, mouseposy))
                    {
                        if (e.RightButton == MouseButtonState.Pressed)
                        {
                            Maploader.setAt(mouseposx + x, mouseposy + y, 1);
                            update();
                        }
                        if (e.LeftButton == MouseButtonState.Pressed)
                        {

                            Maploader.setAt(mouseposx + x, mouseposy + y, colornum);
                            update();
                        }
                        Image tempimage = new Image();
                        tempimage.Width = blocksize;
                        tempimage.Height = blocksize;
                        tempimage.Source = imglist.transparimg;
                        mousetrans.Children.Add(tempimage);
                        Canvas.SetTop(tempimage, (mouseposy + x + blockposy) * blocksize);
                        Canvas.SetLeft(tempimage, (mouseposx + y + blockposx) * blocksize);
                    }
                }
            }
        }

        private bool isInField(int posx, int posy, int mx, int my)
        {
            if (Math.Sqrt((posx * posx) + (posy * posy)) <= Radius) {
                return true;
            }
            return false;
        }



        private void UIBlockButtonClick(object sender, RoutedEventArgs e)
        {
            Button tempbtn = (Button)sender;
            Console.WriteLine(tempbtn.Tag);
            colornum = (byte)tempbtn.Tag;
        }


        private void create()
        {
            cnvas.Children.Clear();
            byte[,] mapints = Maploader.getList();
            for (int y = 0; y < Maploader.getMapYSize(); y++)
            {
                blockgrid.Add(new List<Image>());
                for (int x = 0; x < Maploader.getMapXSize(); x++)
                {
                  Image tempimage = new Image();
                  tempimage.Tag = mapints[x, y];
                  tempimage.Width = blocksize;
                  tempimage.Height = blocksize;
                  tempimage.Source = imglist[mapints[x, y]];
                  cnvas.Children.Add(tempimage);
                  Canvas.SetTop(tempimage, blockposy * blocksize + y * blocksize);
                  Canvas.SetLeft(tempimage, blockposx * blocksize + x * blocksize);
                  blockgrid[y].Insert(x, tempimage);
                }
            }
        }


        private void update()
        {
            byte[,] mapints = Maploader.getList();
            for (int y = 0; y <= Maploader.getMapYSize() - 1; y++)
            {
                for (int x = 0; x <= Maploader.getMapXSize() - 1; x++)
                {
                    if ((byte)blockgrid[y][x].Tag != mapints[x, y])
                    {
                        cnvas.Children.Remove(blockgrid[y][x]);
                        blockgrid[y].RemoveAt(x);
                        
                        Image tempimage = new Image();
                        tempimage.Width = blocksize;
                        tempimage.Height = blocksize;
                        tempimage.Tag = mapints[x, y];
                        tempimage.Source = imglist[mapints[x, y]];
                        cnvas.Children.Add(tempimage);
                        Canvas.SetTop(tempimage, blockposy * blocksize + y * blocksize);
                        Canvas.SetLeft(tempimage, blockposx * blocksize + x * blocksize);
                        blockgrid[y].Insert(x, tempimage);
                    }
                }
            }
        }

        private void moveupdate()
        {
            for (int y = 0; y <= Maploader.getMapYSize() - 1; y++)
            {
                for (int x = 0; x <= Maploader.getMapXSize() - 1; x++)
                {
                    Canvas.SetTop(blockgrid[y][x], blockposy * blocksize + y * blocksize);
                    Canvas.SetLeft(blockgrid[y][x], blockposx * blocksize + x * blocksize);
                }
            }
        }



        private void MenuFileOpen(object sender, RoutedEventArgs e)
        {
            bool? openfileresult = openfiledialog.ShowDialog();
            if (openfileresult == true)
            {
                string filename = openfiledialog.FileName;
                filepath = filename;
                Maploader.load(filename);
                create();
            }
            Title = filepath;

        }

        private void MenuFileSave(object sender, RoutedEventArgs e)
        {
            if (filepath != "")
            {
                Maploader.save(filepath);
            }else
            {
                bool? result = savefiledialog.ShowDialog();
                if(result == true)
                {
                    filepath = savefiledialog.FileName;
                    Maploader.save(filepath);
                }
            }
            Title = filepath;
        }

        private void MenuFileNew(object sender, RoutedEventArgs e)
        {
            fileNewWindow.ShowDialog();
            if (fileNewWindow.correctvalues)
            {
                filepath = "";
                Maploader.createnew((byte)fileNewWindow.terrainxsize, (byte)fileNewWindow.terrainysize, fileNewWindow.blockId);
                create();
            }
            Title = "New File";
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mapResizeWindow.Xvalue = Maploader.getMapXSize();
            mapResizeWindow.Yvalue = Maploader.getMapYSize();
            mapResizeWindow.ShowDialog();
            if (mapResizeWindow.isGood)
            {
                Maploader.resize((byte)mapResizeWindow.xsize, (byte)mapResizeWindow.ysize);
                create();
            }
        }


        private void KeyHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                blockposy--;
                moveupdate();
            }

            else if (e.Key == Key.Down)
            {
                blockposy++;
                moveupdate();
            }

            else if (e.Key == Key.Right)
            {
                blockposx--;
                moveupdate();
            }

            else if (e.Key == Key.Left)
            {
                blockposx++;
                moveupdate();
            }
        }

        private void Click_Run_Ingame(object sender, RoutedEventArgs e)
        {
            string gamepath = "C:\\Users\\Sean\\Desktop\\game";

            if (filepath != "")
            {
                ProcessStartInfo start = new ProcessStartInfo("testgame.exe");
                start.WorkingDirectory = gamepath;
                start.Arguments = "\"" + filepath + "\"";
                int exitCode;

                using (Process proc = Process.Start(start))
                {
                    proc.WaitForExit();

                    exitCode = proc.ExitCode;
                }
            }else
            {
                MessageBox.Show("Level must be saved!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                MenuFileSave(sender, e);
            }
        }

        private void Mouse_Wheel(object sender, MouseWheelEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed || Keyboard.IsKeyDown(Key.LeftShift))
            {
                if (e.Delta > 0)
                {
                    blockposx--;
                    moveupdate();
                }
                else
                {
                    blockposx++;
                    moveupdate();
                }
            }
            else
            {
                if (e.Delta < 0)
                {
                    blockposy--;
                    moveupdate();
                }
                else
                {
                    blockposy++;
                    moveupdate();
                }
            }
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Radius = (int)slider.Value;
            RadiusContenLabel.Content = Radius;
        }


        private void BlockSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            blocksize = (int)BlockSizeSlider.Value;
            BSLC.Content = blocksize;
            create();
            
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            TopBlockarea.Background.Opacity = 100;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TopBlockarea.Background.Opacity = 0;
        }

        private void end(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
