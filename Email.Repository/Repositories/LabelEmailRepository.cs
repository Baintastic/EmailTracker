using Dapper;
using EmailTracker.Core;
using EmailTracker.Repository.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmailTracker.Repository.Repositories
{
    public class LabelEmailRepository : ILabelEmailRepository
    {
        private readonly IConfiguration configuration;

        public LabelEmailRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Add(LabelEmail labelledEmail)
        {
            var sql = "Insert into [dbo].LabelEmail (LabelId, EmailId, CreatedOnDate) VALUES (@LabelId, @EmailId, @CreatedOnDate)";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.QueryAsync<LabelEmail>(sql, labelledEmail);
        }

        public async Task Delete(int id)
        {
            var sql = "DELETE FROM [dbo].LabelEmail WHERE Id = @Id";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.QueryAsync(sql, new { Id = id });
        }
    }
}
