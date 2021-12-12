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
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration configuration;

        public EmailRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Add(Email entity)
        {
            var sql = "INSERT INTO [dbo].Email (FromAddress, ToAddress, EmailSubject, Body, Cc, Bcc, IsArchived, CreatedOnDate) VALUES (@FromAddress, @ToAddress, @EmailSubject, @Body, @Cc, @Bcc, @IsArchived, @CreatedOnDate)";
            using var connection = GetSqlConnection();
            connection.Open();
            await connection.ExecuteAsync(sql, entity);
            connection.Close();
        }

        public async Task Delete(int id)
        {
            var sql = "UPDATE [dbo].Email SET IsArchived = 1 WHERE Id = @Id";
            using var connection = GetSqlConnection();
            connection.Open();
            await connection.ExecuteAsync(sql, new { Id = id });
            connection.Close();
        }

        public async Task<IEnumerable<Email>> GetAll()
        {
            var sql = "SELECT * FROM [dbo].Email WHERE IsArchived = 0";
            using var connection = GetSqlConnection();
            connection.Open();
            var result = await connection.QueryAsync<Email>(sql);
            connection.Close();
            return result.ToList();
        }

        public async Task<Email> GetById(int id)
        {
            var sql = "SELECT * FROM [dbo].Email WHERE Id = @Id";
            using var connection = GetSqlConnection();
            connection.Open();
            var result = await connection.QuerySingleOrDefaultAsync<Email>(sql, new { Id = id });
            connection.Close();
            return result;
        }

        public async Task UndeleteEmailById(int id)
        {
            var sql = "UPDATE [dbo].Email SET IsArchived = 0 WHERE Id = @Id";
            using var connection = GetSqlConnection();
            connection.Open();
            await connection.QueryAsync(sql, new { Id = id });
            connection.Close();
        }


        public async Task<IEnumerable<Email>> FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(string labelName, bool? isArchived, string fromAddress)
        {
            var sql = "SELECT * from [dbo].Email e " +
                "LEFT JOIN [dbo].LabelEmail le ON e.Id = le.EmailId " +
                "LEFT JOIN [dbo].Label l ON l.Id = le.LabelId " +
                "WHERE (@FromAddress IS NULL OR e.FromAddress = @FromAddress) " +
                "AND (@IsArchived IS NULL OR e.IsArchived = @IsArchived) " +
                "AND (@LabelName IS NULL OR l.LabelName = @LabelName) ";

            using var connection = GetSqlConnection();
            connection.Open();
            var result = await connection.QueryAsync<Email>(sql, new { LabelName = labelName, FromAddress = fromAddress, IsArchived = isArchived }); ;
            connection.Close();
            return result.ToList();
        }

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
