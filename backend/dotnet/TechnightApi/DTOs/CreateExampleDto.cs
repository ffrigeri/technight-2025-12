using System.ComponentModel.DataAnnotations;

namespace TechnightApi.DTOs;

/// <summary>
/// DTO for creating a new Example
/// </summary>
public class CreateExampleDto
{
    /// <summary>
    /// Name of the example
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Title of the example
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Optional description
    /// </summary>
    [StringLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Indicates if the example is active
    /// </summary>
    public bool IsActive { get; set; } = true;
}

