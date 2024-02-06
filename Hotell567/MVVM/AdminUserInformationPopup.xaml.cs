using CommunityToolkit.Maui.Views;
using Hotell567.Models;

namespace Hotell567.MVVM;

public partial class AdminUserInformationPopup : Popup
{
    public User selectedUser { get; set; }

    public AdminUserInformationPopup(User user)
    {
        InitializeComponent();

        selectedUser = user;

        UserIdLabel.Text = "User ID: " + selectedUser.user_id;
        UserNameLabel.Text = "Username: " + selectedUser.username;
        FirstNameLabel.Text = "First name: " + selectedUser.first_name;
        LastNameLabel.Text = "Last name: " + selectedUser.last_name;
        EmailLabel.Text = "Email: " + selectedUser.email;
        PhoneNumberLabel.Text = "Phone: " + selectedUser.phone_number;
        AddressLabel.Text = "Address: " + selectedUser.address_line_1 + " " + selectedUser.address_line_2;
        CityLabel.Text = "City: " + selectedUser.city;
        StateLabel.Text = "State: " + selectedUser.state;
        PostalCodeLabel.Text = "Postal code: " + selectedUser.postal_code;
        CountryLabel.Text = "Country: " + selectedUser.country;
    }
}