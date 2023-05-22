using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class User
    {
        /// <summary> Gets or set the id of the player. </summary>
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int UserId { get; set; }

        /// <summary> Gets or sets the username of the player. </summary>
        [Required] [StringLength(ConstData.StringLengh)] public string UserName { get; set; }

        /// <summary> Gets or sets the password of the player. </summary>
        [Required] public string Password { get; set; }
  
    }
}
