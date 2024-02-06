using Hotell567.Logic;
using Hotell567.Models;
using System.Diagnostics;

namespace Hotell567.MVVM;

public partial class AccountSettings : ContentPage
{
    public User CurrentUser { get; set; }

    public AccountSettings()
    {
        InitializeComponent();
        UpdateCurrentUser();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateCurrentUser();
    }

    public void UpdateCurrentUser()
    {
        CurrentUser = AppManager.currentUser;

        Username.Text = CurrentUser.username;
        Password.Text = CurrentUser.password;
        Email.Text = CurrentUser.email;
        FirstName.Text = CurrentUser.first_name;
        LastName.Text = CurrentUser.last_name;
        if (CurrentUser.phone_number != 0)
        {
            PhoneNumber.Text = CurrentUser.phone_number.ToString();
        }
        else
        {
            PhoneNumber.Text = String.Empty;
        }
        AddressLine1.Text = CurrentUser.address_line_1;
        AddressLine2.Text = CurrentUser.address_line_2;
        City.Text = CurrentUser.city;
        State.Text = CurrentUser.state;
        if (CurrentUser.postal_code != 0)
        {
            PostalCode.Text = CurrentUser.postal_code.ToString();
        }
        else
        {
            PostalCode.Text = String.Empty;
        }
        Country.Text = CurrentUser.country;
    }

    private void SaveBtnClicked(object sender, EventArgs e)
    {
        if (CheckIfFieldsAreCorrect() == false) return;

        
        try
        {
            User editedUser = new User
            {
                user_id = CurrentUser.user_id,
                username = Username.Text,
                password = Password.Text,
                email = Email.Text,
                first_name = FirstName.Text,
                last_name = LastName.Text,
                phone_number = int.Parse(PhoneNumber.Text),
                address_line_1 = AddressLine1.Text,
                address_line_2 = AddressLine2.Text,
                city = City.Text,
                state = State.Text,
                postal_code = int.Parse(PostalCode.Text),
                country = Country.Text
            };

            AppManager.userDatabase.EditUser(editedUser);

            User updatedUser = AppManager.userDatabase.GetUserById(editedUser.user_id);
            AppManager.InitializeUserData(updatedUser);
            UpdateCurrentUser();

            DisplayAlert("Account updated", "YAY", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Failed to update account!", "OK");
            Debug.WriteLine("Failed to update account: " + ex.Message);
        }
    }

    private void LogOutBtnClicked(object sender, EventArgs e)
    {
        AppManager.LogUserOut();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ResetBtnClicked(object sender, EventArgs e)
    {
        UpdateCurrentUser();
    }

    private bool CheckIfFieldsAreCorrect()
    {
        if (string.IsNullOrEmpty(Username.Text))
        {
            DisplayAlert("Error", "Please enter a username", "OK");
            return false;
        }

        if (string.IsNullOrEmpty(Password.Text))
        {
            DisplayAlert("Error", "Please enter a password", "OK");
            return false;
        }

        if (string.IsNullOrEmpty(Email.Text))
        {
            DisplayAlert("Error", "Please enter a proper email", "OK");
            return false;
        }

        if (AppManager.userFactory.CheckIfUsernameExists(Username.Text) && Username.Text != CurrentUser.username)
        {
            DisplayAlert("Error", "This username already exists", "OK");
            return false;
        }

        if (AppManager.userFactory.IsValidUsername(Username.Text) == false)
        {
            DisplayAlert("Error", "Your username has to be at least 4 characters long!", "Oopsie!");
            return false;
        }

        if (AppManager.userFactory.IsValidPassword(Password.Text) == false)
        {
            DisplayAlert("Error", "Your password has to be at least 8 characters long!", "Oeh..");
            return false;
        }

        if (AppManager.userFactory.IsValidEmail(Email.Text) == false)
        {
            DisplayAlert("Error", "Your email is not valid!", "Rrr..");
            return false;
        }

        if (AppManager.userFactory.IsValidPhoneNumber(PhoneNumber.Text) == false)
        {
            DisplayAlert("Error", "Your phone number is not valid! It must be made of numbers and at least 5 digits long!", "Rrr..");
            return false;
        }

        if (string.IsNullOrEmpty(City.Text))
        {
            DisplayAlert("Error", "Please enter a city", "OK");
            return false;
        }

        if (string.IsNullOrEmpty(State.Text))
        {
            DisplayAlert("Error", "Please enter a state", "OK");
            return false;
        }

        if (string.IsNullOrEmpty(Country.Text))
        {
            DisplayAlert("Error", "Please enter a country", "OK");
            return false;
        }

        return true;
    }
}