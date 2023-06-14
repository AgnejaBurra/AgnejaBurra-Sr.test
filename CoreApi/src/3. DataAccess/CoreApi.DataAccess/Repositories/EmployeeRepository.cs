using CoreApi.DataAccess.Domains;
using CoreApi.DataAccess.IRepositories;
using CoreApi.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.DataAccess.Repositories
{
    public class EmployeeRepository:  IEmployeeRepository
    {

        /// <summary>
        /// Get employee details by id
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>Employee details</returns>
        //declare connection string  
        string cs = "Data Source=LENOVO-PC;Initial Catalog=AgnejaTest;Integrated Security=True;MultipleActiveResultSets=True";


        //Return list of all Employees  
        public async Task<List<Employee>> EmployeeList()
        {
            List<Employee> lst = new List<Employee>();
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("GetAllEmployee", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = await com.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        lst.Add(new Employee
                        {
                            EmployeeID = Convert.ToInt32(rdr["EmployeeID"]),
                            EmployeeName = rdr["EmployeeName"].ToString(),
                            EmployeeAddress1 = rdr["EmployeeAddress1"].ToString(),
                            PayPerHour = Convert.ToDecimal(rdr["PayPerHour"]),
                            
                           
                            ManagerName = rdr["ManagerName"].ToString(),
                            ManagerAddress1 = rdr["ManagerAddress1"].ToString(),
                            ManagerAnnualSalary = rdr["ManagerAnnualSalary"].ToString(),
                            MaxExpenseAmount = Convert.ToDecimal(rdr["MaxExpenseAmount"]),

                            SupervisorName = rdr["SupervisorName"].ToString(),
                            SupervisorAddress1 = rdr["SupervisorAddress1"].ToString(),
                            SupervisorAnnualSalary = Convert.ToDecimal(rdr["SupervisorAnnualSalary"]),

                        });
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="Employee">Employee object</param>
        /// <returns>void</returns>
        //Method for Adding an Employee  
        public async Task<int> Add(Employee emp)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("AddEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
                com.Parameters.AddWithValue("@FirstName", emp.FirstName);
                com.Parameters.AddWithValue("@LastName", emp.LastName);
                com.Parameters.AddWithValue("@Address1", emp.Address1);
                com.Parameters.AddWithValue("@PayPerHour", emp.PayPerHour);
                com.Parameters.AddWithValue("@ManagerID", emp.ManagerID);
                com.Parameters.AddWithValue("@SupervisorID", emp.SupervisorID); 
                com.Parameters.AddWithValue("@AnnualSalary", emp.AnnualSalary);
                com.Parameters.AddWithValue("@MaxExpenseAmount", emp.MaxExpenseAmount);
                com.Parameters.AddWithValue("@IsManager", emp.IsManager);
                com.Parameters.AddWithValue("@IsSupervisor", emp.IsSupervisor);

                com.Parameters.AddWithValue("@Action", "Insert");
                i =await com.ExecuteNonQueryAsync();
            }
            return i;
        }

        public async Task<List<ManagerSelectListModel>> GetManagerSelectList()
        {
            List<ManagerSelectListModel> lst = new List<ManagerSelectListModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("dbo.GetManagerList", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = await com.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        lst.Add(new ManagerSelectListModel
                        {
                            ManagerID = Convert.ToInt32(rdr["ManagerID"]),
                            ManagerName = rdr["ManagerName"].ToString(),

                        });
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;

        }
    }
}
