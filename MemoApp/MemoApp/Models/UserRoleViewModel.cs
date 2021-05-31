using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> SelectRoleName { get; set; }



    }
}
