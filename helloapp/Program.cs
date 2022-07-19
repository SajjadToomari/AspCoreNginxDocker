using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
                     optional: true, reloadOnChange: true);

builder.Configuration.AddEnvironmentVariables();

if (args != null)
{
    builder.Configuration.AddCommandLine(args);
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
