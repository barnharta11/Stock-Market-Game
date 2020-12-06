using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IGameDAO
    {
        Game GetGame(int gameId);
        Game AddGame(string gameName, int creatorId, DateTime startDate, DateTime endDate);
    }
}
