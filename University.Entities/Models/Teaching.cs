using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Entities.Models
{
    [PrimaryKey(nameof(StaffId), nameof(CourseId))]
    public class Teaching
    {
        public int StaffId { get; set; }
        public string? CourseId { get; set; }
        public virtual Staff Staff { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;

    }
}
