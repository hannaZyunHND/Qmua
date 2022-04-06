using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MI.Bo.Bussiness
{
    public partial class UserBCL : Base<User>
    {
        public UserBCL()
        {

        }

        public User Login(string userName, string passWord)
        {

            using (IDbContext _context = new IDbContext())
            {
                var user = _context.User.SingleOrDefault(x => x.UserName == userName && x.Password == passWord);

                return user;
            }
        }
    }
}
