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
                if (Session["User"] == null) {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        [WebMethod]
        public static string GetAllSites() {
            return Database.Instance.GetAllData();
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
            Database.Instance.DeleteSite(siteName);
        }

        [WebMethod]
        public static void DeleteColumn(string columnName) {
            Database.Instance.DeleteColumn(columnName);
        }
    }
}