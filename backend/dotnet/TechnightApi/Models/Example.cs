namespace TechnightApi.Models;

/// <summary>
/// Example entity representing a sample record
/// </summary>
public class Example
{
    /// <summary>
    /// Unique identifier for the example
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the example
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Title of the example
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Date when the example was entered
    /// </summary>
    public DateTime EntryDate { get; set; }

    /// <summary>
    /// Optional description field
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates if the example is active
    /// </summary>
    public bool IsActive { get; set; } = true;
}

