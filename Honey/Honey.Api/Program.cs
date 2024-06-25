using AutoMapper;
using Honey.BL.Authentication;
using Honey.BL.Authentication.Hashing;
using Honey.BL.Authentication.Options;
using Honey.BL.Authentication.RestoringPassword;
using Honey.BL.Authentication.Services;
using Honey.BL.Services;
using Honey.DB;
using Honey.DB.Repository.Order;
using Honey.DB.Repository.Product;
using Honey.DB.Repository.ProductType;
using Honey.DB.Repository.RestoringCode;
using Honey.DB.Repository.ShopCart;
using Honey.DB.Repository.User;
using Honey.Domain.Interfaces;
using Honey.Domain.Profiles;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Prng;
using ServiceCollectionExtensions = Honey.Extension.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Configure App Options
builder.Services.Configure<AppOptions>(builder.Configuration.GetSection(AppOptions.App));
var appOptions = builder.Configuration.GetSection(AppOptions.App).Get<AppOptions>();
builder.Services.AddSingleton(appOptions);
            
// Configure SmtpClient Options
builder.Services.Configure<SmtpClientOptions>(builder.Configuration.GetSection(SmtpClientOptions.SmtpClient));
var smtpClientOptions = builder.Configuration.GetSection(SmtpClientOptions.SmtpClient).Get<SmtpClientOptions>();
builder.Services.AddSingleton(smtpClientOptions);

builder.Services.AddCors(options =>
{ 
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Configure Authentication
ServiceCollectionExtensions.ConfigureAuthentication(builder.Services, builder.Configuration);
            
// Configure Swagger
ServiceCollectionExtensions.ConfigureSwagger(builder.Services);

// Configure Repositories & Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRestoringCodeRepository, RestoringCodeRepository>();

builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShopCartRepository, ShopCartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRestorePasswordService, RestorePasswordService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShopCartService, ShopCartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
            
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
//services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(connection));
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection,  
    x => x.MigrationsAssembly("Honey.DB")));
            
//Configure AutoMapper Profile
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AppProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
            
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers();});

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
    await context.Database.MigrateAsync();
}

app.Run();