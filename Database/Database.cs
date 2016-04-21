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
        
        /// <summary>
        /// Authenticate a user when they log in
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AuthenticateUser(Account user) {
            SqlConnection conn = null;
            SqlDataReader rdr = null;
            bool authenticated = false;
            
            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_GetUserByUserDetails", conn);
                cmd.Parameters.Add(new SqlParameter("Username", user.Name));
                cmd.Parameters.Add(new SqlParameter("Password", user.Password));
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    int count = Convert.ToInt32(rdr["Count"].ToString());
                    if(count > 0) {
                        authenticated = true;
                    }
                }
            } catch (Exception ex) {
                conn.Close();
            } finally {
                conn.Close();
            }
            
            return authenticated;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CreateUser(Account user) {
            SqlConnection conn = null;
            
            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_InsertNewUser", conn);
                cmd.Parameters.Add(new SqlParameter("Username", user.Name));
                cmd.Parameters.Add(new SqlParameter("Password", user.Password));
                cmd.Parameters.Add(new SqlParameter("CreateDate", DateTime.Now));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BeginExecuteNonQuery();
                
            } catch (Exception ex) {
                conn.Close();
            } finally {
                conn.Close();
            }

            return true;
        }

        /// <summary>
        /// Get all of the column and site data
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get unique columns
        /// </summary>
        /// <returns></returns>
        public List<int> GetDistinctColumn() {
            SqlConnection conn = null;
            SqlDataReader rdr = null;
            List<int> ids = new List<int>();
            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_SelectDistinctColumns", conn);
                cmd.Parameters.Add(new SqlParameter("AccountId", 1));//TODO Change from 1! Need to suss out how to use Session instead
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                
                while (rdr.Read()) {
                    ids.Add(Convert.ToInt32(rdr["ColumnID"].ToString()));
                }
            } catch (Exception ex) {
                conn.Close();
            } finally {
                conn.Close();
            }

            return ids;
        }

        /// <summary>
        /// Get column by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get site by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a list of the sites that belong to a column with Id (param)
        /// </summary>
        /// <param name="columnId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a site to a column, by column name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnName"></param>
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

        /// <summary>
        /// Add a new column to the dashboard
        /// </summary>
        /// <param name="columnName"></param>
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

        /// <summary>
        /// Delete a site across all columns
        /// </summary>
        /// <param name="siteName"></param>
        public void DeleteSite(string siteName) {
            SqlConnection conn = null;

            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_DeleteSite", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("@SiteName", SqlDbType.VarChar).Value = siteName;

                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                conn.Close();
                throw;
            } finally {
                conn.Close();
            }
        }

        /// <summary>
        /// Delete a column and all associated sites
        /// </summary>
        /// <param name="columnName"></param>
        public void DeleteColumn(string columnName) {
            SqlConnection conn = null;

            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_DeleteColumn", conn);
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
