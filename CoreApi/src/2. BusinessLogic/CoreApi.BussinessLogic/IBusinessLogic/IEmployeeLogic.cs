using CoreApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.BussinessLogic.IBusinessLogic
{
    public interface IEmployeeLogic
    {
        #region Methods
        Task<List<EmployeeModel>> GetEmployeesAsync();
        Task<ReturnResponseModel> AddEmployee(EmployeeModel employeeViewModel);
        Task<List<ManagerSelectListModel>> GetManagerSelectList();
        #endregion
    }
}
