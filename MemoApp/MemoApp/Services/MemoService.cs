using MemoApp.Data.Entities;
using MemoApp.Data.Repositories;
using MemoApp.Models;
using MemoApp.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Services
{
    public class MemoService : IMemoService
    {
        private readonly IMemoRepository memoRepository;

        public MemoService(IMemoRepository memoRepository)
        {
            this.memoRepository = memoRepository;
        }

        public Result<MemoViewModel> Add(MemoViewModel memoView)
        {

            var result = new Result<MemoViewModel>();

            var memo = memoRepository.Add(memoView);


            if (memoRepository.SaveAll())
            {
                result.Value = new MemoViewModel(memo);
                result.Outcome = OutcomeEnum.Success;
            }
            else
            {
                result.Outcome = OutcomeEnum.Failure;
            }

            return result;


        }

        public Result<MemoViewModel> Edit(MemoViewModel memoView)
        {
            var result = new Result<MemoViewModel>();
            var memo = memoRepository.GetMemoById(memoView.Id);
            memoRepository.Edit(memo, memoView);
            if (memoRepository.SaveAll())
            {
                result.Value = new MemoViewModel(memo);
                result.Outcome = OutcomeEnum.Success;
            }
            else
            {
                result.Outcome = OutcomeEnum.Failure;
            }



            return result;
        }

        public Result<MemoViewModel> Delete(long memoId)
        {
            var result = new Result<MemoViewModel>();
            var memo = memoRepository.Delete(memoId);
            if (memoRepository.SaveAll())
            {
                result.Value = new MemoViewModel(memo);
                result.Outcome = OutcomeEnum.Success;
            }
            else
            {
                result.Outcome = OutcomeEnum.Failure;
            }


            return result;
        }

        public MemoViewModel GetMemoViewById(long id)
        {
            var memo = memoRepository.GetMemoById(id);

            return new MemoViewModel(memo);
        }
    }
}
