using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LYSApp.Data.DBRepository
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
        int SaveChanges();
    }
}
