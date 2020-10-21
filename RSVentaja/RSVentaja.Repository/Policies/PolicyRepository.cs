using MySql.Data.MySqlClient;
using RSVentaja.Domain.Repository;
using RSVentaja.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RSVentaja.Repository.Policies
{
    public class PolicyRepository : IPolicyRepository
    {
        private string _connectionString;

        public PolicyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddPolicy(Policy policy, File file)
        {
            var changedLines = 0;
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = @"INSERT INTO Apolices (Nome, Placa, Inicio, Fim, Arquivo, nomeArquivo, idSeguradoras, boolCalculado) VALUES (@name, @plate, @startDate, @endDate, @file, @fileName, @insurerId, false)";
                    var cmd = new MySqlCommand(query, mySqlConnection);
                    var name = string.IsNullOrEmpty(policy.CustomerName.LastName) ? policy.CustomerName.FirstName : $"{policy.CustomerName.FirstName} { policy.CustomerName.LastName}";
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@plate", policy.AdditionalInfo);
                    cmd.Parameters.AddWithValue("@startDate", policy.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", policy.EndDate);
                    cmd.Parameters.AddWithValue("@file", file.FileData);
                    cmd.Parameters.AddWithValue("@fileName", file.FileName);
                    cmd.Parameters.AddWithValue("@insurerId", policy.InsurerId);
                    changedLines = await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    mySqlConnection.Close();
                }

            }
            return changedLines > 0;
        }

        public async Task<File> GetFile(int policyId)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = "SELECT nomeArquivo, Arquivo FROM Apolices where idApolices = @policyId";
                    MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                    cmd.Parameters.AddWithValue("@policyId", policyId);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        byte[] buffer = new byte[reader.GetBytes(1, 0L, null, 0, int.MaxValue)];
                        reader.GetBytes(1, 0L, buffer, 0, buffer.Length);
                        var file = new File(reader.GetString(0), buffer);
                        reader.Close();
                        return file;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    mySqlConnection.Close();
                }
            }
            return null;
        }

        public async Task<IEnumerable<Policy>> GetPolicies(string searchTerm, DateTime startDate, DateTime endDate)
        {
            List<Policy> policiesList = new List<Policy>();
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    string query =    @"SELECT a.idApolices, a.Nome, a.Placa, a.Inicio, a.Fim, a.boolCalculado, s.Nome FROM Apolices a INNER JOIN Seguradoras s ON a.idSeguradoras = s.idSeguradoras
                                        WHERE a.Nome LIKE @searchTerm
                                        AND a.Fim >= @startDate AND a.Fim <= @endDate
                                        ORDER BY a.Fim ASC";
                    MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                    searchTerm = string.Join('%', searchTerm.Split(' '));
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var newPolicy = new Policy(reader.GetInt32(0), new Name(reader.GetString(1)), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetString(6));
                        newPolicy.RenewalStarted = reader.GetBoolean(5);
                        newPolicy.AdditionalInfo = reader.GetString(2);
                        policiesList.Add(newPolicy);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    mySqlConnection.Close();
                }
            }
            return policiesList;
        }

        public async Task<bool> UpdateRenewalStarted (int policyId, bool status)
        {
            var changedLines = 0;
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    mySqlConnection.Open();
                    string query = @"UPDATE Apolices SET boolCalculado = @status WHERE idApolices = @policyId;";
                    var cmd = new MySqlCommand(query, mySqlConnection);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@policyId", policyId);
                    changedLines = await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    mySqlConnection.Close();
                }

            }
            return changedLines > 0;
        }
    }
}
