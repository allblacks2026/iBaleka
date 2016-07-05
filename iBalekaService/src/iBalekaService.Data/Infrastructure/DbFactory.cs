using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using iBalekaService.Data.Configurations;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace iBalekaService.Data.Infastructure
{
    public class DbFactory:Disposable,IDbFactory, IDbContextFactory<IBalekaContext>
    {
        IBalekaContext dbContext;
        
  
        public IBalekaContext Create(DbContextFactoryOptions opt)
        {
            var builder = new DbContextOptionsBuilder<IBalekaContext>();
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=iBalekaDB;Integrated Security=True;");
            return new IBalekaContext(builder.Options);
        }
        public IBalekaContext Init(DbContextFactoryOptions opt)
        {
            return dbContext ?? (dbContext = Create(opt));
        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();           
        }
    }
}
