using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Domain.UserManagement
{
    public interface IUserManagement
    {

        int UpdateUser(LYSApp.Model.UserViewModel userViewModel);

        int GetHouseID(long userID);

        int UpdateComment(LYSApp.Model.UserViewModel userviewModel);
    }
}
