using Dapper;
using EmailTracker.Repository.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmailTracker.Repository.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration configuration;

        public EmailRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Add(Core.Models.Email entity)
        {
            var sql = "Insert into [dbo].Email (Sender, Receiver, Subject, Body, Cc, Bcc, IsArchived, CreatedOnDate) VALUES (@Sender, @Receiver, @Subject, @Body, @Cc, @Bcc, @IsArchived, @CreatedOnDate)";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.QueryAsync<Core.Models.Email>(sql, entity);
        }

        public async Task Delete(int id)
        {
            var sql = "UPDATE [dbo].Email SET IsArchived = 1 WHERE Id = @Id";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.QueryAsync(sql, new { Id = id });
        }

        public async Task GetDeletedEmail(int id)
        {
            var sql = "GET * FROM [dbo].Email SET WHERE Id = @Id AND IsArchived = 1";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.QueryAsync(sql, new { Id = id });
        }

        public async Task Undelete(int id)
        {
            var sql = "UPDATE [dbo].Email SET IsArchived = 0 WHERE Id = @Id";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.QueryAsync(sql, new { Id = id });
        }

    }
}
