package com.technight.api.config;

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Contact;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.info.License;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

/**
 * OpenAPI/Swagger configuration
 */
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

