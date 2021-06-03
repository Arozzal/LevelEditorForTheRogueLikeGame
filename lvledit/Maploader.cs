using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace lvledit
{
    static class Maploader
    {
        private static byte[,] mapgrid;

        public static int getMapXSize() { return mapxsize; }
        public static int getMapYSize() { return mapysize; }
        private static byte mapxsize;
        private static byte mapysize;

        public static void setAt(int x, int y, byte value)
        {
            if (x < mapgrid.GetLength(0) && y < mapgrid.GetLength(1) && x >= 0 && y >= 0)
            {
                mapgrid[x, y] = value;
            }
        }

        public static void createnew(byte xsize, byte ysize, byte id)
        {
            mapxsize = xsize;
            mapysize = ysize;
            mapgrid = new byte[xsize, ysize];
            for (int x = 0; x < mapxsize; x++)
            {
                for (int y = 0; y < mapysize; y++)
                {
                    mapgrid[x, y] = id;
                }
            }
            
        }


        public static void load(string path)
        {
            if (mapgrid != null)
            {
                Array.Clear(mapgrid, mapxsize, mapysize);
            }

            

            byte[] mapfile = File.ReadAllBytes(path);

            foreach(byte i in mapfile)
            {
                Console.Write(i);
            }
            Console.WriteLine();

            Console.WriteLine(mapfile[0] + " " + mapfile[1]);

            mapxsize = mapfile[0];
            mapysize = mapfile[1];
            mapgrid = new byte[mapxsize, mapysize];

            for (int x = 0;x < mapxsize; x++)
            {
                for(int y = 0;y < mapysize; y++)
                {
                    mapgrid[x,y] = mapfile[x * mapysize + y + 2];
                    Console.Write(mapgrid[x,y]);
                }
            }

        }

        public static void save(string path)
        {
            if (path == "")
            {
                throw new System.IO.FileLoadException("empty file string");
            }
            byte[] savedata = new byte[mapxsize * mapysize + 2];
            for (int x = 0; x < mapxsize; x++)
            {
                for (int y = 0; y < mapysize; y++)
                {
                    savedata[x * mapysize + y + 2] = mapgrid[x, y];
                }
            }
            savedata[0] = mapxsize;
            savedata[1] = mapysize;
            File.WriteAllBytes(path, savedata);
        }

        public static void resize(byte xsize, byte ysize)
        {
            byte[,] temparr = new byte[xsize,ysize];

            for (int x = 0; x < xsize; x++)
            {
                for (int y = 0; y < ysize; y++)
                {
                    if (x < mapgrid.GetLength(0) && y < mapgrid.GetLength(1))
                    {
                        temparr[x, y] = mapgrid[x, y];
                    }else
                    {
                        temparr[x, y] = 1;
                    }
                }
            }

            mapgrid = new byte[xsize, ysize];

            for (int x = 0; x < xsize; x++)
            {
                for (int y = 0; y < ysize; y++)
                {
                    mapgrid[x, y] = temparr[x, y];
                }
            }

            
            mapxsize = xsize;
            mapysize = ysize;



        }

        public static byte[,] getList()
        {
            return mapgrid;
        }
    }
}
