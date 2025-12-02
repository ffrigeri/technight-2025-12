package com.technight.api.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

/**
 * DTO for creating a new Example
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "DTO for creating a new Example")
public class CreateExampleDto {
    
    /**
     * Name of the example
     */
    @NotBlank(message = "Name is required")
    @Size(max = 200, message = "Name must not exceed 200 characters")
    @Schema(description = "Name of the example", example = "First Example", required = true)
    private String name;
    
    /**
     * Title of the example
     */
    @NotBlank(message = "Title is required")
    @Size(max = 200, message = "Title must not exceed 200 characters")
    @Schema(description = "Title of the example", example = "Introduction", required = true)
    private String title;
    
    /**
     * Optional description
     */
    @Size(max = 1000, message = "Description must not exceed 1000 characters")
    @Schema(description = "Optional description", example = "This is an example entry")
    private String description;
    
    /**
     * Indicates if the example is active
     */
    @Schema(description = "Indicates if the example is active", example = "true", defaultValue = "true")
    private Boolean isActive = true;
}

