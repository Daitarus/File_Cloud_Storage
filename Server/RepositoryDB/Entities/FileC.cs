using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDB
{
    [Table("Files")]
    public class FileC : Entity
    {
        [Column("Name")]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Column("Path")]
        [MaxLength(100)]
        [Required]
        public string Path { get; set; }

        [Column("FullPath")]
        [MaxLength(200)]
        [Required]
        public string FullPath { get; set; }



        public FileC(string name, string path, string fullPath)
        {
            Name = name;
            Path = path;
            FullPath = fullPath;
        }
    }
}
