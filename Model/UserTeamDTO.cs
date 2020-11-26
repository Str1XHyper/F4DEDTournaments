using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum TeamRoles
    {
        Member = 0,
        Admin = 1,
        Owner = 2
    }


    public class UserTeamDTO: TeamDTO
    {
        public TeamRoles Role { get; set; }
    }
}
