using GoalsMover.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoalsMover.BLL.Helpers.Methods
{
    public static class AuthExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}
