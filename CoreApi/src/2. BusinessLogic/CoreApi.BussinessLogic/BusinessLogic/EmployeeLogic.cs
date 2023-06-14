using AutoMapper;
using CoreApi.BussinessLogic.IBusinessLogic;
using CoreApi.DataAccess.IRepositories;
using CoreApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.BussinessLogic.BusinessLogic
{
    public class EmployeeLogic: IEmployeeLogic
    {
        #region Variable
        private readonly IEmployeeRepository _iEmployeeRepository;
        #endregion

        #region Constructor
        public EmployeeLogic(IEmployeeRepository iEmployeeRepository)
        {
            _iEmployeeRepository = iEmployeeRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get all Employee
        /// </summary>
        /// <returns>list of Employee</returns>
        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<DataAccess.Domains.Employee> employeeList = await _iEmployeeRepository.EmployeeList();
            List<EmployeeModel> employeeViewModelList = new List<EmployeeModel>();
            employeeViewModelList = Mapper.Map<List<EmployeeModel>>(employeeList);
            return employeeViewModelList;
        }

        
        /// <summary>
        /// Add Employee 
        /// </summary>
        /// <param name="EmployeeViewModel"></param>
        /// <returns></returns>
        public async Task<ReturnResponseModel> AddEmployee(EmployeeModel employeeViewModel)
        {
            var result = new ReturnResponseModel();
            var domain = Mapper.Map<DataAccess.Domains.Employee>(employeeViewModel);
            await _iEmployeeRepository.Add(domain);
            result.Status = true;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ManagerSelectListModel>> GetManagerSelectList()
        {
            List<ManagerSelectListModel> managerList = await _iEmployeeRepository.GetManagerSelectList();
            return managerList;
        }
        #endregion
    }
}
