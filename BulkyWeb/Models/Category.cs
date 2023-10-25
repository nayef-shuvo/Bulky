using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace BulkyWeb.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [DisplayName("Category Name")]
    public string Name { get; set; }
    [Required]
    [DisplayName("Display Order")]
    [Range(1, 100, ErrorMessage="Display Order must be between 1 and 100")]
    public int DisplayOrder { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}