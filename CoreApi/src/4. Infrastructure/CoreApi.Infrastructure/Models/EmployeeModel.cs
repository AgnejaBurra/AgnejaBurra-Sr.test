using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.Infrastructure.Models
{
    public class EmployeeModel
    {
        public int? EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public decimal? PayPerHour { get; set; }

        public int? ManagerID { get; set; }
        public int? SupervisorID { get; set; }
        public decimal? AnnualSalary { get; set; }
        public decimal? MaxExpenseAmount { get; set; }
        public bool IsManager { get; set; }
        public bool IsSupervisor { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress1 { get; set; }
        public string ManagerName { get; set; }
        public string ManagerAddress1 { get; set; }
        public string ManagerAnnualSalary { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorAddress1 { get; set; }
        public decimal? SupervisorAnnualSalary { get; set; }


    }
}
