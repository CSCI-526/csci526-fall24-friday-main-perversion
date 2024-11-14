using System;
using Microsoft.Data.SqlClient;


public class DatabaseHelper
{
    string connectionString = "Server=sql.freedb.tech;Database=freedb_data526;User Id=freedb_yyc526;Password=*kFF32TmPT6w9cD;";

    // Method to increment 'times' in the database
    public void InsertData(int level, float seconds)
    {
        string updateQuery = "INSERT INTO GameStats(Levels, Seconds) VALUES(@Levels, @Seconds)";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@Levels", level);
                command.Parameters.AddWithValue("@Seconds", seconds);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) updated.");
            }
        }
    }
}


