﻿using JWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User{Id = 1, UserName="Batman", Password="batman", Role="Manager"},
                new User{Id = 2, UserName="Robin", Password="robin", Role="Employee"}
            };

            users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}