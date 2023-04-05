namespace Configure.Models.interfaces;

public interface IUser
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public Email Email { get; set; }
    
    public Role Role { get; set; }
}