using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dashboard_Azure {
    public partial class Home : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                HttpCookie myCookie = new HttpCookie("User");
                myCookie = Request.Cookies["User"];

                if (myCookie == null) {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void LogoutOnCommand(object sender, EventArgs e) {
            HttpCookie myCookie = new HttpCookie("User");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            Response.Redirect("Login.aspx");
        }

        [WebMethod]
        public static string GetAllSites() {
            HttpCookie myCookie = new HttpCookie("User");
            myCookie = HttpContext.Current.Request.Cookies["User"];
            return Database.Instance.GetAllData(myCookie["User"]);
        }

        [WebMethod]
        public static void InsertSite(Site data, string columnName) {
            Database.Instance.InsertSiteToColumn(data, columnName);
        }

        [WebMethod]
        public static void InsertColumn(string columnName) {
            Database.Instance.InsertColumn(columnName);
        }

        [WebMethod]
        public static void DeleteSite(string siteName) {
            HttpCookie myCookie = new HttpCookie("User");
            myCookie = HttpContext.Current.Request.Cookies["User"];
            Database.Instance.DeleteSite(siteName, myCookie["User"]);
        }

        [WebMethod]
        public static void DeleteColumn(string columnName) {
            HttpCookie myCookie = new HttpCookie("User");
            myCookie = HttpContext.Current.Request.Cookies["User"];
            Database.Instance.DeleteColumn(columnName, myCookie["User"]);
        }
    }
}