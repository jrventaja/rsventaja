using RSVentaja.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RSVentaja.Domain.Entity;
using MySql.Data.MySqlClient;

namespace RSVentaja.Repository.Insurers
{
    public class InsurerRepository : IInsurerRepository
    {
        private string _connectionString;

        public InsurerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddInsurer(string insurer)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    var query = "INSERT INTO Seguradoras (Nome) VALUES (@insurer);";
                    var cmd = new MySqlCommand(query, mySqlConnection);
                    cmd.Parameters.AddWithValue("@insurer", insurer);
                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Insurer>> GetInsurers()
        {
            var insurerList = new List<Insurer>();
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    var query = "SELECT idSeguradoras, Nome FROM Seguradoras;";
                    var cmd = new MySqlCommand(query, mySqlConnection);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        insurerList.Add(new Insurer
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            return insurerList;
        }
    }
}
