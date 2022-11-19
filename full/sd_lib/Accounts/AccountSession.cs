using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SD_lib.Entity.Models
{
    [Table("Session")]
    public class AccountSession
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        [Column("token")]
        [StringLength(384)]
        public string SessionToken { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_last_request")]
        public DateTime DateLastRequest { get; set; }

        [Column("date_closed")]
        public DateTime DateClosed { get; set; }

        [Column("user")]
        public virtual Account Account { get; set; }
    }
}

