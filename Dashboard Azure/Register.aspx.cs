using Data;
using Model;
using System;
using System.Collections.Specialized;
using System.Web;

namespace Dashboard_Azure {
    public partial class Register : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void RegisterClicked(object sender, EventArgs e) {
            NameValueCollection form = Request.Form;
            
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            string passwordConfirm = PasswordConfirmTextBox.Text;
            bool isValid = true;
            ErrorLabel.Text = "";

            if (username.Equals("")) {
                ErrorLabel.Text = "Username cannot be blank. ";
                isValid = false;
            }

            if (password.Equals("")) {
                ErrorLabel.Text += "Password cannot be blank. ";
                isValid = false;
            }

            if (passwordConfirm.Equals("")) {
                ErrorLabel.Text += "Password confirmation cannot be blank. ";
                isValid = false;
            }

            if (!password.Equals(passwordConfirm)) {
                ErrorLabel.Text += "Passwords must match. ";
                isValid = false;
            }

            if (Database.Instance.DoesUsernameExist(username)) {
                ErrorLabel.Text += "That username already exists. ";
                isValid = false;
            }


            if (isValid) {
                Account newUser = new Account();
                newUser.Name = username;
                newUser.Password = newUser.EncryptPassword(password);

                if (Database.Instance.CreateUser(newUser)) {
                    HttpCookie myCookie = new HttpCookie("User");
                    myCookie["User"] = newUser.Name;
                    myCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(myCookie);
                    Response.Redirect("/Default.aspx");
                } else {
                    ErrorLabel.Text += "Something bad happened. Don't worry, your data is safe :) ";
                }
            } else {

            }
        }
    }
}