using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
    public class CreateUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string IdentityRoleName { get; set; }

    }
}
