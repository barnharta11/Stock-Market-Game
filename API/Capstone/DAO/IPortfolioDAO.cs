using Capstone.Models;

namespace Capstone.DAO
{
    public interface IPortfolioDAO
    {
        Portfolio GetPortfolio(int userGameID);
    }
}