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
            var sql = "IF NOT EXISTS (SELECT Id FROM [dbo].[LabelEmail] WHERE LabelId = @LabelId AND EmailId = @EmailId) " +
                "INSERT INTO [dbo].LabelEmail (LabelId, EmailId, CreatedOnDate) VALUES (@LabelId, @EmailId, @CreatedOnDate)";
            using var connection = GetSqlConnection();
            await connection.QueryAsync<LabelEmail>(sql, new { LabelId = labelledEmail.LabelId, EmailId = labelledEmail.EmailId, CreatedOnDate = labelledEmail.CreatedOnDate });
        }

        public async Task Delete(int labelId, int emailId)
        {
            var sql = "IF EXISTS (SELECT Id FROM [dbo].[LabelEmail] WHERE LabelId = @LabelId AND EmailId = @EmailId) " +
                "DELETE FROM [dbo].LabelEmail WHERE EmailId = @EmailId AND LabelId = @LabelId";
            using var connection = GetSqlConnection();
            await connection.QueryAsync(sql, new { LabelId = labelId, EmailId = emailId });
        }

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
