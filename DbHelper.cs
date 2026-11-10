using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace PharmacyManagementSystem
{
    public class DbHelper
    {
        private static string dbFile = "PharmacyDB.sqlite";
        private static string connectionString = $"Data Source={dbFile};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        CREATE TABLE Users (
                            UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            Role TEXT NOT NULL
                        );
                        CREATE TABLE Medicines (
                            MedicineID INTEGER PRIMARY KEY AUTOINCREMENT,
                            MedicineName TEXT NOT NULL,
                            Price DECIMAL(10,2) NOT NULL,
                            Quantity INTEGER NOT NULL
                        );
                        CREATE TABLE Sales (
                            SaleID INTEGER PRIMARY KEY AUTOINCREMENT,
                            MedicineName TEXT NOT NULL,
                            Quantity INTEGER NOT NULL,
                            TotalPrice DECIMAL(10,2) NOT NULL,
                            SaleDate TEXT NOT NULL
                        );
                        INSERT INTO Users (Username, Password, Role) VALUES ('admin', 'admin123', 'Admin');
                        INSERT INTO Medicines (MedicineName, Price, Quantity) VALUES 
                        ('Panadol 500mg', 25.00, 100),
                        ('Amoxil 500mg', 120.00, 50),
                        ('Brufen 400mg', 45.00, 80),
                        ('Disprin 300mg', 15.00, 150);
                    ";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static DataTable ExecuteQuery(string query, SQLiteParameter[] parameters = null)
        {
            InitializeDatabase();
            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (var da = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string query, SQLiteParameter[] parameters = null)
        {
            InitializeDatabase();
            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
