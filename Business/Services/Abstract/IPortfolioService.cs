using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IPortfolioService
    {
        Task AddAsync(Portfolio portfolio);
        void Delete(int id);
        void Update(int id, Portfolio newPortfolio);
        Portfolio Get(Func<Portfolio, bool>? func = null);
        List<Portfolio> GetAll(Func<Portfolio, bool>? func = null);

    }
}
