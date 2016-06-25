using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iBalekaService.Data.Configurations;
using System.Threading.Tasks;

namespace iBalekaService.Data.Infastructure
{
    public interface IDbFactory:IDisposable
    {
        IBalekaContext Init();
    }
}
