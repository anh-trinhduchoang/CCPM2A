using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBack.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ShopBackDbContext Init();
    }
}
