using Dapper;
using EmailTracker.Core.Models;
using EmailTracker.Repository.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTracker.Repository.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly IConfiguration configuration;

        public LabelRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Add(Label entity)
        {
            var sql = "IF NOT EXISTS (SELECT LabelName FROM [dbo].[Label] WHERE LabelName = @LabelName) " +
                     "INSERT INTO [dbo].Label (LabelName, CreatedOnDate) VALUES (@LabelName, @CreatedOnDate)";
            using var connection = GetSqlConnection();
            connection.Open();
            await connection.ExecuteAsync(sql, entity);
            connection.Close();
        }

        public async Task Delete(int id)
        {
            var sql = "IF EXISTS (SELECT Id FROM [dbo].[Label] WHERE Id = @Id) " +
                "DELETE FROM LabelEmail WHERE LabelId = @Id " +
                "DELETE FROM [dbo].Label WHERE Id = @Id";
            using var connection = GetSqlConnection();
            connection.Open();
            await connection.ExecuteAsync(sql, new { Id = id });
            connection.Close();
        }

        public async Task<IEnumerable<Label>> GetAll()
        {
            var sql = "SELECT * FROM [dbo].Label";
            using var connection = GetSqlConnection();
            connection.Open();
            var result = await connection.QueryAsync<Label>(sql);
            connection.Close();
            return result.ToList();
        }

        public async Task<Label> GetById(int id)
        {
            var sql = "SELECT * FROM [dbo].Label WHERE Id = @Id";
            using var connection = GetSqlConnection();
            connection.Open();
            var result = await connection.QuerySingleOrDefaultAsync<Label>(sql, new { Id = id });
            connection.Close();
            return result;
        }

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
