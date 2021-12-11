using Dapper;
using EmailTracker.Core.Models;
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
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var receiverEmailAddresses = getEmailAddresses(entity.Receiver);
            var cCEmailAddresses = getEmailAddresses(entity.Cc);
            var bCCEmailAddresses = getEmailAddresses(entity.Bcc);
            var emailAddressesToSendTo = receiverEmailAddresses.Concat<string>(cCEmailAddresses).Concat<string>(bCCEmailAddresses).ToList();
            for (int i = 0; i < emailAddressesToSendTo.Count; i++)
            {
                await connection.ExecuteAsync(sql, entity);
            }
            connection.Close();
        }

        private List<string> getEmailAddresses(string addressString)
        {
            //Email addresses are separated by semi colon.
            var emailAddresses = new List<string>();
            if (!string.IsNullOrEmpty(addressString))
            {
                emailAddresses = addressString.Split(";").ToList();
            }
            return emailAddresses;
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

        public async Task<IEnumerable<Core.Models.Email>> GetAllDeletedEmails()
        {
            var sql = "SELECT * FROM [dbo].Email WHERE IsArchived = 1";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var result = await connection.QueryAsync<Core.Models.Email>(sql);
            connection.Close();
            return result.ToList();
        }

        public async Task UndeleteEmailById(int id)
        {
            var sql = "UPDATE [dbo].Email SET IsArchived = 0 WHERE Id = @Id";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            await connection.QueryAsync(sql, new { Id = id });
            connection.Close();
        }

        public async Task<IEnumerable<Core.Models.Email>> GetAllEmailsByLabelName(string labelName)
        {
            var sql = "SELECT * from [dbo].Email e " +
                "JOIN [dbo].LabelEmail le ON e.Id = le.EmailId " +
                "JOIN [dbo].Label l ON l.Id = le.LabelId " +
                "WHERE l.LabelName = @LabelName";
            using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var result = await connection.QueryAsync<Core.Models.Email>(sql, new { LabelName = labelName });
            connection.Close();
            return result.ToList();
        }

    }
}
