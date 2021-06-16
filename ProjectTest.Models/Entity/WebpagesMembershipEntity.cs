using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectTest.Models
{
    public class WebpagesMembershipEntity
    {
        [Column("UserId")]
        public int? UserId { get; set; }
        [Column("CreateDate")]
        public DateTime? CreateDate { get; set; }
        [Column("ConfirmationToken")]
        public string ConfirmationToken { get; set; }
        [Column("IsConfirmed")]
        public Byte IsConfirmed { get; set; }
        [Column("LastPasswordFailureDate")]
        public DateTime? LastPasswordFailureDate { get; set; }
        [Column("PasswordFailuresSinceLastSuccess")]
        public int? PasswordFailuresSinceLastSuccess { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("PasswordChangedDate")]
        public DateTime? PasswordChangedDate { get; set; }
        [Column("PasswordSalt")]
        public string PasswordSalt { get; set; }
        [Column("PasswordVerificationToken")]
        public string PasswordVerificationToken { get; set; }
        [Column("PasswordVerificationTokenExpirationDate")]
        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }
    }
}
