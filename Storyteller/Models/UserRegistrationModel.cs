namespace Storyteller.API.Models
{
    public class UserRegistrationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Invitation { get; set; }
    }
}
