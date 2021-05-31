using MemoApp.Data.Entities;
using MemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Data.Repositories
{
    public interface IMemoRepository
    {
        public IEnumerable<Memo> GetAllMemos();
        public IEnumerable<MemoViewModel> GetAllMemosByUserId(string userId);
        public Memo GetMemoById(long memoId);
        public MemoViewModel GetMemoViewById(long memoId);
        public Memo Delete(long memoId);
        public Memo Add(MemoViewModel memoView);
        public Memo Edit(Memo memo, MemoViewModel memoView);
        public bool SaveAll();
    }
}
