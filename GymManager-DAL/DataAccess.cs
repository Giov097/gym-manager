using System.Data;
using Microsoft.Data.SqlClient;

namespace GymManager_DAL;

public sealed class DataAccess : IDataAccess
{
    private static readonly Lazy<DataAccess> _instance = new(() => new DataAccess());

    public static IDataAccess Instance => _instance.Value;

    private readonly SqlConnection _sqlConnection;
    private readonly ILogger _logger;

    private DataAccess()
    {
        var conn = Environment.GetEnvironmentVariable("GYM_DB_CONNECTION");
        if (string.IsNullOrWhiteSpace(conn))
        {
            throw new InvalidOperationException(
                "Environment variable GYM_DB_CONNECTION is not set or is empty.");
        }

        _sqlConnection = new SqlConnection(conn);

        var factory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger("DataAccess");
    }

    public async Task<DataSet> Read(string query)
    {
        try
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                await _sqlConnection.OpenAsync();
            }

            var dataSet = new DataSet();
            await using var command = new SqlCommand(query, _sqlConnection);
            using var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataSet);

            return dataSet;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al leer de la base de datos: {Message}", ex.Message);
            throw new DatabaseException("Error al leer de la base de datos.", ex);
        }
    }

    public async Task<object?> Write(string query)
    {
        if (_sqlConnection.State == ConnectionState.Closed)
        {
            await _sqlConnection.OpenAsync();
        }

        var transaction =
            await Task.Run(() =>
                _sqlConnection.BeginTransaction()
            );

        await using var command = new SqlCommand(query, _sqlConnection);
        command.Transaction = transaction;
        try
        {
            var result = await command.ExecuteScalarAsync();
            await Task.Run(() => transaction.Commit());
            return result;
        }
        catch (Exception ex)
        {
            await Task.Run(() => transaction.Rollback());
            _logger.LogError(ex, "Error al escribir en la base de datos: {Message}", ex.Message);
            throw new DatabaseException("Error al escribir en la base de datos.", ex);
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