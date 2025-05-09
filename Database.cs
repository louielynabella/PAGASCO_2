using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace PAGASCO
{
    public class Database
    {
        private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\wayen\\OneDrive\\Documents\\Louielyn's random files\\PAGASCOOP2\\Abella, Wayen\\OOPAGASCODATABASE.accdb";
        private OleDbDataAdapter? da;
        private OleDbCommand? cmd;
        private DataSet? ds;
        private OleDbConnection? myConn;

        // Add public property to access the connection string
        public string ConnectionString => connectionString;

        // Verification codes storage
        public static Dictionary<string, (string code, DateTime expiry)> verificationCodes =
            new Dictionary<string, (string, DateTime)>();

        private string selectedFuelType = "";
        private int selectedBranchID = -1;


        public event Action? OnBranchUpdated;
        public event Action<int>? OnFuelStockUpdated;



        public Database()
        {
            myConn = new OleDbConnection(connectionString);
        }

        //BRANCH OPERATIONS


        public DataTable GetBranches()
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT branchID, branchName FROM Branch", connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

            public DataTable GetStocksByBranch(int branchID)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Stocks WHERE branchID = " + branchID, myConn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }

        public DataTable GetAllManagers()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM ManagerQuerry", myConn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public DataTable GetAllBranches()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Branch", myConn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public void UpdateBranchManager(int branchID, int newMID)
        {
            OleDbCommand cmd = new OleDbCommand("UPDATE Branch SET MID = ? WHERE branchID = ?", myConn);
            cmd.Parameters.AddWithValue("@MID", newMID);
            cmd.Parameters.AddWithValue("@branchID", branchID);
            myConn.Open();
            cmd.ExecuteNonQuery();
            myConn.Close();
        }

        public void UpdateManagerBranch(int mid, int branchID)
        {
            using (OleDbCommand cmd = new OleDbCommand("UPDATE ManagerQuerry SET branchID = ? WHERE MID = ?", myConn))
            {
                cmd.Parameters.AddWithValue("@branchID", branchID);
                cmd.Parameters.AddWithValue("@MID", mid);
                myConn.Open();
                cmd.ExecuteNonQuery();
                myConn.Close();
            }
        }

        public DataTable GetBranchByManagerID(int mid)
        {
            // Step 1: Get the branchID for this manager
            OleDbCommand cmd = new OleDbCommand("SELECT branchID FROM ManagerQuerry WHERE MID = ?", myConn);
            cmd.Parameters.AddWithValue("@MID", mid);

            myConn.Open();
            object result = cmd.ExecuteScalar();
            myConn.Close();

            if (result != null)
            {
                int branchID = Convert.ToInt32(result);

                // Step 2: Get the Branch info
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Branch WHERE branchID = " + branchID, myConn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetPricesByBranch(int branchID)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Prices WHERE branchID = " + branchID, myConn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public void UpdateStock(DataGridViewRow row)
        {
            string query = "UPDATE Stocks SET Diesel = ?, Premium = ?, Regular = ? WHERE branchID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, myConn))
            {
                decimal diesel = Convert.ToDecimal(row.Cells["Diesel"].Value);
                decimal premium = Convert.ToDecimal(row.Cells["Premium"].Value);
                decimal regular = Convert.ToDecimal(row.Cells["Regular"].Value);

                // Show a warning if values exceed 20000
                if (diesel > 20000 || premium > 20000 || regular > 20000)
                {
                    MessageBox.Show("Warning: One or more fuel stocks exceed the 20,000 liters limit.", "Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                cmd.Parameters.AddWithValue("?", diesel);
                cmd.Parameters.AddWithValue("?", premium);
                cmd.Parameters.AddWithValue("?", regular);
                cmd.Parameters.AddWithValue("?", row.Cells["branchID"].Value);

                myConn.Open();
                cmd.ExecuteNonQuery();
                myConn.Close();

                int branchID = Convert.ToInt32(row.Cells["branchID"].Value);
                CheckFuelThreshold("Diesel", diesel, branchID);
                CheckFuelThreshold("Premium", premium, branchID);
                CheckFuelThreshold("Regular", regular, branchID);
            }
        }



        public void CheckFuelThreshold(string fuelType, decimal liters, int branchID)
        {
            string managerEmail = GetManagerEmailByBranchID(branchID);

            if (liters <= 5000)
            {
                // Critical Warning - Only send email
                string message = $"WARNING: {fuelType} stock at branch {branchID} is critically low ({liters} liters remaining). Immediate refueling is required.";
                SendCustomEmail(managerEmail, $"{fuelType} CRITICAL LOW STOCK", message);
            }
            else if (liters >= 10000)
            {
                // High stock alert - Only send email
                string message = $"ALERT: {fuelType} stock at branch {branchID} is high ({liters} liters). Please monitor stock levels.";
                SendCustomEmail(managerEmail, $"{fuelType} HIGH STOCK ALERT", message);
            }
        }

        public void UpdatePrice(DataGridViewRow row)
        {
            string query = "UPDATE Prices SET dieselPrice = ?, premiumPrice = ?, regularPrice = ? WHERE branchID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, myConn))
            {
                cmd.Parameters.AddWithValue("?", row.Cells["dieselPrice"].Value);
                cmd.Parameters.AddWithValue("?", row.Cells["premiumPrice"].Value);
                cmd.Parameters.AddWithValue("?", row.Cells["regularPrice"].Value);
                cmd.Parameters.AddWithValue("?", row.Cells["branchID"].Value);
                myConn.Open();
                cmd.ExecuteNonQuery();
                myConn.Close();
            }
        }

        public void AddBranch(string branchName)
        {
            myConn.Open();

            // Insert into Branch
            string insertBranch = "INSERT INTO Branch (branchName) VALUES (?)";
            OleDbCommand cmd = new OleDbCommand(insertBranch, myConn);
            cmd.Parameters.AddWithValue("?", branchName);
            cmd.ExecuteNonQuery();

            // Get the new branch ID
            int newBranchId = 0;
            cmd = new OleDbCommand("SELECT MAX(branchID) FROM Branch", myConn);
            newBranchId = (int)cmd.ExecuteScalar();

            // Insert default stocks
            cmd = new OleDbCommand("INSERT INTO Stocks (branchID, Diesel, Premium, Regular) VALUES (?, 0, 0, 0)", myConn);
            cmd.Parameters.AddWithValue("?", newBranchId);
            cmd.ExecuteNonQuery();

            // Insert default prices
            cmd = new OleDbCommand("INSERT INTO Prices (branchID, dieselPrice, premiumPrice, regularPrice) VALUES (?, 0, 0, 0)", myConn);
            cmd.Parameters.AddWithValue("?", newBranchId);
            cmd.ExecuteNonQuery();

            myConn.Close();
        }



        // MANAGER ACCCOUNT OPERATIONS
        public bool RegisterManager(string managerName, string username, string password, string email)
        {
            string query = "INSERT INTO Manager (ManagerName, Username, [Password], Email) VALUES (?, ?, ?, ?)";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@p1", managerName);
                cmd.Parameters.AddWithValue("@p2", username);
                cmd.Parameters.AddWithValue("@p3", HashPassword(password));
                cmd.Parameters.AddWithValue("@p4", email);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Manager account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating manager account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }



        public bool RegisterManagerDetails(int mid, string managerName, string email, DateTime birthOfDate, string gender,
                                   string contactNumber, string address, string nationality, string educationBackground,
                                   string degree, string institution)
        {
            string query = "INSERT INTO ManagerDetails (MID, ManagerName, Email, BirthOfDate, Gender, ContactNumber, Adress, Nationality, EducationBackGround, Degree, [Institution/Univeresity]) " +
                           "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@p1", mid);
                cmd.Parameters.AddWithValue("@p2", managerName);
                cmd.Parameters.AddWithValue("@p3", email);
                cmd.Parameters.AddWithValue("@p4", birthOfDate);
                cmd.Parameters.AddWithValue("@p5", gender);
                cmd.Parameters.AddWithValue("@p6", contactNumber);
                cmd.Parameters.AddWithValue("@p7", address);
                cmd.Parameters.AddWithValue("@p8", nationality);
                cmd.Parameters.AddWithValue("@p9", educationBackground);
                cmd.Parameters.AddWithValue("@p10", degree);
                cmd.Parameters.AddWithValue("@p11", institution);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Manager details saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving manager details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }



        public void SendCustomEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    mail.From = new MailAddress("yenyown@gmail.com");
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = false;

                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential("yenyown@gmail.com", "hmja mlar ieyl ktlb");
                    smtpServer.EnableSsl = true;
                    smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpServer.Timeout = 30000; // 30 seconds timeout

                    smtpServer.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                MessageBox.Show($"SMTP Error: {ex.Message}\nStatus Code: {ex.StatusCode}\nPlease check your internet connection and try again.", "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}\nPlease check your internet connection and try again.", "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public string GetManagerEmailByBranchID(int branchID)
        {
            string query = "SELECT Manager.Email FROM (Branch INNER JOIN ManagerQuerry ON Branch.branchID = ManagerQuerry.branchID) " +
                           "INNER JOIN Manager ON ManagerQuerry.MID = Manager.MID WHERE Branch.branchID = ?";
            return ExecuteScalar(query, new OleDbParameter("@branchID", branchID))?.ToString() ?? "";
        }


        public int ValidateLogin(string username, string inputPassword)
        {
            string query = "SELECT MID, [Password] FROM ManagerQuerry WHERE Username = ?";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedHash = reader["Password"].ToString();
                        int mid = Convert.ToInt32(reader["MID"]);

                        if (VerifyPassword(inputPassword, storedHash))
                        {
                            return mid;
                        }
                    }
                }
            }

            return -1; // Invalid login
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
            int rowsAffected = ExecuteNonQuery(query,
                new OleDbParameter("@Password", hashedPassword),
                new OleDbParameter("@Username", username));

            return rowsAffected > 0;
        }

        // Verification operations
        public bool StartVerification(string username)
        {
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
            if (verificationCodes.TryGetValue(username, out var codeInfo))
            {
                if (DateTime.Now > codeInfo.expiry)
                {
                    verificationCodes.Remove(username);
                    MessageBox.Show("Verification code expired!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (codeInfo.code == inputCode)
                {
                    verificationCodes.Remove(username);
                    return true;
                }
            }
            MessageBox.Show("Invalid verification code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        // Helper methods
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

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == storedHash;
        }

        private bool IsValidUsername(string username)
        {
            return username.StartsWith("PAGASCO_") && username.Length > 9;
        }

        public bool ManagerExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Manager WHERE Username = ?";
            object result = ExecuteScalar(query, new OleDbParameter("@Username", username));
            return result != null && Convert.ToInt32(result) > 0;
        }

        public string GetManagerEmail(string username)
        {
            string query = "SELECT Email FROM Manager WHERE Username = ?";
            return ExecuteScalar(query, new OleDbParameter("@Username", username))?.ToString() ?? "";
        }

        private string GenerateVerificationCode()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        private bool SendVerificationEmail(string recipientEmail, string code)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    mail.From = new MailAddress("yenyown@gmail.com");
                    mail.To.Add(recipientEmail);
                    mail.Subject = "Password Reset Verification Code";
                    mail.Body = $"Your verification code is: {code}\nIt will expire in 5 minutes.";
                    mail.IsBodyHtml = false;

                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential("yenyown@gmail.com", "hmja mlar ieyl ktlb");
                    smtpServer.EnableSsl = true;
                    smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpServer.Timeout = 30000; // 30 seconds timeout

                    smtpServer.Send(mail);
                    MessageBox.Show("Verification code sent to email!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (SmtpException ex)
            {
                MessageBox.Show($"SMTP Error: {ex.Message}\nStatus Code: {ex.StatusCode}\nPlease check your internet connection and try again.", "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}\nPlease check your internet connection and try again.", "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Database helper methods
        private object ExecuteScalar(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        public int ExecuteNonQuery(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        private DataTable FillDataTable(string query, params OleDbParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }


        public List<string> GetAllManagerNames()
        {
            List<string> managerNames = new List<string>();
            try
            {
                string query = "SELECT ManagerName FROM Manager";
                DataTable dt = FillDataTable(query);

                foreach (DataRow row in dt.Rows)
                {
                    managerNames.Add(row["ManagerName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching manager names: " + ex.Message);
            }
            return managerNames;
        }

        //COSTUMER OPERATIONS

        public DataTable GetPricesByBranchCostumer(int branchID)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Prices WHERE branchID = " + branchID, myConn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public decimal GetFuelPrice(int branchID, string fuelType)
        {
            decimal price = 0;

            // Fix: Convert fuel type to matching column name
            string columnName = fuelType.ToLower() switch
            {
                "diesel" => "dieselPrice",
                "premium" => "premiumPrice",
                "regular" => "regularPrice",
                _ => throw new ArgumentException("Invalid fuel type")
            };

            string query = $"SELECT {columnName} FROM Prices WHERE branchID = @branchID";

            using (OleDbCommand cmd = new OleDbCommand(query, myConn))
            {
                cmd.Parameters.AddWithValue("@branchID", branchID);

                myConn.Open();
                object result = cmd.ExecuteScalar();
                myConn.Close();

                if (result != null && decimal.TryParse(result.ToString(), out price))
                {
                    return price;
                }
            }

            return price;
        }

        public bool DeductFuelStock(int branchID, string fuelType, decimal litersToDeduct)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // 1. Validate fuel type (matches column name)
                if (fuelType != "Diesel" && fuelType != "Premium" && fuelType != "Regular")
                {
                    MessageBox.Show("Invalid fuel type.");
                    return false;
                }

                // 2. Prepare SELECT query to get current stock for that branch and fuel type column
                string selectQuery = $"SELECT [{fuelType}] FROM Stocks WHERE branchID = ?";
                using (OleDbCommand selectCmd = new OleDbCommand(selectQuery, conn))
                {
                    selectCmd.Parameters.AddWithValue("?", branchID);
                    object result = selectCmd.ExecuteScalar();

                    if (result != null && decimal.TryParse(result.ToString(), out decimal currentStock))
                    {
                        if (currentStock >= litersToDeduct)
                        {
                            // 3. Deduct and update stock
                            string updateQuery = $"UPDATE Stocks SET [{fuelType}] = ? WHERE branchID = ?";
                            using (OleDbCommand updateCmd = new OleDbCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("?", currentStock - litersToDeduct);
                                updateCmd.Parameters.AddWithValue("?", branchID);
                                updateCmd.ExecuteNonQuery();
                                return true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Not enough fuel in stock.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fuel stock not found.");
                    }
                }
            }

            return false;
        }

        public void InsertSale(int branchID, string fuelType, decimal liters, decimal totalCost, DateTime date)
        {
            MessageBox.Show("Inserting sale..."); // For debugging

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO Sales (branchID, FuelType, QuantityLiters, TotalMoney, SaleDate) VALUES (?, ?, ?, ?, ?)";
                using (OleDbCommand cmd = new OleDbCommand(insertQuery, conn))
                {
                    cmd.Parameters.Add("branchID", OleDbType.Integer).Value = branchID;
                    cmd.Parameters.Add("FuelType", OleDbType.VarChar).Value = fuelType;
                    cmd.Parameters.Add("QuantityLiters", OleDbType.Double).Value = Convert.ToDouble(liters);
                    cmd.Parameters.Add("TotalMoney", OleDbType.Currency).Value = Convert.ToDecimal(totalCost); // or Convert.ToDouble(totalCost)
                    cmd.Parameters.Add("SaleDate", OleDbType.Date).Value = date;
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Sale inserted."); // For confirmation
        }


        //Sales for ManagerDashboard
        public DataTable GetAvailableSalesMonths(int branchID)
        {
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                "SELECT DISTINCT FORMAT(SaleDate, 'yyyy-mm') AS SaleMonth " +
                "FROM Sales WHERE branchID = " + branchID + " " +
                "ORDER BY FORMAT(SaleDate, 'yyyy-mm')", myConn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }


        public DataTable GetSalesByBranchFuelAndMonth(int branchID, string fuelType, string yearMonth)
        {
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                "SELECT * FROM Sales WHERE branchID = ? AND FuelType = ? AND FORMAT(SaleDate, 'yyyy-mm') = ?", myConn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@branchID", branchID);
                adapter.SelectCommand.Parameters.AddWithValue("@FuelType", fuelType);
                adapter.SelectCommand.Parameters.AddWithValue("@YearMonth", yearMonth);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public decimal GetTotalSalesForToday(int branchID)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                "SELECT SUM(TotalMoney) FROM Sales WHERE branchID = ? AND SaleDate = ?", myConn))
            {
                cmd.Parameters.AddWithValue("@branchID", branchID);
                cmd.Parameters.AddWithValue("@today", DateTime.Today);

                myConn.Open();
                object result = cmd.ExecuteScalar();
                myConn.Close();

                if (result != DBNull.Value)
                    return Convert.ToDecimal(result);
                else
                    return 0;
            }
        }

        public Dictionary<int, decimal> GetWeeklySalesByBranchFuelAndMonth(int branchID, string fuelType, string yearMonth)
        {
            Dictionary<int, decimal> weeklyLiters = new Dictionary<int, decimal>();

            string query = @"SELECT DatePart('ww', SaleDate) AS WeekNumber, 
                            SUM(QuantityLiters) AS TotalLiters
                     FROM Sales 
                     WHERE branchID = ? 
                       AND FuelType = ? 
                       AND FORMAT(SaleDate, 'yyyy-MM') = ?
                     GROUP BY DatePart('ww', SaleDate)";

            using (OleDbCommand cmd = new OleDbCommand(query, myConn))
            {
                cmd.Parameters.AddWithValue("?", branchID);
                cmd.Parameters.AddWithValue("?", fuelType);
                cmd.Parameters.AddWithValue("?", yearMonth);

                try
                {
                    myConn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int weekNumber = Convert.ToInt32(reader["WeekNumber"]);
                            decimal totalLiters = reader["TotalLiters"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["TotalLiters"]);
                            weeklyLiters[weekNumber] = totalLiters;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error getting weekly sales: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (myConn.State == ConnectionState.Open)
                        myConn.Close();
                }
            }

            return weeklyLiters;
        }


        public DataTable GetFuelSalesSummaryByMonth(int branchID, string yearMonth)
        {
            string query = @"SELECT FuelType, SUM(TotalMoney) AS TotalSales
                     FROM Sales 
                     WHERE branchID = ? AND FORMAT(SaleDate, 'yyyy-MM') = ?
                     GROUP BY FuelType";

            using (OleDbCommand cmd = new OleDbCommand(query, myConn))
            {
                cmd.Parameters.AddWithValue("?", branchID);
                cmd.Parameters.AddWithValue("?", yearMonth);

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }


        //total sales for manager
        // Total sales for manager (corrected version)
        public decimal GetTotalSalesForFuelAndMonth(int branchID, string fuelType, string month)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SUM(TotalMoney) FROM Sales WHERE branchID = ? AND FuelType = ? AND FORMAT(SaleDate, 'yyyy-MM') = ?";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@branchID", branchID);
                command.Parameters.AddWithValue("@fuelType", fuelType);
                command.Parameters.AddWithValue("@month", month);

                object result = command.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        // Total liters sold for manager (corrected version)
        public decimal GetTotalLitersSoldForFuelAndMonth(int branchID, string fuelType, string month)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SUM(QuantityLiters) FROM Sales WHERE branchID = ? AND FuelType = ? AND FORMAT(SaleDate, 'yyyy-MM') = ?";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@branchID", branchID);
                command.Parameters.AddWithValue("@fuelType", fuelType);
                command.Parameters.AddWithValue("@month", month);

                object result = command.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }


        //daabase
        // OWNER SALES METHODS

        public DataTable OwnerGetAvailableSalesYears(int branchID)
        {
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                "SELECT DISTINCT YEAR(SaleDate) AS SaleYear FROM Sales WHERE branchID = " + branchID + " ORDER BY YEAR(SaleDate)", myConn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable OwnerGetAvailableSalesMonths(int branchID, string year)
        {
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                "SELECT DISTINCT FORMAT(SaleDate, 'yyyy-MM') AS SaleMonth FROM Sales WHERE branchID = " + branchID + " AND FORMAT(SaleDate, 'yyyy') = ? ORDER BY FORMAT(SaleDate, 'yyyy-MM')", myConn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("?", year);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable GetSalesByBranchAndFuel(int branchID, string fuelType)
        {
            string query = "SELECT * FROM Sales WHERE branchID = ? AND FuelType = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, myConn))
            {
                cmd.Parameters.AddWithValue("?", branchID);
                cmd.Parameters.AddWithValue("?", fuelType);

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }



        public DataTable GetSalesByYearForBranch(int branchID, string year)
        {
            string query = "SELECT * FROM Sales WHERE branchID = ? AND YEAR(SaleDate) = ?";

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, myConn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("?", branchID);
                adapter.SelectCommand.Parameters.AddWithValue("?", year);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable GetSalesByMonthForBranch(int branchID, string year, string month)
        {
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                "SELECT * FROM Sales WHERE branchID = ? AND FORMAT(SaleDate, 'yyyy') = ? AND FORMAT(SaleDate, 'MM') = ?", myConn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("?", branchID);
                adapter.SelectCommand.Parameters.AddWithValue("?", year);
                adapter.SelectCommand.Parameters.AddWithValue("?", month);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllSalesYears()
        {
            string query = "SELECT DISTINCT YEAR(SaleDate) AS SaleYear FROM Sales ORDER BY YEAR(SaleDate)";
            return FillDataTable(query);
        }

        public DataTable GetBranchSalesByYear(string year)
        {
            string query = @"
                SELECT s.*, b.BranchName 
                FROM Sales s 
                INNER JOIN Branch b ON s.branchID = b.branchID 
                WHERE YEAR(SaleDate) = ? 
                ORDER BY s.SaleDate";

            using (OleDbCommand cmd = new OleDbCommand(query, myConn))
            {
                cmd.Parameters.AddWithValue("?", year);
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public List<(string managerName, string branchName)> GetManagersWithBranches()
        {
            List<(string managerName, string branchName)> managersWithBranches = new List<(string managerName, string branchName)>();
            string query = "SELECT ManagerQuerry.ManagerName, Branch.BranchName " +
                         "FROM ManagerQuerry LEFT JOIN Branch " +
                         "ON ManagerQuerry.branchID = Branch.branchID";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string managerName = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0);  // First column (ManagerName)
                        string branchName = reader.IsDBNull(1) ? "No Branch Assigned" : reader.GetString(1);  // Second column (BranchName)
                        managersWithBranches.Add((managerName, branchName));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting manager and branch information: " + ex.Message);
                }
            }
            return managersWithBranches;
        }




        public Dictionary<string, object> GetManagerDetailsFromTable(int mid)
        {
            string query = "SELECT * FROM ManagerDetails WHERE MID = ?";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", mid);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                            return null;

                        var row = dt.Rows[0];
                        var result = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)  
                        {
                            result[col.ColumnName] = row[col];
                        }
                        return result;
                    }
                }
            }
        }

        public Dictionary<string, object> GetManagerQueryDetails(int mid)
        {
            string query = "SELECT * FROM ManagerQuerry WHERE MID = ?";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", mid);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                            return null;

                        var row = dt.Rows[0];
                        var result = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            result[col.ColumnName] = row[col];
                        }
                        return result;
                    }
                }
            }
        }

        public Dictionary<string, object> GetBranchDetails(int branchID)
        {
            string query = "SELECT * FROM Branch WHERE branchID = ?";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", branchID);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                            return null;

                        var row = dt.Rows[0];
                        var result = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            result[col.ColumnName] = row[col];
                        }
                        return result;
                    }
                }
            }
        }

        public Dictionary<string, object> GetManagerFullDetails(int mid)
        {
            // Get details from ManagerQuery table first
            var queryDetails = GetManagerQueryDetails(mid);
            if (queryDetails == null)
            {
                MessageBox.Show($"No manager found with ID: {mid}");
                return null;
            }

            // Get details from ManagerDetails table
            var details = GetManagerDetailsFromTable(mid);

            // Get branch details if branchID exists
            Dictionary<string, object> branchDetails = null;
            if (queryDetails.ContainsKey("branchID") && queryDetails["branchID"] != DBNull.Value)
            {
                int branchID = Convert.ToInt32(queryDetails["branchID"]);
                branchDetails = GetBranchDetails(branchID);
            }

            // Combine all details
            var result = new Dictionary<string, object>();

            // Add ManagerQuery details
            foreach (var kvp in queryDetails)
            {
                result[kvp.Key] = kvp.Value;
            }

            // Add ManagerDetails if they exist
            if (details != null)
            {
                foreach (var kvp in details)
                {
                    result[kvp.Key] = kvp.Value;
                }
            }

            // Add Branch details if they exist
            if (branchDetails != null && branchDetails.ContainsKey("BranchName"))
            {
                result["BranchName"] = branchDetails["BranchName"];
            }
            else
            {
                result["BranchName"] = "No Branch Assigned";
            }

            return result;
        }

        public int GetManagerID(string username)
        {
            string query = "SELECT MID FROM Manager WHERE Username = ?";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    return -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error getting manager ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

    }
}





