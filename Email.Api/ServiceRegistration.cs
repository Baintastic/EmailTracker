using EmailTracker.Repository.IRepositories;
using EmailTracker.Repository.Repositories;
using EmailTracker.Service.IServices;
using EmailTracker.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmailTracker.Api
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ILabelRepository, LabelRepository>();
            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<ILabelEmailRepository, LabelEmailRepository>();
            services.AddTransient<ILabelService, LabelService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ILabelEmailService, LabelEmailService>();
        }
    }
}
