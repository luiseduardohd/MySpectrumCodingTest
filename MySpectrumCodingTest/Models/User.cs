﻿using System;
using MySpectrumCodingTest.Models;

namespace MySpectrumCodingTest
{
    public enum UserType
    {
        Admin,
        RegularUser
    }
    public class User : Entity
    {
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Admin { get; set; }
        public string Description { get; set; }
    }
}
