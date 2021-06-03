using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.IO;

namespace lvledit
{
    class Imagelist
    {
        private BitmapImage[] imgdata = new BitmapImage[255];
        private int lenght = 255;

        public int Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }

        public bool isUsedId(byte i) {
            if(this[i] != imgdata[0])
            {
                return true;
            }
            return false;
        }

        public BitmapImage this[int index]
        {
            get {
                if (imgdata[index] != null)
                {
                    return imgdata[index];
                }
                else
                {
                    return imgdata[0];
                }
            }


            set { imgdata[index] = value; }
        }
        public BitmapImage gridimg { get; set;}
        public BitmapImage transparimg { get; set; }

        public Imagelist()
        {
            imgdata[0] = new BitmapImage();
            imgdata[0].BeginInit();
            imgdata[0].UriSource = new Uri("images/Block0.png", UriKind.Relative);
            imgdata[0].EndInit();

            for (int i = 2; i < 10; i++)
            {
                imgdata[i] = new BitmapImage();
                imgdata[i].BeginInit();
                imgdata[i].UriSource = new Uri("images/Block" + i + ".png", UriKind.Relative);
                imgdata[i].EndInit();
            }
            imgdata[21] = new BitmapImage();
            imgdata[21].BeginInit();
            imgdata[21].UriSource = new Uri("images/Keyblock.png", UriKind.Relative);
            imgdata[21].EndInit();

            imgdata[22] = new BitmapImage();
            imgdata[22].BeginInit();
            imgdata[22].UriSource = new Uri("images/Key.png", UriKind.Relative);
            imgdata[22].EndInit();

            imgdata[32] = new BitmapImage();
            imgdata[32].BeginInit();
            imgdata[32].UriSource = new Uri("images/arrow32.png", UriKind.Relative);
            imgdata[32].EndInit();

            imgdata[33] = new BitmapImage();
            imgdata[33].BeginInit();
            imgdata[33].UriSource = new Uri("images/arrow33.png", UriKind.Relative);
            imgdata[33].EndInit();

            imgdata[34] = new BitmapImage();
            imgdata[34].BeginInit();
            imgdata[34].UriSource = new Uri("images/arrow34.png", UriKind.Relative);
            imgdata[34].EndInit();

            imgdata[35] = new BitmapImage();
            imgdata[35].BeginInit();
            imgdata[35].UriSource = new Uri("images/arrow35.png", UriKind.Relative);
            imgdata[35].EndInit();

            imgdata[45] = new BitmapImage();
            imgdata[45].BeginInit();
            imgdata[45].UriSource = new Uri("images/BulletTower.png", UriKind.Relative);
            imgdata[45].EndInit();

            imgdata[46] = new BitmapImage();
            imgdata[46].BeginInit();
            imgdata[46].UriSource = new Uri("images/BulletTowerMega.png", UriKind.Relative);
            imgdata[46].EndInit();

            imgdata[48] = new BitmapImage();
            imgdata[48].BeginInit();
            imgdata[48].UriSource = new Uri("images/Walker.png", UriKind.Relative);
            imgdata[48].EndInit();

            imgdata[49] = new BitmapImage();
            imgdata[49].BeginInit();
            imgdata[49].UriSource = new Uri("images/Ghost.png", UriKind.Relative);
            imgdata[49].EndInit();

            imgdata[50] = new BitmapImage();
            imgdata[50].BeginInit();
            imgdata[50].UriSource = new Uri("images/Berserker.png", UriKind.Relative);
            imgdata[50].EndInit();

            imgdata[70] = new BitmapImage();
            imgdata[70].BeginInit();
            imgdata[70].UriSource = new Uri("images/Speed_Item.png", UriKind.Relative);
            imgdata[70].EndInit();

            imgdata[71] = new BitmapImage();
            imgdata[71].BeginInit();
            imgdata[71].UriSource = new Uri("images/Bullet_Upgrade_Item.png", UriKind.Relative);
            imgdata[71].EndInit();

            imgdata[72] = new BitmapImage();
            imgdata[72].BeginInit();
            imgdata[72].UriSource = new Uri("images/Bullet_Upgrade_Speed_Item.png", UriKind.Relative);
            imgdata[72].EndInit();

            imgdata[73] = new BitmapImage();
            imgdata[73].BeginInit();
            imgdata[73].UriSource = new Uri("images/Bullet_Upgrade_Add_Item.png", UriKind.Relative);
            imgdata[73].EndInit();

            imgdata[74] = new BitmapImage();
            imgdata[74].BeginInit();
            imgdata[74].UriSource = new Uri("images/Shield_Upgrade_Item.png", UriKind.Relative);
            imgdata[74].EndInit();

            imgdata[123] = new BitmapImage();
            imgdata[123].BeginInit();
            imgdata[123].UriSource = new Uri("images/Player.png", UriKind.Relative);
            imgdata[123].EndInit();

            imgdata[124] = new BitmapImage();
            imgdata[124].BeginInit();
            imgdata[124].UriSource = new Uri("images/arrow124.png", UriKind.Relative);
            imgdata[124].EndInit();

            imgdata[125] = new BitmapImage();
            imgdata[125].BeginInit();
            imgdata[125].UriSource = new Uri("images/arrow125.png", UriKind.Relative);
            imgdata[125].EndInit();

            imgdata[126] = new BitmapImage();
            imgdata[126].BeginInit();
            imgdata[126].UriSource = new Uri("images/arrow126.png", UriKind.Relative);
            imgdata[126].EndInit();

            imgdata[127] = new BitmapImage();
            imgdata[127].BeginInit();
            imgdata[127].UriSource = new Uri("images/arrow127.png", UriKind.Relative);
            imgdata[127].EndInit();


            gridimg = new BitmapImage();
            gridimg.BeginInit();
            gridimg.UriSource = new Uri("images/grid.png", UriKind.Relative);
            gridimg.EndInit();

            

            transparimg = new BitmapImage();
            transparimg.BeginInit();
            transparimg.UriSource = new Uri("images/transparent.png", UriKind.Relative);
            transparimg.EndInit();
        }
    }
}
