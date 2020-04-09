using System;
using MySpectrumCodingTest.Models;

namespace MySpectrumCodingTest
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //public static implicit operator int(User v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
