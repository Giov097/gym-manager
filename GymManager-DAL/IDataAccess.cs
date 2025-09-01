using System.Data;

namespace GymManager_DAL;

/// <summary>
/// Defines the contract for data access operations, including read and write methods.
///
/// </summary>
public interface IDataAccess
{
    /// <summary>
    /// Executes a read operation on the database using the provided query.
    /// </summary>
    /// <param name="query">The SQL query to execute for retrieving data.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="DataSet"/> with the data retrieved from the database.
    /// </returns>
    Task<DataSet> Read(string query);

    /// <summary>
    /// Executes a write operation on the database using the provided query.
    /// </summary>
    /// <param name="query">The SQL query to execute for performing the write operation (INSERT, UPDATE, DELETE).</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task<object?> Write(string query);

    /// <summary>
    /// Tests the connection to the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a boolean indicating whether the connection test was successful.
    /// </returns>
    Task<bool> TestConnectionAsync();
}