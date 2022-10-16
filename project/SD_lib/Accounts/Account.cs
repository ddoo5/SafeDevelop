using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SD_lib.Entity.Models
{
    [Table("Account")]
    public class Account
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("salt")]
        [StringLength(100)]
        public string PasswordSalt { get; set; }

        [Column("hash")]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [Column("banned")]
        public bool IsBanned { get; set; }

        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Column("surname")]
        [StringLength(255)]
        public string Surname { get; set; }

        [InverseProperty(nameof(AccountSession.Account))]
        public virtual ICollection<AccountSession> Sessions { get; set; }
    }
}

