using FirstWebApp.Models;

namespace FirstWebAPI.Models
{
    //Client sends the JSON stringified object to the server
    public class AuthenticationRequest
    {
        public string Username { get; set; }    
        public string Password { get; set; }

    }
    //JSON serialized and sent back to the client
    public class AuthenticationResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public AuthenticationResponse(User user, string token) =>
            (UserId, FullName, Token) = (user.UserId, user.FullName, token); 
    }
    //Configuration Section Class defined here. 
    //Name of the class and properties should match with the configuration section name and the JSON properties
    public class AppSettings
    {
        public string ApplicationId { get; set; } = string.Empty;
        public string AppSecretKey { get; set; } = string.Empty;
    }
}
