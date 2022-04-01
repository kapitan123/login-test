namespace UserLoginFormTest.ProgramExtension;

public static class ProgramExtensions
{
    public static void AddDb(this WebApplicationBuilder builder)
    {
        var cs = builder.Configuration.GetConnectionString("SqlDb");
         
        var services = builder.Services;
        services.AddScoped(c => new AdoConnectionString(cs));

        services.AddScoped<ICreateCommand, CreateCommand>();
        services.AddScoped<IGetFirstQuery, GetFirstQuery>();
    }

    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<IUserRepository, UserRepositoryADO>();
        services.AddScoped<IEncryptionService, EncryptionService>();
    }
}
