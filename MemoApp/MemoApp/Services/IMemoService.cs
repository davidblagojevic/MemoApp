using MemoApp.Models;
using MemoApp.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Services
{
    public interface IMemoService
    {
        public Result<MemoViewModel> Delete(long memoId);
        public Result<MemoViewModel> Add(MemoViewModel memoView);
        public Result<MemoViewModel> Edit(MemoViewModel memoView);
        public MemoViewModel GetMemoViewById(long id);
    }
}
