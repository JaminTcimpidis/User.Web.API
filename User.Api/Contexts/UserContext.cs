﻿using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Jamin.Users.Api.Models;
using System;
using System.Data;

namespace Jamin.Users.Api.Contexts
{
    public class UserContext
    {
        private readonly string _connectionString;

        public UserContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("GetAllUsers", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["task_id"]);
                        string firstName = reader["firstname"].ToString();
                        string lastName = reader["lastname"].ToString();
                        string email = reader["email"].ToString();

                        users.Add(new User(id, firstName, lastName, email));
                    }
                }
            }

            return users;
        }

        public int AddUser(User user)
        {
            int userId;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("AddUser", conn);
                cmd.Parameters.Add(new MySqlParameter("firstName", user.FirstName));
                cmd.Parameters.Add(new MySqlParameter("lastName", user.LastName));
                cmd.Parameters.Add(new MySqlParameter("email", user.Email));
                cmd.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int32).Direction = ParameterDirection.Output);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                userId = Convert.ToInt32(cmd.Parameters["id"].Value);
            }

            return userId;
        }
    }
}
