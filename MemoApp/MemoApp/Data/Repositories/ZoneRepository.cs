using MemoApp.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoApp.Data.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly MemoContext memoCtx;
        private readonly ILogger<ZoneRepository> logger;

        public ZoneRepository(MemoContext memoCtx, ILogger<ZoneRepository> logger)
        {
            this.memoCtx = memoCtx;
            this.logger = logger;
        }
        public Zone GetZoneByUserId(string userId)
        {
            return memoCtx.Zone.FirstOrDefault(z => z.AspNetUsersId == userId);
        }
    }
}
