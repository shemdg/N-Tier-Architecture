using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // added for [Required]
using System.ComponentModel.DataAnnotations.Schema; // added for [Column]

namespace DTO
{
  public class PatientDTO
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Auto-incrementing ID

    [Required]
    [Column(TypeName = "decimal(7,4)")]
    public decimal Dosage { get; set; }

    [Required]
    [StringLength(50)]
    public string Drug { get; set; }

    [Required]
    [StringLength(50)]
    public string Patient { get; set; }
    // although the instruction was assigned to name it Patient, i still decided to use PatientName since my table name is Patient in order to avoid confusion

    [Required]
    public System.DateTime ModifiedDate { get; set; }
  }
}
