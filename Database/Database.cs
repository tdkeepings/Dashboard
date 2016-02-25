using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Web.Script.Serialization;

namespace Data {
    public class Database {

        private static Database database;
        
        public static Database Instance {
            get {
                if (database == null) {
                    database = new Database();
                }

                return database;
            }
        }

        

        public string GetAllData() {
            List<int> ids = GetDistinctColumn();
            List<Column> columns = new List<Column>();
            foreach(int i in ids) {
                Column col = new Column();
                col = GetColumnById(i);
                
                List<int> siteIds = GetSiteIDsForColumn(i);
                foreach(int siteId in siteIds) {
                    col.Sites.Add(GetSiteById(siteId));
                }
                columns.Add(col);
            }

            string json = new JavaScriptSerializer().Serialize(columns);

            return json;
        }

        public List<int> GetDistinctColumn() {
            SqlConnection conn = null;
            SqlDataReader rdr = null;
            List<int> ids = new List<int>();
            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_SelectDistinctColumns", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                
                while (rdr.Read()) {
                    ids.Add(Convert.ToInt32(rdr["ID"].ToString()));
                }
            } catch (Exception ex) {
                conn.Close();
            } finally {
                conn.Close();
            }

            return ids;
        }

        public Column GetColumnById(int id) {
            Column col = new Column();

            SqlConnection conn = null;
            SqlDataReader rdr = null;
            List<int> ids = new List<int>();
            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_SelectColumnById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@columnId", SqlDbType.VarChar).Value = id;
                rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    col.Id = Convert.ToInt32(rdr["ID"].ToString());
                    col.Name = rdr["Name"].ToString();
                }
            } catch (Exception ex) {
                conn.Close();
                throw;
            } finally {
                conn.Close();
            }


            return col;
        }

        public Site GetSiteById(int id) {
            Site site = new Site();

            SqlConnection conn = null;
            SqlDataReader rdr = null;
            List<int> ids = new List<int>();
            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_SelectSiteById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@siteId", SqlDbType.Int).Value = id;
                rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    site.Id = Convert.ToInt32(rdr["ID"].ToString());
                    site.Name = rdr["Name"].ToString();
                    site.Url = rdr["Url"].ToString();
                    site.BgColour = rdr["BgColour"].ToString();
                    site.Colour = rdr["Colour"].ToString();
                }
            } catch (Exception ex) {
                conn.Close();
                throw;
            } finally {
                conn.Close();
            }

            return site;
        }

        public List<int> GetSiteIDsForColumn(int columnId) {
            
            SqlConnection conn = null;
            SqlDataReader rdr = null;
            List<int> ids = new List<int>();
            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_SelectSitesForColumn", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@columnId", SqlDbType.Int).Value = columnId;
                rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    ids.Add(Convert.ToInt32(rdr["SiteID"].ToString()));
                }
            } catch (Exception ex) {
                conn.Close();
                throw;
            } finally {
                conn.Close();
            }

            return ids;
        }


        public void InsertSiteToColumn(Site data, string columnName) {
            SqlConnection conn = null;
            
            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_InsertSite", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar).Value = columnName;
                cmd.Parameters.Add("@SiteName", SqlDbType.VarChar).Value = data.Name;
                cmd.Parameters.Add("@SiteUrl", SqlDbType.VarChar).Value = data.Url;
                cmd.Parameters.Add("@SiteBgColour", SqlDbType.VarChar).Value = data.BgColour;
                cmd.Parameters.Add("@SiteColour", SqlDbType.VarChar).Value = data.Colour;
                
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                throw;
            } finally {
                conn.Close();
            }
        }


        public void InsertColumn(string columnName) {
            SqlConnection conn = null;

            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_InsertColumn", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar).Value = columnName;
                
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                throw;
            } finally {
                conn.Close();
            }
        }
    }
}
