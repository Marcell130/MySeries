using System;

namespace MySeries.Client.Core.Model
{
    public class UserInfo
    {
        public string FullName { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserGender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
