using Dapper;
using EmailTracker.Repository.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            var sql = "Insert into [dbo].Email (Sender, Receiver, EmailSubject, Body, Cc, Bcc, IsArchived, CreatedOnDate) VALUES (@Sender, @Receiver, @EmailSubject, @Body, @Cc, @Bcc, @IsArchived, @CreatedOnDate)";
            using IDbConnection connection  = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            await connection.ExecuteAsync(sql, entity);
            connection.Close();
        }

        public async Task Delete(int id)
        {
            var sql = "UPDATE [dbo].Email SET IsArchived = 1 WHERE Id = @Id";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            await connection.ExecuteAsync(sql, new { Id = id });
            connection.Close();
        }

        public async Task<IEnumerable<Core.Models.Email>> GetAllEmailsByEmailAddress(string senderEmailAddress)
        {
            var sql = "SELECT * FROM [dbo].Email WHERE Sender = @Sender";
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var result = await connection.QueryAsync<Core.Models.Email>(sql, new { Sender = senderEmailAddress });
            connection.Close();

            return result.ToList();
        }

       

    }
}
