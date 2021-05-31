using MemoApp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList.Mvc;
using X.PagedList;
using MemoApp.Data.Entities;
using MemoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MemoApp.Services;
using MemoApp.Wrapper;
using Microsoft.Extensions.Localization;

namespace MemoApp.Controllers
{
    [Authorize]
    public class MemoController : Controller
    {
        private readonly ILogger<MemoController> logger;
        private readonly IMemoRepository memoRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMemoService memoService;
        private readonly IStringLocalizer<MemoController> localizer;

        public MemoController(ILogger<MemoController> logger, IMemoRepository memoRepository,
            UserManager<IdentityUser> userManager, IMemoService memoService, IStringLocalizer<MemoController> localizer)
        {
            this.logger = logger;
            this.memoRepository = memoRepository;
            this.userManager = userManager;
            this.memoService = memoService;
            this.localizer = localizer;
        }


        public async Task<IActionResult> Index()
        {
            var loggedUser = await userManager.GetUserAsync(User);
            foreach (var memo in memoRepository.GetAllMemos())
            {
                foreach (var tag in memo.Tag)
                {
                    logger.LogError(tag.Name);
                }
            }

            var result = memoRepository.GetAllMemosByUserId(loggedUser.Id).ToList();
            return View(result);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = memoService.Delete(id);
            return Json(result);

        }

        [HttpPost]
        public IActionResult AddOrEditMemo(MemoViewModel memoView)
        {
            var loggedUser = userManager.GetUserAsync(User).Result;
            
            var result = new Result<MemoViewModel>();
            
            if (ModelState.IsValid)
            {
                if (memoView.Id != 0)
                {
                    result = memoService.Edit(memoView);
                }
                else
                {
                    memoView.AspNetUsersId = loggedUser.Id;
                    result = memoService.Add(memoView);
                }

            }
            return Json(result);
        }
        public async Task<IActionResult> GetAddEditModal(long? id)
        {

            var loggedUser = await userManager.GetUserAsync(User);
            MemoViewModel memoView;
            if (id.HasValue)
            {

                memoView = memoService.GetMemoViewById(id.Value);
            }
            else
            {
                memoView = new MemoViewModel();
            }


            return PartialView("_AddEditMemoPartial", memoView);

        }

        public IActionResult Test()
        {
            //sa parametrima localizer
            var i = localizer["Customer not found", 0];
            return View();
        }

    }
}
