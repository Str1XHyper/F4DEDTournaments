using System;
using System.Collections.Generic;
using System.Text;

namespace Interface.User
{
    public interface IUserDB
    {
        int GetCurrency(string UserID);
    }
}
