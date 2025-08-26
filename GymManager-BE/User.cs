namespace GymManager_BE;

public class User : IEntity<long>
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IEnumerable<UserRole> UserRoles { get; set; }

    public IEnumerable<Fee> Fees { get; set; } = new List<Fee>();
}