using System.ComponentModel.DataAnnotations;

namespace TechnightApi.DTOs;

/// <summary>
/// DTO for updating an existing Example
/// </summary>
public class UpdateExampleDto
{
    /// <summary>
    /// Name of the example
    /// </summary>
    [StringLength(200)]
    public string? Name { get; set; }

    /// <summary>
    /// Title of the example
    /// </summary>
    [StringLength(200)]
    public string? Title { get; set; }

    /// <summary>
    /// Optional description
    /// </summary>
    [StringLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Indicates if the example is active
    /// </summary>
    public bool? IsActive { get; set; }
}

