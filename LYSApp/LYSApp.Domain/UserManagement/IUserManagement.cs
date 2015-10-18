using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;

namespace LYSApp.Domain.UserManagement
{
    public interface IUserManagement
    {

        int UpdateUser(UserViewModel userViewModel);

        int GetHouseID(long userID);

        int UpdateComment(UserViewModel userviewModel);

    }
}
