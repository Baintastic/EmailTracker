using Dapper;
using EmailTracker.Core.Models;
using EmailTracker.Repository.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
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

        public async Task Add(LabelField entity)
        {
            var sql = "Insert into [dbo].Label (LabelName, CreatedOnDate) VALUES (@LabelName, @CreatedOnDate)";
            using var connection =  new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.QueryAsync<LabelField>(sql, entity);
        }

        public async Task Delete(int id)
        {
            var sql = "DELETE FROM [dbo].Label WHERE Id = @Id";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.QueryAsync(sql, new { Id = id });
        }
    }
}
