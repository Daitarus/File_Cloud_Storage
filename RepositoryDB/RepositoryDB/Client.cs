using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDB
{
    [Table("Clients")]
    public class Client : Entity
    {
        [Column("Hash")]
        [Required]
        public byte[] Hash { get; set; }

        [Column("Name")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Column("Id_Files")]
        public int[]? Id_Files { get; set; }




        public Client(byte[] hash, string name, int[] id_Files)
        {
            Hash = hash;
            Name = name;
            Id_Files = id_Files;
        }
        public Client(byte[] hash, string name)
        {
            Hash = hash;
            Name = name;
        }
    }
}
