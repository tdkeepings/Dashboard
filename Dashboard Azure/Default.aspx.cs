using System;
using System.Web.Script.Serialization;
using System.Web.Services;
using Data;
using Model;

namespace Dashboard_Azure {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
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
    }
}