using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using iBalekaService.Data.Configurations;
using System.Threading.Tasks;

namespace iBalekaService.Data.Infastructure
{
    public class DbFactory:Disposable,IDbFactory
    {
        IBalekaContext dbContext;
        DbContextOptions<IBalekaContext> options;
  
        
        public IBalekaContext Init()
        {
                       
            return dbContext ?? (dbContext = new IBalekaContext(options));
        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();           
        }
    }
}
