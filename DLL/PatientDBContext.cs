using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DLL
{
  public class PatientDBContext : DbContext
  {
    public PatientDBContext() : base("DefaultConnection")
    {
    }

    public DbSet<PatientDTO> Patients { get; set; }
  }
}
