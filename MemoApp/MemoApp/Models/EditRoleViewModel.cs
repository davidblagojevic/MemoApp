using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
    public class EditRoleViewModel
    {
        [Required]
        public string RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
