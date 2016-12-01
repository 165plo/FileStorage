using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.IO;
using System.Drawing;

namespace FileStorageMVC
{
    public class File : FileMeta
    {
        public byte[] Payload { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public bool Deleted { get; set; }
        public DateTime MarkedDeleted { get; set; }

        public static int CompareByName(File file1, File file2)
        { 
            return String.Compare(file1.Name, file2.Name);
        }
    }

    public class FileMeta
    {
        [Key]
        public int Key { get; set; }

        public string Name { get; set; }
    }

    public class FileViewModel
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public string Tags { get; set; }

        public byte[] File { get; set; }

    }


    public class EditViewModel
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public string Tags { get; set; }
        public List<string> SelectTags { get; set; }
    }

    public static class FileExtenstions
    {
        public static FileViewModel ToViewModel(this File file)
        {
            string tags = string.Empty;

            Image thumb = null;
            byte[] imageBytes = null;
            var image = byteArrayToImage(file.Payload);
            if(image != null)
            {
                thumb = image.GetThumbnailImage(80, 80, () => false, IntPtr.Zero);
                imageBytes = imageToByteArray(thumb);
            }
            

            return new FileViewModel
            {
                Key = file.Key,
                Name = file.Name,
                Tags = file.Tags != null && file.Tags.Any() ? string.Join(",", file.Tags) : string.Empty,
                File = imageBytes
            };
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            try{
            MemoryStream ms = new MemoryStream(byteArrayIn);
            
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }catch
            {
                return null;
            }
        }
    }
}