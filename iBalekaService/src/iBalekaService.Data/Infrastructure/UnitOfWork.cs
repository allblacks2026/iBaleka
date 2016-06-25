using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Data.Configurations;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaService.Data.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private IBalekaContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public IBalekaContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
