package com.technight.api.model;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

/**
 * Example entity representing a sample record
 */
@Entity
@Table(name = "example", indexes = {
    @Index(name = "ix_example_entry_date", columnList = "entry_date")
})
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Example {
    
    /**
     * Unique identifier for the example
     */
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Integer id;
    
    /**
     * Name of the example
     */
    @Column(name = "name", nullable = false, length = 200)
    private String name;
    
    /**
     * Title of the example
     */
    @Column(name = "title", nullable = false, length = 200)
    private String title;
    
    /**
     * Date when the example was entered
     */
    @Column(name = "entry_date", nullable = false)
    private LocalDateTime entryDate;
    
    /**
     * Optional description field
     */
    @Column(name = "description", length = 1000)
    private String description;
    
    /**
     * Indicates if the example is active
     */
    @Column(name = "is_active", nullable = false)
    private Boolean isActive = true;
}

