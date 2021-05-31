using MemoApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Models
{
    public class MemoViewModel
    {
        public MemoViewModel() { }
        public MemoViewModel(Memo memo)
        {
            Id = memo.Id;
            Title = memo.Title;
            Note = memo.Note;
            Status = (StatusEnum)memo.StatusId;
            //vraca samo name za tag
            TagList = memo.Tag.Select(t => t.Name).ToList();
            CreatedAt = memo.CreatedAt;
            UpdatedAt = memo.UpdatedAt;
            AspNetUsersId = memo.AspNetUsersId;
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Active;
        public List<string> TagList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string AspNetUsersId { get; set; }

        //public string InputTags { get; set; }
    }
    public enum StatusEnum
    {
        Active = 1,
        Deleted = 2
    }
}
