using MySql.Data.MySqlClient;
using RSVentaja.Domain.Entity;
using RSVentaja.Domain.Repository;
using RSVentaja.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserTokenData> GetOrAddTokenByCredentials(string user, string password, string token)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    mySqlConnection.Open();                    
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mySqlConnection;
                    cmd.CommandText = "generate_token";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inguid", token);
                    cmd.Parameters.AddWithValue("@inusername", user);
                    cmd.Parameters.AddWithValue("@inpassword", password);
                    var reader = await cmd.ExecuteReaderAsync();
                    reader.Read();
                    if (reader.HasRows)
                    {                        
                        var name = reader.GetString(0);
                        token = reader.GetString(1);
                        reader.Close();
                        return new UserTokenData
                        {
                            Name = new Name(name),
                            Token = token,
                            Username = user
                        };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
                return null;
            }
        }

        public async Task<bool> ValidateToken(string token)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT 1 FROM Autenticacao WHERE guid = @guid");
                    cmd.Connection = mySqlConnection;
                    cmd.Parameters.AddWithValue("@guid", token);
                    var reader = await cmd.ExecuteReaderAsync();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        MySqlCommand cmdUpdate = new MySqlCommand("UPDATE Autenticacao SET createdAt = now() WHERE guid = @guid");
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return false;
            }
        }
    }
}


