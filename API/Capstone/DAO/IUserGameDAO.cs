using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IUserGameDAO
    {
        int InviteUser(int userId, int gameId);
        void AcceptInvite(int userGameId);
    }
}
