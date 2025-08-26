namespace GymManager_BE;

/// <summary>
/// Interface for entities with an identifier.
/// This interface is used to define a contract for entities that have a unique identifier.
/// It is typically used in data access layers to ensure that all entities can be identified by a unique ID.
/// The ID type is generic, allowing for flexibility in the type of identifier used (e.g., int, string, Guid).
/// This interface can be implemented by any class that represents an entity in the system, such as a user, product, or order.
/// By implementing this interface,
/// classes can be easily managed in collections, databases, or other data structures that require unique identification
/// </summary>
/// <typeparam name="TId">The identifier of the entity</typeparam>
public interface IEntity<TId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// This property is used to uniquely identify the entity within the system.
    /// It is typically used as a primary key in databases or as a unique identifier in collections.
    /// The type of the identifier is generic, allowing for different types of identifiers to be used
    /// (e.g., int, string, Guid).
    /// </summary>
    public TId Id { get; set; }
}