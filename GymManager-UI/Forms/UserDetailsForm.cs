using GymManager_BE;

namespace GymManager.Forms;

public partial class UserDetailsForm : Form
{
    public UserDetailsForm(User user)
    {
        InitializeComponent();

        txtId.Text = user.Id.ToString();
        txtFirstName.Text = user.FirstName;
        txtLastName.Text = user.LastName;
        txtEmail.Text = user.Email;
        lstRoles.Items.AddRange(user.UserRoles.Select(r => r.GetRoleName()).ToArray<object>());
        Text = $"{user.FirstName} {user.LastName} - {Lang.Details}";

    }

}