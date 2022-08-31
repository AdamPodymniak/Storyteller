namespace Storyteller.API.Models
{
    public class ResponseTokenModel
    {
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public Guid UserGuid{ get; set; }
    }
}
