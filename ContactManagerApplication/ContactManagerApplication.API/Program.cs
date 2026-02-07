using ContactManagerApplication.API.Extensions;

namespace ContactManagerApplication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices();
            builder.Services.AddRepositories();
            builder.Services.AddDbContext(builder.Configuration);
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapControllerRoute(name: "default", pattern: "{controller=Contacts}/{action=Index}");

            app.Run();
        }
    }
}
