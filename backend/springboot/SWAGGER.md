# Swagger UI Configuration Guide

## 🎯 What's Installed

The TechnightApi Spring Boot project includes full Swagger/OpenAPI support:

- **SpringDoc OpenAPI** (v2.7.0) - Complete OpenAPI 3.0 support
- **Swagger UI** - Interactive API documentation interface

## 🌐 Accessing Swagger UI

Once the API is running (`mvn spring-boot:run` or `./run.sh`), navigate to:

```
http://localhost:8080/swagger
```

Or for different ports:
```
http://localhost:<your-port>/swagger
```

### What You'll See

Swagger UI provides:
- ✅ Interactive API documentation
- ✅ "Try it out" feature for testing endpoints
- ✅ Request/response schemas with examples
- ✅ Automatic validation of request bodies
- ✅ Model definitions with field descriptions
- ✅ Response examples for each endpoint

## 📋 Using Swagger UI

### 1. Browse Endpoints

All endpoints are organized by tags:
- **Health** - Health check endpoint
- **Examples** - CRUD operations for examples

### 2. Test an Endpoint

1. Click on any endpoint to expand it
2. Click the "Try it out" button
3. Fill in any required parameters or request body
4. Click "Execute"
5. View the response, status code, and headers below

### 3. View Schemas

Scroll down to the "Schemas" section at the bottom to see all DTO and model definitions with:
- Field names and types
- Validation constraints
- Field descriptions
- Example values

## 🛠️ Current Configuration

### OpenAPI Configuration

The OpenAPI configuration is defined in `OpenApiConfig.java`:

```java
@Configuration
public class OpenApiConfig {
    @Bean
    public OpenAPI technightOpenAPI() {
        return new OpenAPI()
            .info(new Info()
                .title("TechnightApi")
                .description("A Spring Boot API with PostgreSQL and JPA")
                .version("v1")
                .contact(new Contact()
                    .name("Technight Team")
                    .email("info@technight.com"))
                .license(new License()
                    .name("MIT License")
                    .url("https://opensource.org/licenses/MIT")));
    }
}
```

### Application Properties

Swagger UI settings in `application.properties`:

```properties
# OpenAPI docs endpoint
springdoc.api-docs.path=/api/openapi

# Swagger UI path
springdoc.swagger-ui.path=/swagger

# Sort operations by HTTP method
springdoc.swagger-ui.operationsSorter=method

# Sort tags alphabetically
springdoc.swagger-ui.tagsSorter=alpha

# Don't expand operations by default
springdoc.swagger-ui.doc-expansion=none
```

## 🎨 Customization Options

### Adding More Metadata to Endpoints

Use annotations in your controllers:

```java
@Operation(
    summary = "Create a new example",
    description = "Creates a new example record in the database"
)
@ApiResponses(value = {
    @ApiResponse(
        responseCode = "201",
        description = "Example created successfully",
        content = @Content(schema = @Schema(implementation = Example.class))
    ),
    @ApiResponse(
        responseCode = "400",
        description = "Invalid input data"
    )
})
@PostMapping
public ResponseEntity<Example> createExample(@Valid @RequestBody CreateExampleDto dto) {
    // implementation
}
```

### Adding Parameter Descriptions

```java
@GetMapping("/{id}")
public ResponseEntity<?> getExampleById(
    @Parameter(description = "ID of the example to retrieve", required = true)
    @PathVariable Integer id
) {
    // implementation
}
```

### Adding Schema Descriptions

Use `@Schema` annotation on DTOs and models:

```java
@Data
@Schema(description = "DTO for creating a new Example")
public class CreateExampleDto {
    
    @Schema(
        description = "Name of the example",
        example = "First Example",
        required = true
    )
    @NotBlank
    private String name;
}
```

### Customize Swagger UI Appearance

Add to `application.properties`:

```properties
# Expand all operations by default
springdoc.swagger-ui.doc-expansion=list

# Set default model rendering (example or model)
springdoc.swagger-ui.default-model-rendering=example

# Enable deep linking
springdoc.swagger-ui.deep-linking=true

# Display request duration
springdoc.swagger-ui.display-request-duration=true

# Disable "Try it out" button
springdoc.swagger-ui.try-it-out-enabled=false

# Set maximum number of displayed tags
springdoc.swagger-ui.max-displayed-tags=10
```

### Group APIs

Create multiple API groups:

```java
@Bean
public GroupedOpenApi publicApi() {
    return GroupedOpenApi.builder()
        .group("public")
        .pathsToMatch("/api/**")
        .build();
}

@Bean
public GroupedOpenApi adminApi() {
    return GroupedOpenApi.builder()
        .group("admin")
        .pathsToMatch("/admin/**")
        .build();
}
```

### Add Authentication to Swagger

If your API uses JWT authentication:

```java
@Bean
public OpenAPI technightOpenAPI() {
    return new OpenAPI()
        .info(new Info()
            .title("TechnightApi")
            .version("v1"))
        .addSecurityItem(new SecurityRequirement().addList("bearerAuth"))
        .components(new Components()
            .addSecuritySchemes("bearerAuth",
                new SecurityScheme()
                    .name("bearerAuth")
                    .type(SecurityScheme.Type.HTTP)
                    .scheme("bearer")
                    .bearerFormat("JWT")
                    .description("Enter JWT token")));
}
```

Then add to controllers:

```java
@SecurityRequirement(name = "bearerAuth")
@RestController
public class SecureController {
    // endpoints
}
```

## 📦 OpenAPI Specification

The raw OpenAPI/Swagger specification is available at:

```
http://localhost:8080/api/openapi
```

You can also get it in YAML format:

```
http://localhost:8080/api/openapi.yaml
```

You can use this specification to:
- Generate client SDKs in various languages
- Import into tools like Postman, Insomnia, or Bruno
- Share with frontend developers
- Generate documentation with tools like Redoc or Slate
- Validate API contracts

### Generate Client SDK

Using OpenAPI Generator:

```bash
# Install OpenAPI Generator
npm install -g @openapitools/openapi-generator-cli

# Generate TypeScript client
openapi-generator-cli generate \
  -i http://localhost:8080/api/openapi \
  -g typescript-fetch \
  -o ./generated-client
```

## 🔧 Production Considerations

### Security in Production

For production environments, consider:

1. **Disable Swagger UI entirely:**

```properties
# application-prod.properties
springdoc.swagger-ui.enabled=false
springdoc.api-docs.enabled=false
```

2. **Protect with authentication:**

```java
@Configuration
public class SecurityConfig {
    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        http
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/swagger/**", "/api/openapi/**")
                    .hasRole("ADMIN")
                .anyRequest().authenticated()
            );
        return http.build();
    }
}
```

3. **Host on separate documentation server:**
   - Deploy only the OpenAPI spec to a documentation server
   - Keep Swagger UI disabled in production API

### Performance Optimization

Swagger UI can impact startup time. To improve performance:

```properties
# Disable automatic schema generation for specific packages
springdoc.packages-to-exclude=com.technight.api.internal

# Limit paths to scan
springdoc.paths-to-match=/api/**
```

## 📚 Additional Resources

- [SpringDoc OpenAPI Documentation](https://springdoc.org/)
- [OpenAPI Specification](https://swagger.io/specification/)
- [Swagger UI Configuration](https://swagger.io/docs/open-source-tools/swagger-ui/usage/configuration/)
- [OpenAPI Generator](https://openapi-generator.tech/)

## 🐛 Troubleshooting

### Swagger UI shows empty page

1. Verify the application is running on the correct port
2. Check that `springdoc` dependencies are in `pom.xml`
3. Clear browser cache
4. Check console for JavaScript errors

### Endpoints not showing up

1. Ensure controllers have `@RestController` or `@Controller` annotations
2. Verify endpoints have HTTP method annotations (`@GetMapping`, etc.)
3. Check `springdoc.paths-to-match` configuration
4. Verify controller package is within component scan

### Models not showing correctly

1. Ensure classes are public
2. Use proper validation annotations (`@NotNull`, `@Size`, etc.)
3. Add `@Schema` annotations for better descriptions
4. Check that DTOs are used in controller method signatures

### "No operations defined in spec!"

1. Check that controllers are properly annotated
2. Verify Spring Boot is scanning controller packages
3. Ensure endpoints have return types defined
4. Check application logs for scanning errors

### OpenAPI JSON returns 404

1. Verify `springdoc.api-docs.path` configuration
2. Check that `springdoc.api-docs.enabled=true`
3. Ensure application started successfully
4. Try the default path: `/v3/api-docs`

## 💡 Tips

1. **Use Example Values**: Add `@Schema(example = "...")` to make "Try it out" easier
2. **Group Related Endpoints**: Use `@Tag` to organize endpoints
3. **Document Error Responses**: Include all possible response codes with `@ApiResponse`
4. **Validation Annotations**: Use Jakarta Validation annotations - they automatically show up in Swagger
5. **Custom Examples**: Use `@io.swagger.v3.oas.annotations.media.ExampleObject` for complex examples

