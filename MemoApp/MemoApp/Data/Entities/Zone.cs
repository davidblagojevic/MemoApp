using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MemoApp.Data.Entities
{
    public partial class Zone
    {
        public long Id { get; set; }
        public string ZoneName { get; set; }
        public string AspNetUsersId { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string Culture { get; set; }

        public virtual IdentityUser AspNetUsers { get; set; }
    }
}
