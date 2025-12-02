package com.technight.api.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

/**
 * DTO for updating an existing Example
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "DTO for updating an existing Example")
public class UpdateExampleDto {
    
    /**
     * Name of the example
     */
    @Size(max = 200, message = "Name must not exceed 200 characters")
    @Schema(description = "Name of the example", example = "Updated Example")
    private String name;
    
    /**
     * Title of the example
     */
    @Size(max = 200, message = "Title must not exceed 200 characters")
    @Schema(description = "Title of the example", example = "Updated Title")
    private String title;
    
    /**
     * Optional description
     */
    @Size(max = 1000, message = "Description must not exceed 1000 characters")
    @Schema(description = "Optional description", example = "This is an updated description")
    private String description;
    
    /**
     * Indicates if the example is active
     */
    @Schema(description = "Indicates if the example is active", example = "false")
    private Boolean isActive;
}

