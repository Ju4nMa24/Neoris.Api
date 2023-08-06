using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Neoris.Abstractions.Repositories.Account;
using Neoris.Abstractions.Repositories.Auth;
using Neoris.Abstractions.Repositories.Clients;
using Neoris.Abstractions.Repositories.Persons;
using Neoris.Abstractions.Repositories.Report;
using Neoris.Abstractions.Repositories.Transaction;
using Neoris.Abstractions.Types.Clients;
using Neoris.Abstractions.Types.Persons;
using Neoris.Business.Commands.Account;
using Neoris.Business.Commands.Auth;
using Neoris.Business.Commands.Client;
using Neoris.Business.Commands.Person;
using Neoris.Business.Commands.Report;
using Neoris.Business.Commands.Transaction;
using Neoris.Business.Processors.Account;
using Neoris.Business.Processors.Auth;
using Neoris.Business.Processors.Client;
using Neoris.Business.Processors.Person;
using Neoris.Business.Processors.Report;
using Neoris.Business.Processors.Transaction;
using Neoris.Commons.Repositories;
using Neoris.Commons.Types.Tables;
using Neoris.Repositories.Context;
using Neoris.Repositories.Services.Account;
using Neoris.Repositories.Services.Clients;
using Neoris.Repositories.Services.Persons;
using Neoris.Repositories.Services.Report;
using Neoris.Repositories.Services.Transaction;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NeorisContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#region ENTITIES AND REPOSITORIES
builder.Services.AddScoped<IClient, Client>();
builder.Services.AddScoped<IPerson, Person>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
#endregion
#region PROCCESORS
builder.Services.AddScoped<IRequestHandler<PersonCommand, PersonResponse>, PersonProcessor>();
builder.Services.AddScoped<IRequestHandler<ClientCommand, ClientResponse>, ClientProcessor>();
builder.Services.AddScoped<IRequestHandler<EditCommand, EditResponse>, EditProcessor>();
builder.Services.AddScoped<IRequestHandler<DeleteCommand, DeleteResponse>, DeleteProcessor>();
builder.Services.AddScoped<IRequestHandler<AccountCommand, AccountResponse>, AccountProcessor>();
builder.Services.AddScoped<IRequestHandler<ACEditCommand, AccountEditResponse>, ACEditProcessor>();
builder.Services.AddScoped<IRequestHandler<ACDeleteCommand, AccountDeleteResponse>, ACDeleteProcessor>();
builder.Services.AddScoped<IRequestHandler<TDeleteCommand, TDeleteResponse>, TDeleteProcessor>();
builder.Services.AddScoped<IRequestHandler<TEditCommand, TEditResponse>, TEditProcessor>();
builder.Services.AddScoped<IRequestHandler<TransactionCommand, TransactionResponse>, TransactionProcessor>();
builder.Services.AddScoped<IRequestHandler<AuthenticationCommand, AuthenticationResponse>, AuthProcessor>();
builder.Services.AddScoped<IRequestHandler<ReportCommand, ReportResponse>, ReportProcessor>();
#endregion
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
#region Json Web Token Configuration
#nullable disable
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.RequireHttpsMetadata = false;
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtParameters:SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = builder.Configuration["JwtParameters:Audience"],
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
#nullable enable
#endregion
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

app.Run();
