using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BD.Models;

[Table("GroupAssignment")]
[PrimaryKey(nameof(GroupId), nameof(StudentId))]
public class GroupAssignment
{
    [Column("Group_Id")]
    public int GroupId { get; set; }
    [Column("Student_Id")]
    public int StudentId { get; set; }
}