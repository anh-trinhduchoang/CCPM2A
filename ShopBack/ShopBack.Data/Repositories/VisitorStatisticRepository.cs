using ShopBack.Data.Infrastructure;
using ShopBack.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBack.Data.Repositories
{
    public interface IVisitorStatisticRepository : IRepository<VisitorStatistic>
    {
    }

    public class VisitorStatisticRepository : RepositoryBase<VisitorStatistic>, IVisitorStatisticRepository
    {
        public VisitorStatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
