using System.Text.Json.Serialization;

public class LoginDTO
{
    public string Correo { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
    