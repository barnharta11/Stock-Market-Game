using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IUserGameDAO
    {
        int InviteUser(Invite inviteRequest);
        void AcceptInvite(int userGameId);
    }
}
