using System.ComponentModel.DataAnnotations;

namespace FileStorageMVC
{
    public class Tag
    {
        [Key]
        public int Key { get; set; }
        public string Value { get; set; }
    }
}