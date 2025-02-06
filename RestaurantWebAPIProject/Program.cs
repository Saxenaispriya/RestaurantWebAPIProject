using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Routing.Matching;
using RestaurantWebAPIProject.BO.Implementation;
using RestaurantWebAPIProject.BO.Interface;
using RestaurantWebAPIProject.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRestaurantService, RestaurantService>();//add service class
builder.Services.AddSingleton<IDataStorageRepository,DataStorageRepository>();//for storage

//builder.Services.AddControllers()
//    .AddNewtonsoftJson(options =>
//    {
//        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
//        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
//    });

// Add services to the container.

builder.Services.AddControllers();

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            // Allow your frontend to make requests from localhost (adjust if needed)
            policy.WithOrigins("https://localhost:7156").AllowAnyHeader().AllowAnyMethod();// Your frontend's URL
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(logging =>
{
    // Customize HTTP logging here.
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("my-response-header");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

// Enable CORS globally
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
