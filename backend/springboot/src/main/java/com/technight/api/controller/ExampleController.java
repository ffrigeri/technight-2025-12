package com.technight.api.controller;

import com.technight.api.dto.CreateExampleDto;
import com.technight.api.dto.UpdateExampleDto;
import com.technight.api.model.Example;
import com.technight.api.repository.ExampleRepository;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * REST controller for Example endpoints
 */
@RestController
@RequestMapping("/api/examples")
@Tag(name = "Examples", description = "Example management endpoints")
@CrossOrigin(origins = {"http://localhost:5173", "http://localhost:3000"})
public class ExampleController {

    private final ExampleRepository exampleRepository;

    @Autowired
    public ExampleController(ExampleRepository exampleRepository) {
        this.exampleRepository = exampleRepository;
    }

    /**
     * Get all examples
     */
    @GetMapping
    @Operation(summary = "Get all examples", description = "Retrieves all example records from the database")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully retrieved list",
                     content = @Content(schema = @Schema(implementation = Example.class)))
    })
    public ResponseEntity<List<Example>> getAllExamples() {
        List<Example> examples = exampleRepository.findAllOrderByEntryDateDesc();
        return ResponseEntity.ok(examples);
    }

    /**
     * Get example by ID
     */
    @GetMapping("/{id}")
    @Operation(summary = "Get example by ID", description = "Retrieves a specific example by its ID")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully retrieved example",
                     content = @Content(schema = @Schema(implementation = Example.class))),
        @ApiResponse(responseCode = "404", description = "Example not found")
    })
    public ResponseEntity<?> getExampleById(
            @Parameter(description = "ID of the example to retrieve", required = true)
            @PathVariable Integer id) {
        return exampleRepository.findById(id)
                .<ResponseEntity<?>>map(ResponseEntity::ok)
                .orElseGet(() -> {
                    Map<String, String> error = new HashMap<>();
                    error.put("message", "Example with ID " + id + " not found");
                    return ResponseEntity.status(HttpStatus.NOT_FOUND).body(error);
                });
    }

    /**
     * Create a new example
     */
    @PostMapping
    @Operation(summary = "Create a new example", description = "Creates a new example record")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "201", description = "Example created successfully",
                     content = @Content(schema = @Schema(implementation = Example.class))),
        @ApiResponse(responseCode = "400", description = "Invalid input data")
    })
    public ResponseEntity<Example> createExample(
            @Parameter(description = "Example data to create", required = true)
            @Valid @RequestBody CreateExampleDto dto) {
        Example example = new Example();
        example.setName(dto.getName());
        example.setTitle(dto.getTitle());
        example.setDescription(dto.getDescription());
        example.setEntryDate(LocalDateTime.now());
        example.setIsActive(dto.getIsActive() != null ? dto.getIsActive() : true);

        Example savedExample = exampleRepository.save(example);
        return ResponseEntity.status(HttpStatus.CREATED).body(savedExample);
    }

    /**
     * Update an existing example
     */
    @PutMapping("/{id}")
    @Operation(summary = "Update an example", description = "Updates an existing example record")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Example updated successfully",
                     content = @Content(schema = @Schema(implementation = Example.class))),
        @ApiResponse(responseCode = "404", description = "Example not found"),
        @ApiResponse(responseCode = "400", description = "Invalid input data")
    })
    public ResponseEntity<?> updateExample(
            @Parameter(description = "ID of the example to update", required = true)
            @PathVariable Integer id,
            @Parameter(description = "Updated example data", required = true)
            @Valid @RequestBody UpdateExampleDto dto) {
        return exampleRepository.findById(id)
                .<ResponseEntity<?>>map(example -> {
                    // Update only the provided fields
                    if (dto.getName() != null) {
                        example.setName(dto.getName());
                    }
                    if (dto.getTitle() != null) {
                        example.setTitle(dto.getTitle());
                    }
                    if (dto.getDescription() != null) {
                        example.setDescription(dto.getDescription());
                    }
                    if (dto.getIsActive() != null) {
                        example.setIsActive(dto.getIsActive());
                    }

                    Example updatedExample = exampleRepository.save(example);
                    return ResponseEntity.ok(updatedExample);
                })
                .orElseGet(() -> {
                    Map<String, String> error = new HashMap<>();
                    error.put("message", "Example with ID " + id + " not found");
                    return ResponseEntity.status(HttpStatus.NOT_FOUND).body(error);
                });
    }

    /**
     * Delete an example
     */
    @DeleteMapping("/{id}")
    @Operation(summary = "Delete an example", description = "Deletes an example record")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "204", description = "Example deleted successfully"),
        @ApiResponse(responseCode = "404", description = "Example not found")
    })
    public ResponseEntity<?> deleteExample(
            @Parameter(description = "ID of the example to delete", required = true)
            @PathVariable Integer id) {
        return exampleRepository.findById(id)
                .<ResponseEntity<?>>map(example -> {
                    exampleRepository.delete(example);
                    return ResponseEntity.noContent().build();
                })
                .orElseGet(() -> {
                    Map<String, String> error = new HashMap<>();
                    error.put("message", "Example with ID " + id + " not found");
                    return ResponseEntity.status(HttpStatus.NOT_FOUND).body(error);
                });
    }

    /**
     * Search examples by name
     */
    @GetMapping("/search")
    @Operation(summary = "Search examples", description = "Search examples by name (case-insensitive)")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully retrieved search results",
                     content = @Content(schema = @Schema(implementation = Example.class)))
    })
    public ResponseEntity<List<Example>> searchExamples(
            @Parameter(description = "Name to search for (case-insensitive)")
            @RequestParam(required = false) String name) {
        List<Example> examples;
        
        if (name != null && !name.isBlank()) {
            examples = exampleRepository.findByNameContainingIgnoreCase(name);
        } else {
            examples = exampleRepository.findAllOrderByEntryDateDesc();
        }
        
        return ResponseEntity.ok(examples);
    }
}

