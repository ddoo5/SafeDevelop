using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SD_lib.Entity.Models
{
    [Table("Clients")]
    public class Client
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [Column("surname")]
        [StringLength(50)]
        public string? Surname { get; set; }

        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }

        [Column("patronymic")]
        [StringLength(50)]
        public string? Patronymic { get; set; }

        [InverseProperty(nameof(Card.Holder))]
        public virtual ICollection<Card> ClientCards { get; set; } = new HashSet<Card>();
    }
}

