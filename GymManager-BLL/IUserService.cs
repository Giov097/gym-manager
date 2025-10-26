using GymManager_BE;

namespace GymManager_BLL;

/// <summary>
/// Defines the contract for user-related operations in the application.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Authenticates a user based on their email and password.
    /// </summary>
    /// <param name="user">The user to validate containing email and password</param>
    /// <returns>A <see cref="User"/> object representing the authenticated user.</returns>
    Task<User> Login(User user);

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>A <see cref="User"/> object representing the user with the specified email.</returns>
    Task<User> GetUserByEmail(string email);

    /// <summary>
    /// Creates a user and saves it to the system.
    /// </summary>
    /// <param name="user">The <see cref="User"/> object to create.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<User> CreateUser(User user);

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>A <see cref="User"/> object representing the user with the specified ID.</returns>
    Task<User> GetUserById(long id);

    /// <summary>
    /// Retrieves a list of all users in the system.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a list of <see cref="User"/> objects.</returns>
    /// <remarks>This method retrieves all users.</remarks>
    Task<List<User>> GetUsers();

    /// <summary>
    /// Updates an existing user in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="user">The <see cref="User"/> object containing the updated information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>This method updates the user with the specified ID.</remarks>
    Task UpdateUser(long id, User user);

    /// <summary>
    /// Retrieves a user associated with a specific fee ID.
    /// </summary>
    /// <param name="feeId">The unique identifier of the fee.</param>
    /// <returns>A <see cref="User"/> object representing the user associated with the specified
    Task<User> GetUserByFeeId(long feeId);
}