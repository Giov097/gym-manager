namespace GymManager_MPP;

/// <summary>
/// Defines a generic interface for performing CRUD operations on objects of type <typeparamref name="T"/>
/// with identifiers of type <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="T">The type of the objects managed by this interface.</typeparam>
/// <typeparam name="TId">The type of the unique identifier for the objects.</typeparam>
public interface IMapper<T, in TId>
{
    /// <summary>
    /// Saves an object to the storage.
    /// </summary>
    /// <param name="obj">The object to save.</param>
    /// <returns>The saved object.</returns>
    Task<T> Create(T obj);

    /// <summary>
    /// Retrieves an object by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the object.</param>
    /// <returns>The found object, or <c>null</c> if not found.</returns>
    Task<T?> GetById(TId id);

    /// <summary>
    /// Retrieves all stored objects.
    /// </summary>
    /// <returns>A list of all objects.</returns>
    Task<List<T>> GetAll();

    /// <summary>
    /// Deletes an object by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the object to delete.</param>
    /// <returns><c>true</c> if the object was successfully deleted; otherwise, <c>false</c>.</returns>
    Task<bool> Delete(TId id);

    /// <summary>
    /// Updates an existing object in the storage.
    /// </summary>
    /// <param name="obj">The object with updated data.</param>
    /// <returns><c>true</c> if the object was successfully updated; otherwise, <c>false</c>.</returns>
    Task<bool> Update(T obj);
}