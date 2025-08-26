using System.Data;
using Microsoft.Data.SqlClient;

namespace GymManager_DAL;

public class DataAccess : IDataAccess
{
    //"Server=localhost,1433;Database=master;User Id=sa;Password=Passw0rd#2025;TrustServerCertificate=True;"
    private readonly SqlConnection _sqlConnection =
        new(
            "Server=localhost,1433;Database=GymManager;User Id=sa;Password=NuevoPass#2025;TrustServerCertificate=True;MultipleActiveResultSets=True");

    public async Task<DataSet> Read(string query)
    {
        try
        {
            var dataSet = new DataSet();

            await _sqlConnection.OpenAsync();
            await using var command = new SqlCommand(query, _sqlConnection);
            using var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataSet);

            return dataSet;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public async Task<object?> Write(string query)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            await using var command = new SqlCommand(query, _sqlConnection);
            return await command.ExecuteScalarAsync();
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public async Task<bool> TestConnectionAsync()
    {
        using var factory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = factory.CreateLogger("Program");
        try
        {
            await _sqlConnection.OpenAsync();
            await _sqlConnection.CloseAsync();
            logger.LogInformation("Connection to the database was successful.");
            return true;
        }
        catch (Exception e)
        {
            logger.LogError("Failed to connect to the database: {Message} {Exception}", e.Message,
                e);
            return false;
        }
    }
}