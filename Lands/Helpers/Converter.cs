﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.Helpers
{
    using System;
    using Domain;
    using Models;

    public static class Converter
    {
        internal static UserLocal ToUserLocal(User user)
        {
            return new UserLocal
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                Telephone = user.Telephone,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId,
            };
    }

        internal static User ToUserDomain(UserLocal user, byte[] imageArray)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                Telephone = user.Telephone,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId.Value,
                ImageArray = imageArray,
            };
    }
    }
}
