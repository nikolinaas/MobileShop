﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.View;

namespace WpfApp1.DataBase
{
    internal class DbMobileShop
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["hciMobileShop"].ConnectionString;
        public static List<string> ThemeAttributes = new List<string>();
        public static Account LoggedAccount;
        public static List<Employee> GetEmployees()
        {
            List<Employee> result = new List<Employee>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT *  FROM `zaposleni` ORDER BY Ime"; 
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Employee()
                {
                    UID = reader.GetString(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    DateOfBirth = reader.GetString(3)

                }); ;
            }
            reader.Close();
            conn.Close();
            return result;
        }

        public static List<Account> GetAccounts()
        {
            List <Account> result = new List<Account>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `nalog`";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(reader.GetString(3)!=null)
                result.Add(new Account()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Password = reader.GetString(2),
                    ThemeColor = reader.GetString(3),
                    ThemeFontSize = reader.GetString(4),
                    ThemeFontStyle = reader.GetString(5),
                    Language = reader.GetString(6)
                });

                
            }

            return result;
        }

        public static void createArticle(Article art)
        {

        }

        public static List<Article> GetArticlesByCategory(string category)
        {
            List<Article> result = new List<Article>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `artikal` WHERE `KategorijaArtikla` = '" + category + "'";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Article()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    NumberOfArticles = reader.GetInt32(2),
                    ArticleCategory = reader.GetString(3),
                    ArticlePrice = reader.GetInt32(4),
                    Varrranty = reader.GetInt32(5),
                  //  Photography = reader.GetString(6)
                   
                });
            }
            return result;
        }

        public static bool IsAdmin(Account acc)
        {

            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand mySqlCommand = con.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM `administrator`";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if(acc.Id == reader.GetInt32(4))
                {
                    return true;
                }
            }
            return false;

        }

        public static void changeThemeParameters()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
               
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("change_Theme_Parameters", connection))
                {
                   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    
                    cmd.Parameters.AddWithValue("@nalogID",LoggedAccount.Id);
                    cmd.Parameters.AddWithValue("@lang", ThemeAttributes[0]);
                    cmd.Parameters.AddWithValue("@color", ThemeAttributes[1]);
                    cmd.Parameters.AddWithValue("@fontsize", ThemeAttributes[2]);
                    cmd.Parameters.AddWithValue("@fontstyle", ThemeAttributes[3]);
                    Console.WriteLine(LoggedAccount.Id);
                    Console.WriteLine(ThemeAttributes[0]);
                    Console.WriteLine(ThemeAttributes[1]);
                    Console.WriteLine(ThemeAttributes[2]);
                    Console.WriteLine(ThemeAttributes[3]);
                    MySqlDataReader reader = cmd.ExecuteReader();


                  
                }
            }
        }

     
    }
}
