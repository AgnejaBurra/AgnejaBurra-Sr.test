using CoreApi.DataAccess.Domains;
using CoreApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.DataAccess.IRepositories
{
    public interface IEmployeeRepository
    {
         Task<List<Employee>> EmployeeList();
        Task<int> Add(Employee emp);
        Task<List<ManagerSelectListModel>> GetManagerSelectList();

    }
}
