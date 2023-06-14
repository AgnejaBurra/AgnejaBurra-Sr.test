using CoreApi.BussinessLogic.IBusinessLogic;
using CoreApi.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        #region Variables
        private readonly IEmployeeLogic _iEmployeeLogic;
        #endregion

        #region Constructor
        /// <summary>
        /// Employee Controller Constructor
        /// </summary>
        /// <param name="iEmployeeLogic"></param>
        public EmployeeController(IEmployeeLogic iEmployeeLogic)
        {
            _iEmployeeLogic = iEmployeeLogic;
        }
      
        #endregion
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> employeeList = await _iEmployeeLogic.GetEmployeesAsync();
            return employeeList;
        }
        

        // POST api/<EmployeeController>
        [HttpPost("AddEmployee")]
        public async Task<ReturnResponseModel> AddEmployee(EmployeeModel employeeViewModel)
        {
            var result = new ReturnResponseModel();
            await _iEmployeeLogic.AddEmployee(employeeViewModel);
            result.Status = true;
            return result;
        }
        [HttpGet("GetManagerList")]
        public async Task<List<ManagerSelectListModel>> GetManagerList()
        {
            List<ManagerSelectListModel> managerList = await _iEmployeeLogic.GetManagerSelectList();
            return managerList;
        }


    }
}
