using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Entity
{
    public class InviteHistoryEntity
    {
        public int inviteUser { get; set; }

        public int invitedUser { get; set; }

        public int coin { get; set; }
    }
}