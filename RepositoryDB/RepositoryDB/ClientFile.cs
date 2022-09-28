using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDB
{
    [Table("ClientFiles")]
    public class ClientFile : Entity
    {
        [Column("Name")]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }



        public ClientFile(string name)
        {
            Name = name;
        }
    }
}
