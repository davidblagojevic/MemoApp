using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MemoApp.Data.Entities
{
    public partial class Memo
    {
        public Memo()
        {
            Tag = new HashSet<Tag>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int StatusId { get; set; }
        public string AspNetUsersId { get; set; }

        public virtual IdentityUser AspNetUsers { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
    }
}
