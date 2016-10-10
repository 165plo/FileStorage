using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FileStorageMVC
{
    public class File : FileMeta
    {
        public byte[] Payload { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public bool Deleted { get; set; }
        public DateTime MarkedDeleted { get; set; }
    }

    public class FileMeta
    {
        [Key]
        public int Key { get; set; }
        
        public string Name { get; set; }
    }

    public class FileViewModel
    {
        public int Key {get;set;}
        public string Name{get; set;}

        public string Tags {get;set;}
    }

    public static class FileExtenstions
    {
        public static FileViewModel ToViewModel(this File file)
        {
            string tags = string.Empty;

            return new FileViewModel
            {
                Key = file.Key,
                Name = file.Name,
                Tags = file.Tags!=null && file.Tags.Any() ? string.Join(",", file.Tags) : string.Empty
            };
        }
    }
}