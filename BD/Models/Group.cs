using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD.Models;

[Table("Group")]
public class Group
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(10)]
    public string Name { get; set; }
}