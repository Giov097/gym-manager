using System.Data;
using Microsoft.Data.SqlClient;

namespace GymManager_DAL;

public sealed class DataAccessConnected : IDataAccess
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    public DataAccessConnected()
    {
        _connectionString = Environment.GetEnvironmentVariable("GYM_DB_CONNECTION")
                            ?? throw new InvalidOperationException(
                                "La variable de entorno GYM_DB_CONNECTION no está configurada.");

        var factory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger("DataAccessConnected");
    }

    public async Task<DataSet> Read(string query)
    {
        var dataSet = new DataSet();
        try
        {
            var dataTable = new DataTable();
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();
            dataTable.Load(reader);

            dataSet.Tables.Add(dataTable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al leer de la base de datos: {Message}", ex.Message);
            throw new DatabaseException("Error al leer de la base de datos.", ex);
        }

        return dataSet;
    }

    public async Task<object?> Write(string query)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var transaction = await Task.Run(() => connection.BeginTransaction());
        await using var command = new SqlCommand(query, connection, transaction);
        try
        {
            var result = await command.ExecuteScalarAsync();
            await Task.Run(() => transaction.Commit());
            return result;
        }
        catch (Exception ex)
        {
            await Task.Run(() => transaction.Rollback());
            _logger.LogError(ex, "Error writing to database: {Message}", ex.Message);
            throw new DatabaseException("Error al escribir en la base de datos.", ex);
        }
    }

    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.CloseAsync();
            _logger.LogInformation("Connection successful.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error connecting to database: {Message}", ex.Message);
            return false;
        }
    }
}