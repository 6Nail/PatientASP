using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientAndDoctors.Models
{
    public class VisitHistoryViewModel
    {
        public Guid PatientId { get; set; }
        public List<VisitHistory> VisitHistories { get; set; }
    }
}
