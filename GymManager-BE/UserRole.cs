namespace GymManager_BE;

public enum UserRole
{
    Admin,
    Trainer,
    Student
}

public static class UserRoleExtensions
{
    private static readonly Dictionary<UserRole, string> RoleNames = new()
    {
        { UserRole.Admin, "ADMINISTRADOR" },
        { UserRole.Trainer, "ENTRENADOR" },
        { UserRole.Student, "ALUMNO" }
    };

    public static string GetRoleName(this UserRole role) => RoleNames[role];
}