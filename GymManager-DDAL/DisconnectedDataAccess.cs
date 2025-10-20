using Microsoft.Extensions.Logging;

namespace GymManager_DDAL;

using System.Data;
using Microsoft.Data.SqlClient;

public sealed class DataAccessDisconnected : IDisconnectedDataAccess
{
    private static readonly Lazy<DataAccessDisconnected> _instance =
        new(() => new DataAccessDisconnected());

    public static IDisconnectedDataAccess Instance => _instance.Value;

    private readonly SqlConnection _sqlConnection;
    private readonly ILogger _logger;

    private DataAccessDisconnected()
    {
        var conn = Environment.GetEnvironmentVariable("GYM_DB_CONNECTION");
        if (string.IsNullOrWhiteSpace(conn))
        {
            throw new InvalidOperationException(
                "Environment variable CHATBOTS_DB_CONNECTION is not set or is empty.");
        }

        _sqlConnection = new SqlConnection(conn);

        var factory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger("DataAccess");
    }

    public async Task<DataSet> Read(string query, string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
            throw new ArgumentException("El nombre de la tabla es obligatorio.", nameof(tableName));

        try
        {
            if (_sqlConnection.State == ConnectionState.Closed)
                await _sqlConnection.OpenAsync();

            var dataSet = new DataSet();
            await using var command = new SqlCommand(query, _sqlConnection);
            using var adapter = new SqlDataAdapter(command);

            adapter.Fill(dataSet, tableName);

            return dataSet;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al leer de la base de datos: {Message}", ex.Message);
            throw new DatabaseException("Error al leer de la base de datos.", ex);
        }
    }

    public async Task<int> Write(DataSet dataSet)
    {
        try
        {
            if (dataSet == null || dataSet.Tables.Count == 0)
                throw new ArgumentException("El DataSet está vacío.", nameof(dataSet));

            if (_sqlConnection.State == ConnectionState.Closed)
                await _sqlConnection.OpenAsync();

            var table = dataSet.Tables[0];
            if (string.IsNullOrWhiteSpace(table.TableName))
                throw new DatabaseException(
                    "La tabla del DataSet no tiene TableName. Asigne TableName antes de llamar a Write.",
                    null!);

            using var adapter =
                new SqlDataAdapter($"SELECT * FROM [{table.TableName}]", _sqlConnection);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            using var builder = new SqlCommandBuilder(adapter);

            var affectedRows = adapter.Update(table);
            return affectedRows;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error writing to database: {Message}", ex.Message);
            throw new DatabaseException("Error al escribir en la base de datos", ex);
        }
    }


    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            await _sqlConnection.OpenAsync();
            await _sqlConnection.CloseAsync();
            _logger.LogInformation("Connection to the database was successful.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to the database: {Message}", ex.Message);
            return false;
        }
    }
}