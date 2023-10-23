using HRMSystemDataAccess;
using HRMSystemModel;

namespace HRMSystemBusinessLogic
{
    public class EmployeeService
    {

        public static List<Employee> GetEmployee(string searchTerm)
        {
            List<Employee> employeeList = new List<Employee>();

            if (int.TryParse(searchTerm, out _))
            {
                Employee? employee = EmployeeRepository.GetById(Convert.ToInt32(searchTerm));
                if (employee != null)
                {
                    employeeList.Add(employee);
                }
            }
            else
            {
                List<Employee> fetchedEmployeeList = EmployeeRepository.GetByFullName(searchTerm);
                if (fetchedEmployeeList.Count > 0)
                {
                    employeeList.AddRange(fetchedEmployeeList);
                }
                else
                {
                    Employee? employee = EmployeeRepository.GetByBankAccountNumber(searchTerm);
                    if (employee != null)
                    {
                        employeeList.Add(employee);
                    }
                }
            }
            return employeeList;
        }
    }
}