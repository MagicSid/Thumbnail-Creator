using System;
using System.Drawing;
using System.IO;

namespace ThumbNail_Creator
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean menu = true;
            while (menu)
            {
                menu = menuchoice();
            }
        }


        static private Boolean menuchoice()
        {
            Console.Write("Type \"exit\" to exit or type a file directory: ");
            String userdirectory = Console.ReadLine();
            if (userdirectory.ToLower() == "exit")
            {
                return false;
            }
            else if (Directory.Exists(userdirectory))
            {
                scanner(userdirectory);
                return false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Directory not found");
                return true;
            }
        }

        static private void scanner(String path)
        {
            
            DirectoryInfo d = new DirectoryInfo(path);
            foreach (var file in d.GetFiles("*.jpg"))
            {
                Image tempimage = resizeimage(Image.FromFile(file.FullName));
                savethumbnail(tempimage, file.DirectoryName, file.Name);
            }
            foreach (var file in d.GetFiles("*.jpeg"))
            {
                Image tempimage = resizeimage(Image.FromFile(file.FullName));
                savethumbnail(tempimage, file.DirectoryName, file.Name);
            }

            Console.WriteLine("Finished Gracefully");
            Console.ReadLine();

        }
        static private Image resizeimage(Image image)
        {
            Double newwidth;
            Double newheight;
            if (image.Width >= image.Height)
            {
                newwidth = 180;
                newheight = Math.Floor((Double)(image.Height / (image.Width / 180)));
            }
            else
            {
                newheight = 180;
                newwidth = Math.Floor((Double)(image.Width / (image.Height / 180)));
            }
            Console.WriteLine("(" + image.Height + "," + image.Width + ") + new proportions (" + newheight + "," + newwidth + ")");
            return image.GetThumbnailImage((int)Math.Floor(newwidth), (int)Math.Floor(newheight), null, IntPtr.Zero);
        }
        static private void savethumbnail(Image image, String path, String name)
        {
            String fullpath = path + "\\Thumbnails";
            Directory.CreateDirectory(fullpath);
            image.Save(fullpath + "\\thumb" + name);
        }
    }
}
