using Business.Services.Abstract;
using Core.Models;
using Core.RepoAbstract;
using Data.RepoConcretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository )
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task AddAsync(Portfolio portfolio)
        {
            await _portfolioRepository.AddAsync(portfolio);
            await _portfolioRepository.CommitAsync();
        }

        public void Delete(int id)
        {
            var existPortfolio = _portfolioRepository.Get(x => x.Id == id);
            if (existPortfolio == null) throw new NullReferenceException();

            _portfolioRepository.Delete(existPortfolio);
            _portfolioRepository.Commit();
        }

        public Portfolio Get(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.Get(func);
        }

        public List<Portfolio> GetAll(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.GetAll(func);
        }

        public void Update(int id, Portfolio newPortfolio)
        {
            Portfolio oldPortfolio = _portfolioRepository.Get(x => x.Id == id);
            if (oldPortfolio == null) throw new NullReferenceException();


            oldPortfolio.Title = newPortfolio.Title;
            oldPortfolio.Description = newPortfolio.Description;
            oldPortfolio.ImageUrl = newPortfolio.ImageUrl;
            oldPortfolio.ImageFile = newPortfolio.ImageFile;


            _portfolioRepository.Commit();
        }
    }
}
