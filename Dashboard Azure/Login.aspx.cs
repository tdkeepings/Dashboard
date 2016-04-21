using System;
using Data;
using Model;
using System.Collections.Specialized;

namespace Dashboard_Azure {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            
        }

        protected void LoginClicked(object sender, EventArgs e) {

            Account user = new Account();
            user.Name = UsernameTextBox.Text;
            user.Password = user.EncryptPassword(PasswordTextBox.Text);
            
            if (Database.Instance.AuthenticateUser(user)) {
                Session["User"] = user;
                Response.Redirect("/Home.aspx");
            } else {
                //Try again
                ErrorLabel.Text = "Incorrect details, please try again.";
            }
        }
    }
}