﻿using System;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class PasswordChangeDAL
    {
        private readonly string _connectionString = "Data Source=DESKTOP-V9FJ71D\\SQLEXPRESS;Initial Catalog=HTS_BANK_FINAL;Integrated Security=True";

        public string GetCurrentPassword(int accountNo)
        {
            string currentPassword = ""; // Initialize to empty string

            // Query to retrieve the current password from the Accounts table
            string query = "SELECT Account_Password FROM Accounts WHERE Account_No = @AccountNo";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNo", accountNo);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        currentPassword = result.ToString(); // Assign retrieved password
                    }
                }
            }

            return currentPassword;
        }

        public string UpdatePassword(int accountNo, string newPassword)
        {
            // Query to update password in the Accounts table
            string query = "UPDATE Accounts SET Account_Password = @NewPassword WHERE Account_No = @AccountNo";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return "Success: Password updated successfully.";
                    }
                    else
                    {
                        return "Error: Password update failed.";
                    }
                }
            }
        }
    }
}
