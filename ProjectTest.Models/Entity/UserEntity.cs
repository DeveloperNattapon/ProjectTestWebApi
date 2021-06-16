using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectTest.Models
{
    public class UserEntity
    {
        [Column("UsersID")]
        public int? UsersID { get; set; }
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("RealName")]
        public string RealName { get; set; }
        [Column("EmailAddress")]
        public string EmailAddress { get; set; }
        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }
    }
}
