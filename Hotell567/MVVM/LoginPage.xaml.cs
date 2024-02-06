using Hotell567.Logic;
using Hotell567.Models;
using System.Diagnostics;

namespace Hotell567.MVVM;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();

        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (AppManager.currentUser != null)
        {
            usernameEntry.Text = String.Empty;
            passwordEntry.Text = String.Empty;
            emailEntry.Text = String.Empty;
        }
    }

    // Adds to database
    private void LoginBtnClicked(object sender, EventArgs e)
    {
        
        if (AppManager.userFactory.CheckIfCredentialsAreCorrect(usernameEntry.Text, passwordEntry.Text) == true)
        {
            DisplayAlert("Success!", "Welcome " + usernameEntry.Text + "!", "OK");

            if (AppManager.userFactory.CheckAuthorityLevel(usernameEntry.Text) == 0)
            {
                AppManager.InitializePermissions(0);
            }
            else if (AppManager.userFactory.CheckAuthorityLevel(usernameEntry.Text) == 1)
            {
                AppManager.InitializePermissions(1);
            }

            int userId = AppManager.userDatabase.CheckDBForUser(usernameEntry.Text).user_id;
            User user = AppManager.userDatabase.GetUserById(userId);

            AppManager.InitializeUserData(user);

            Shell.Current.GoToAsync("//RoomsPage");
        }
        else
        {
            DisplayAlert("Error", "Please enter a valid username and password!", "OK");
        }
    }

    private void StartRegisteringBtnClicked(object sender, EventArgs e)
    {
        loginBtn.IsVisible = false;
        registerStartingBtn.IsVisible = false;
        emailEntry.IsVisible = true;
        registerBtn.IsVisible = true;

        usernameEntry.Text = String.Empty;
        passwordEntry.Text = String.Empty;

        loginBoxTitle.Text = "Register an Account!";
    }

    private void RegisterBtnClicked(object sender, EventArgs e)
    {
        if (AppManager.userFactory.CheckIfAccountFieldsAreValid(usernameEntry.Text, passwordEntry.Text,
                emailEntry.Text) == true)
        {
            if (AppManager.userFactory.CheckIfUsernameExists(usernameEntry.Text))
            {
                DisplayAlert("Error", "Username already exists", "OK");
                return;
            }

            User u = new User
            {
                username = usernameEntry.Text,
                password = passwordEntry.Text,
                email = emailEntry.Text
            };

            try
            {
                AppManager.userDatabase.AddUser(u);
                DisplayAlert("Success!", "Account created successfully", "OK");
                RegistrationComplete();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to create an account: " + ex);
            }
        }
        else
        {
            DisplayAlert("Error", "Please enter a valid username, password and email", "OK");
        }
    }

    private void RegistrationComplete()
    {
        loginBtn.IsVisible = true;
        registerStartingBtn.IsVisible = true;
        emailEntry.IsVisible = false;
        registerBtn.IsVisible = false;
        loginBoxTitle.Text = "Login to Your Account!";

        LoginBtnClicked(this, EventArgs.Empty);
    }
}