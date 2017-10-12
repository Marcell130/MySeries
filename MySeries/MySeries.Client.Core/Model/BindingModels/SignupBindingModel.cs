using System;

namespace MySeries.Client.Core.Model.BindingModels
{
    public class SignupBindingModel
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public UserGender? Gender { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
