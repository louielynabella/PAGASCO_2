using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;


namespace PAGASCO
{
    internal class Database
    {
        private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= C:\\Users\\wayen\\OneDrive\\Documents\\Louielyn's random files\\PAGASCOOP2\\Abella, Wayen\\OOPAGASCODATABASE.accdb";
        OleDbDataAdapter? da;
        OleDbCommand? cmd;
        DataSet? ds;
        OleDbConnection? myConn;

       
        //newAdded
        public static Dictionary<string, (string code, DateTime expiry)> verificationCodes = new Dictionary<string, (string, DateTime)>();

        public Database()
        {
            myConn = new OleDbConnection(connectionString);
        }


        public void testconnection()
        {
            myConn = new OleDbConnection(connectionString);
            ds = new DataSet();
            myConn.Open();
            MessageBox.Show("Connected Successfully!");
            myConn.Close();
        }

        public List<string> GetBranchNames()
        {
            List<string> branchNames = new List<string>();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("SELECT BranchName FROM InventoryQuery", connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        branchNames.Add(reader["BranchName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching branch names: " + ex.Message);
            }

            return branchNames;
        }


        public int GetBranchID(string branchName)
        {
            int branchID = -1; // Default value if no branch is found

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("SELECT BranchID FROM InventoryQuery WHERE BranchName = @BranchName", connection);
                    command.Parameters.AddWithValue("@BranchName", branchName);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        branchID = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching branch ID: " + ex.Message);
            }

            return branchID;
        }

        public DataTable GetFuelStocks(int branchID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("SELECT * FROM Stocks WHERE BranchID = @BranchID", connection);
                    command.Parameters.AddWithValue("@BranchID", branchID);

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching fuel stocks: " + ex.Message);
            }

            return dt;
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool IsValidUsername(string username)
        {
            return username.StartsWith("PAGASCO_") && username.Length > 9;
        }

        public bool ManagerExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Manager WHERE Username = ?";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public string GetManagerEmail(string username)
        {
            string email = "";
            string query = "SELECT Email FROM Manager WHERE Username = ?";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    email = result.ToString();
                }
            }
            return email;
        }

        public bool RegisterManager(string managerName, string username, string password, string email)
        {
            if (!IsValidUsername(username))
            {
                MessageBox.Show("Username must be in format 'PAGASCO_Surname'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (ManagerExists(username))
            {
                MessageBox.Show("Manager already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string hashedPassword = HashPassword(password);
            string query = "INSERT INTO Manager (ManagerName, Username, [Password], Email) VALUES (?, ?, ?, ?)";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                // Add parameters in exact order of question marks (?)
                cmd.Parameters.AddWithValue("?", managerName);
                cmd.Parameters.AddWithValue("?", username);
                cmd.Parameters.AddWithValue("?", hashedPassword);
                cmd.Parameters.AddWithValue("?", email);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Manager account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to create manager account!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInput = HashPassword(inputPassword); // Assuming you have HashPassword method
            return hashedInput == storedHash;
        }

        public bool ValidateLogin(string username, string password)
        {
            string query = "SELECT Password FROM Manager WHERE Username = ?";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string storedHashedPassword = result.ToString();

                    // Check password (Assuming you hash passwords)
                    return VerifyPassword(password, storedHashedPassword);
                }
                else
                {
                    return false; // Username not found
                }
            }
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            if (!ManagerExists(username))
            {
                MessageBox.Show("Manager does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string hashedPassword = HashPassword(newPassword);
            string query = "UPDATE Manager SET [Password] = ? WHERE Username = ?";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Password", hashedPassword); // First ?
                cmd.Parameters.AddWithValue("@Username", username);       // Second ?

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public string GenerateVerificationCode()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString(); // 6-digit
        }

        public bool SendVerificationEmail(string recipientEmail, string code)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("yenyown@gmail.com"); // your sender email
                mail.To.Add(recipientEmail);
                mail.Subject = "Password Reset Verification Code";
                mail.Body = $"Your verification code is: {code}\nIt will expire in 5 minutes.";

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("yenyown@gmail.com", "hmja mlar ieyl ktlb"); // Insert your password/app password
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                MessageBox.Show("Verification code sent to email!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}");
                return false;
            }
        }

        public bool StartVerification(string username)
        {
            if (!ManagerExists(username))
            {
                MessageBox.Show("Manager does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string email = GetManagerEmail(username);
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email not found for user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string code = GenerateVerificationCode();
            DateTime expiry = DateTime.Now.AddMinutes(5);

            verificationCodes[username] = (code, expiry);
            return SendVerificationEmail(email, code);
        }

        public bool VerifyCode(string username, string inputCode)
        {
            if (verificationCodes.ContainsKey(username))
            {
                var (storedCode, expiry) = verificationCodes[username];

                if (DateTime.Now > expiry)
                {
                    verificationCodes.Remove(username);
                    MessageBox.Show("Verification code expired!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (storedCode == inputCode)
                {
                    verificationCodes.Remove(username); // Clear after use
                    return true;
                }
            }
            MessageBox.Show("Invalid verification code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        // Fetch all branches for ComboBox
       

       
       




       

    }
}
