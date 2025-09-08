using GymManager_DAL;
using GymManager.Forms;

namespace GymManager;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        var dataAccess = DataAccess.Instance;
        var connectionSuccessful = dataAccess.TestConnectionAsync();
        if (!connectionSuccessful.Result)
        {
            MessageBox.Show(
                "Cannot connect to the database. Please check your connection settings.",
                "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            Application.Run(new LoginForm());
        }
    }
}