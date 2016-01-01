using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LYSApp.Data.DBEntity;

namespace LYSApp.Data.DBRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private LYSAdminEntities context;

        public UnitOfWork()
        {
            context = new LYSAdminEntities();
        }

        public DbContext DbContext
        {
            get { return context; }
        }

        public int SaveChanges()
        {
            return context.SaveChanges();

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
