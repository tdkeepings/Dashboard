using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Model {
    public class Account {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Column> Columns { get; set; }
        public DateTime LastLoggedIn { get; set; }
        
        public string EncryptPassword(string password) {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string encryptedPassword = System.Text.Encoding.ASCII.GetString(data);

            return encryptedPassword;
        }

        /// <summary>
        /// Fully populate this Account with data from the database, using the users username
        /// </summary>
        public void PopulateUserDetails() {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azureDB"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_GetCountUserByUserDetails", conn);
                cmd.Parameters.Add(new SqlParameter("Username", this.Name));
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    int count = Convert.ToInt32(rdr["Count"].ToString());

                    this.ID = Convert.ToInt32(rdr["ID"].ToString());
                    this.Name = rdr["Username"].ToString();
                    this.Password = rdr["Password"].ToString();
                    this.CreateDate = Convert.ToDateTime(rdr["CreateDate"].ToString());
                    this.LastLoggedIn = Convert.ToDateTime(rdr["LastLoggedIn"].ToString());
                }
            } catch (Exception ex) {
                conn.Close();
            } finally {
                conn.Close();
            }

        }
    }
}
