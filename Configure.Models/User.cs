namespace Configure.Models;

public class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public Email? Email { get; set; }
    
    public Role? Role { get; set; }
}