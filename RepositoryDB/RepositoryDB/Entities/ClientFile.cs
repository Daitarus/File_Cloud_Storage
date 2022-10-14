using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDB
{
    [Table("Client_File")]
    public class ClientFile : Entity
    {
        [Column("Id_Client")]
        [Required]
        public int Id_Client { get; set; }

        [Column("Id_File")]
        [Required]
        public int Id_File { get; set; }

        public ClientFile(int id_Client, int id_File)
        {
            Id_Client = id_Client;
            Id_File = id_File;
        }
    }
}
