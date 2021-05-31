using MemoApp.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MemoApp.Models;

namespace MemoApp.Data.Repositories
{
    public class MemoRepository : IMemoRepository
    {
        private readonly MemoContext memoCtx;
        private readonly ILogger<MemoRepository> logger;

        public MemoRepository(MemoContext memoCtx, ILogger<MemoRepository> logger)
        {
            this.memoCtx = memoCtx;
            this.logger = logger;
        }

        public IEnumerable<Memo> GetAllMemos()
        {
            return memoCtx.Memo
                .Include(m => m.Status)
                // ne mora include tag vec je ubacen
                //.Include(m => m.Tag)
                //pitaj zasto ovo ne radi
                //.Include(m => m.AspNetUsers)
                .ToList();
        }

        public IEnumerable<MemoViewModel> GetAllMemosByUserId(string userId)
        {
            return memoCtx.Memo
                .Where(m => m.AspNetUsersId == userId)
                //ne mora ni status jer se koristi samo id, a ne ceo objekat
                //.Include(m => m.Status)
                // ne mora include tag
                //.Include(m => m.Tag)
                .Select(m => new MemoViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Note = m.Note,
                    Status = (StatusEnum)m.StatusId,
                    //vraca samo name za tag
                    TagList = m.Tag.Select(t => t.Name).ToList(),
                    CreatedAt = m.CreatedAt,
                    UpdatedAt = m.UpdatedAt,
                    AspNetUsersId = m.AspNetUsersId
                }).ToList();
        }

        public Memo Delete(long memoId)
        {
            Memo memo = memoCtx.Memo.FirstOrDefault(m => m.Id == memoId);
            if (memo != null)
            {
                //memo.Status = memoCtx.Status.FirstOrDefault(s => s.Name == "Deleted");
                memo.StatusId = (int)StatusEnum.Deleted;
            }
            return memo;
        }

        public Memo GetMemoById(long memoId)
        {
            return memoCtx.Memo.Include(m => m.Tag).FirstOrDefault(m => m.Id == memoId);
        }



        public bool SaveAll()
        {
            //proverava koliko redova je izmenjeno, 
            //ako je vece od 0 onda je true i bice saveovano
            return memoCtx.SaveChanges() > 0;
        }

        public Memo Add(MemoViewModel memoView)
        {
            var memo = new Memo();
            memo.Title = memoView.Title;
            memo.Note = memoView.Note;
            memo.CreatedAt = DateTime.UtcNow;
            memo.Tag = memoView.TagList.Select(t => new Tag() { Name = t }).ToList();
            memo.AspNetUsersId = memoView.AspNetUsersId;
            memo.StatusId = (int)StatusEnum.Active;
            memoCtx.Add(memo);
            return memo;
        }

        public Memo Edit(Memo memo, MemoViewModel memoView)
        {
            memoCtx.Tag.RemoveRange(memo.Tag.ToList());
            memo.Title = memoView.Title;
            memo.Note = memoView.Note;
            memo.UpdatedAt = DateTime.UtcNow;
            memo.Tag = memoView.TagList.Select(t => new Tag() { Name = t }).ToList();

            memoCtx.Update(memo);

            return memo;
        }



        public MemoViewModel GetMemoViewById(long memoId)
        {
            return memoCtx.Memo.Include(m => m.Tag).Where(m => m.Id == memoId).Select(m => new MemoViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Note = m.Note,
                Status = (StatusEnum)m.StatusId,
                //vraca samo name za tag
                TagList = m.Tag.Select(t => t.Name).ToList(),
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt,
                AspNetUsersId = m.AspNetUsersId
            }).FirstOrDefault();
        }
    }
}
