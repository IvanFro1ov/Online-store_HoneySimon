﻿namespace Honey.Domain.Dto.User.Update
{
    public class UserUpdateRequestDto
    {
        public string UserName { get; set; }
        public string NewUserName { get; set; }
        public string Firstname { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
    }
}