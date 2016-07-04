using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using iBalekaService.Data.Configurations;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using Microsoft.Extensions.DependencyInjection.Extensions;
>>>>>>> 1d33643fbaf15f98ba7a817b3e1159d536cd8a52

namespace iBalekaService.Data.Infastructure
{
    public class DbFactory:Disposable,IDbFactory
    {
        IBalekaContext dbContext;
<<<<<<< HEAD
        DbContextOptions<IBalekaContext> options;
=======
        DbContextOptionsBuilder<IBalekaContext> options;
>>>>>>> 1d33643fbaf15f98ba7a817b3e1159d536cd8a52
  
        
        public IBalekaContext Init()
        {
<<<<<<< HEAD
                       
            return dbContext ?? (dbContext = new IBalekaContext(options));
=======
            options = new DbContextOptionsBuilder<IBalekaContext>();
            
                return dbContext ?? (dbContext = new IBalekaContext(options));
>>>>>>> 1d33643fbaf15f98ba7a817b3e1159d536cd8a52
        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();           
        }
    }
}
