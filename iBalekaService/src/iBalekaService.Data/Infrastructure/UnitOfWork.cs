using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Data.Configurations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace iBalekaService.Data.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private IBalekaContext dbContext;
        DbContextFactoryOptions opt { get; set; }

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public IBalekaContext DbContext
        {
            
            get { return dbContext ?? (dbContext = dbFactory.Init(opt)); }
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
