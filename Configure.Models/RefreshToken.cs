namespace Configure.Models;

public class RefreshToken
{
    public int Id { get; set; }

    public required int UserId { get; set; }
    
    public required string Token { get; set; }

}   
