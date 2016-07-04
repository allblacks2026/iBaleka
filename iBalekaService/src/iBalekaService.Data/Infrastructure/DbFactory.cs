using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using iBalekaService.Data.Configurations;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace iBalekaService.Data.Infastructure
{
    public class DbFactory:Disposable,IDbFactory
    {
        IBalekaContext dbContext;
DbContextOptionsBuilder<IBalekaContext> options;
  
        
        public IBalekaContext Init()
        {  
           

            options = new DbContextOptionsBuilder<IBalekaContext>();
            
           return dbContext ?? (dbContext = new IBalekaContext(options));

        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();           
        }
    }
}
