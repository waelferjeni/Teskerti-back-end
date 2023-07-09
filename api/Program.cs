using Data.Context;
using Data.Repository;
using Domain.Commands;
using Domain.Handler;
using Domain.Handlers;
using Domain.Interface;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin",
        options =>
        options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});



    builder.Services.AddTransient<IRepository<Ticket>, Repository<Ticket>>();
    builder.Services.AddTransient<IRequestHandler<PostCommand<Ticket>, string>, PostHandler<Ticket>>();
    builder.Services.AddTransient<IRequestHandler<PutCommand<Ticket>, string>, PutHandler<Ticket>>();
    builder.Services.AddTransient<IRequestHandler<DeleteCommand<Ticket>, string>, DeleteHandler<Ticket>>();
    builder.Services.AddTransient<IRequestHandler<GetListQuery<Ticket>, IEnumerable<Ticket>>, GetListHandler<Ticket>>();
    builder.Services.AddTransient<IRequestHandler<GetQuery<Ticket>, Ticket>, GetHandler<Ticket>>(); 
    builder.Services.AddTransient<IRepository<Employee>, Repository<Employee>>();
    builder.Services.AddTransient<IRequestHandler<PostCommand<Employee>, string>, PostHandler<Employee>>();
    builder.Services.AddTransient<IRequestHandler<PutCommand<Employee>, string>, PutHandler<Employee>>();
    builder.Services.AddTransient<IRequestHandler<DeleteCommand<Employee>, string>, DeleteHandler<Employee>>();
    builder.Services.AddTransient<IRequestHandler<GetListQuery<Employee>, IEnumerable<Employee>>, GetListHandler<Employee>>();
    builder.Services.AddTransient<IRequestHandler<GetQuery<Employee>, Employee>, GetHandler<Employee>>();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(GetListHandler<Ticket>).Assembly);
});
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddAutoMapper(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StagePerfContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
