using MemoApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Data.Repositories
{
    public interface IZoneRepository
    {
        public Zone GetZoneByUserId(string userId);
    }
}
