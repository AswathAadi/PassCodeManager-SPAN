using Microsoft.EntityFrameworkCore;
using PassCodeManager;
using PassCodeManager.Classified.DBcontext;
using PassCodeManager.Services;
using PassCodeManager.Services.Abstract;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services, builder.Environment);

var app = builder.Build();
startup.Configure(app, builder.Environment);