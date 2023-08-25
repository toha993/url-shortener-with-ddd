using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortenerWithDDD.Domain.Models;

public class Url
{
    public int Id { get; set; }
    [Required]
    public string? Code { get; set; }
    [Required]
    public string? Link { get; set; }
    public long VisitCount { get; set; } = 0L;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; }
}

