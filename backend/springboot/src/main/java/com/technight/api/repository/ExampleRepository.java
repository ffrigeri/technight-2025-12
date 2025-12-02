package com.technight.api.repository;

import com.technight.api.model.Example;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Repository interface for Example entity
 */
@Repository
public interface ExampleRepository extends JpaRepository<Example, Integer> {
    
    /**
     * Find examples by name containing the given string (case-insensitive)
     * @param name the name to search for
     * @return list of matching examples
     */
    @Query("SELECT e FROM Example e WHERE LOWER(e.name) LIKE LOWER(CONCAT('%', :name, '%')) ORDER BY e.entryDate DESC")
    List<Example> findByNameContainingIgnoreCase(@Param("name") String name);
    
    /**
     * Find all examples ordered by entry date descending
     * @return list of all examples ordered by entry date
     */
    @Query("SELECT e FROM Example e ORDER BY e.entryDate DESC")
    List<Example> findAllOrderByEntryDateDesc();
}

