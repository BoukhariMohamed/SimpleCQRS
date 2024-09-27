using Microsoft.EntityFrameworkCore;
using SimpleCQRS.Application;
using SimpleCQRS.Infrastructure;
using SimpleCQRS.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<SimpleCqrsContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))); 



    //builder.Services.AddMediatR();
    builder.Services.RegisterApplication();
    builder.Services.RegisterInfrastructure();



    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();




    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
