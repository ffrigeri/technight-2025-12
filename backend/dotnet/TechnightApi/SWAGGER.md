# Swagger UI Configuration Guide

## 🎯 What's Installed

The TechnightApi project now includes full Swagger/OpenAPI support:

- **Swashbuckle.AspNetCore** (v10.0.1) - Complete Swagger toolchain
- **Microsoft.OpenApi** (v2.3.0) - OpenAPI specification support

## 🌐 Accessing Swagger UI

Once the API is running (`dotnet run`), navigate to:

```
http://localhost:5291/swagger
```

Or for different ports:
```
http://localhost:<your-port>/swagger
```

### What You'll See

Swagger UI provides:
- ✅ Interactive API documentation
- ✅ "Try it out" feature for testing endpoints
- ✅ Request/response schemas
- ✅ Example values
- ✅ Model definitions
- ✅ Authentication configuration (if enabled)

## 📋 Using Swagger UI

### 1. Browse Endpoints

All endpoints are organized by tags (e.g., "Examples", "Health")

### 2. Test an Endpoint

1. Click on any endpoint to expand it
2. Click the "Try it out" button
3. Fill in any required parameters
4. Click "Execute"
5. View the response below

### 3. View Schemas

Scroll down to the "Schemas" section to see all DTO and model definitions

## 🛠️ Current Configuration

The Swagger configuration in `Program.cs`:

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "TechnightApi",
        Version = "v1",
        Description = "A .NET 10 Web API with PostgreSQL and Entity Framework Core"
    });
});
```

Swagger UI configuration:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TechnightApi v1");
        options.RoutePrefix = "swagger";
        options.DocumentTitle = "TechnightApi - Swagger UI";
    });
}
```

## 🎨 Customization Options

### Adding More Metadata

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "TechnightApi",
        Version = "v1",
        Description = "Your custom description here",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new()
        {
            Name = "Your Name",
            Email = "your@email.com",
            Url = new Uri("https://yourwebsite.com")
        },
        License = new()
        {
            Name = "Use under MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});
```

### Enable XML Comments

1. Update your `.csproj` file:

```xml
<PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

2. Update `Program.cs`:

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "TechnightApi", Version = "v1" });
    
    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
```

3. Add XML comments to your code:

```csharp
/// <summary>
/// Creates a new example
/// </summary>
/// <param name="dto">The example data</param>
/// <returns>The created example</returns>
/// <response code="201">Returns the newly created example</response>
/// <response code="400">If the data is invalid</response>
```

### Customize Swagger UI Appearance

```csharp
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TechnightApi v1");
    options.RoutePrefix = "swagger";
    options.DocumentTitle = "TechnightApi - API Documentation";
    
    // Optional: Expand all operations by default
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    
    // Optional: Set default model rendering
    options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
    
    // Optional: Enable deep linking
    options.EnableDeepLinking();
    
    // Optional: Display request duration
    options.DisplayRequestDuration();
});
```

### Add Authentication to Swagger

If your API uses JWT authentication:

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "TechnightApi", Version = "v1" });
    
    options.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token"
    });
    
    options.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
```

## 📦 OpenAPI Specification

The raw OpenAPI/Swagger specification is available at:

```
http://localhost:5291/swagger/v1/swagger.json
```

You can use this JSON file to:
- Generate client SDKs
- Import into tools like Postman or Insomnia
- Share with frontend developers
- Generate documentation

## 🔧 Production Considerations

The current configuration only enables Swagger in Development mode:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(/* ... */);
}
```

For production:
- Consider removing Swagger entirely for security
- Or protect it with authentication
- Or host it on a separate documentation server

## 📚 Additional Resources

- [Swashbuckle Documentation](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [OpenAPI Specification](https://swagger.io/specification/)
- [Swagger UI Configuration](https://swagger.io/tools/swagger-ui/configuration/)

## 🐛 Troubleshooting

### Swagger page shows "No operations defined in spec!"

- Ensure your endpoints are mapped before `app.Run()`
- Check that endpoints have proper HTTP verb attributes

### Swagger JSON returns 404

- Verify the endpoint path matches your configuration
- Check that `app.UseSwagger()` is called

### Models not showing up correctly

- Ensure your DTOs/models are public
- Check that they're being used in endpoint signatures

