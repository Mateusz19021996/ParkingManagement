using ParkingManagement.src.Seeding;
using ParkingManagement.src.infrastructure;
using ParkingManagement.src.application;


namespace ParkingManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services
                .AddApplicationServices()
                .AddInfrastructureServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
                seeder.Seed();
            }

            app.UseExceptionHandler(_ => { });

            app.MapControllers();

            app.Run();
        }
    }
}
