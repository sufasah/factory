using System;
using System.Linq;
using System.Threading.Tasks;
using BF.DataAccess.Abstract.Repositories;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EFScheduleRepo<TContext> : EFEntityRepoBase<Schedule, TContext>, IScheduleRepo
        where TContext : DbContext, new()
    {
    }
}