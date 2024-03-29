using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilesAPI.Models
{
    public class FileEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToLocalTime();

    }
}