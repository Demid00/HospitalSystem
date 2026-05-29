using Hospital.Domain.Repositories;
using HospitalSystem.Infrastructure.EntityFramework.Data;
using HospitalSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalSystem.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Регистрация репозиториев
        services.AddScoped<IPatientRepository, EfPatientRepository>();
        services.AddScoped<IDoctorRepository, EfDoctorRepository>();
        services.AddScoped<IMedicalRecordRepository, EfMedicalRecordRepository>();
        services.AddScoped<IAppointmentRepository, EfAppointmentRepository>();
        services.AddScoped<IReviewRepository, EfReviewRepository>();

        return services;
    }
}