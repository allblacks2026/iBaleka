using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using iBalekaService.Data.Configurations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace iBalekaService.Data.Infastructure
{
    public interface IDbFactory:IDisposable
    {
        IBalekaContext Init(DbContextFactoryOptions opt);
    }
}
