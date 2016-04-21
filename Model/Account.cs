using System.Collections.Generic;

namespace Model {
    public class Account {
        public string Name { get; set; }
        public string Password { get; set; }
        //CreateDate?
        public List<Column> Columns { get; set; }
        
        public string EncryptPassword(string password) {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string encryptedPassword = System.Text.Encoding.ASCII.GetString(data);

            return encryptedPassword;
        }
    }
}
