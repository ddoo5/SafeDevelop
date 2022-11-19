using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SD_lib.Entity.Models
{
    [Table("Cards")]
    public class Card
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CardId { get; set; }

        [Column("card_number")]
        public string CardNumber { get; set; }

        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }

        [Column("cvv")]
        [MaxLength(3)]
        public int CVV { get; set; }

        [Column("date_expiration")]
        public DateTime ExpirationDate { get; set; }

        [Column("holder")]
        public virtual Client? Holder { get; set; }
    }
}

