using MemoryInCRUD.Interfaces;
using MemoryInCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryInCRUD.Services
{
    public class DataService : IDataService
    {
        public List<Employee> employees { get; set; }

        public ResponseMessage<bool> DateValidate(string BirthDate)
        {
            DateTime temp;
            if (DateTime.TryParse(BirthDate, out temp))
            {
                return new ResponseMessage<bool>
                {
                    Data = true,
                    MESSAGE = "",
                    STATUS = true
                };
            }
            else
            {
                return new ResponseMessage<bool>
                {
                    Data = false,
                    MESSAGE = "Please input a valid date yyyy-MM-dd",
                    STATUS = false
                };
            };
        }

        public void GetDataEmployee()
        {
            employees = new List<Employee>();

            var employee1 = new Employee
            {
                EmployeeId = "1001",
                FullName = "Ronaldo",
                BirthDate = "1985-04-01"
            };

            employees.Add(employee1);

            var employee2 = new Employee
            {
                EmployeeId = "1002",
                FullName = "Messi",
                BirthDate = "1987-05-01"
            };

            employees.Add(employee2);

            var employee3 = new Employee
            {
                EmployeeId = "1003",
                FullName = "Kaka",
                BirthDate = "1982-04-12"
            };

            employees.Add(employee3);
        }

        public ResponseMessage<string> InsertEmployee(Employee employee)
        {
            try
            {
                if(employee.EmployeeId == "" || employee.FullName == "" || employee.BirthDate == "")
                {
                    return new ResponseMessage<string>
                    {
                        Data = "",
                        MESSAGE = "One of data cannot be empty or null, Please input a valid data",
                        STATUS = false
                    };
                }

                

                bool checkEmployeeId = employees.Any(item => item.EmployeeId == employee.EmployeeId);
                if (checkEmployeeId)
                {
                    return new ResponseMessage<string>
                    {
                        Data = "",
                        MESSAGE = "Employee Id " + employee.EmployeeId + " is already exists",
                        STATUS = false
                    };
                }

                Employee emp = new Employee
                {
                    EmployeeId = employee.EmployeeId,
                    FullName = employee.FullName,
                    BirthDate = employee.BirthDate
                };

                employees.Add(emp);

                return new ResponseMessage<string>
                {
                    Data = "",
                    MESSAGE = "",
                    STATUS = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<string>
                {
                    Data = "",
                    MESSAGE = ex.Message,
                    STATUS = false
                };
            }
        }

        public ResponseMessage<bool> CheckIfExistsEmployee(string employeeId)
        {
            try
            {
                bool checkEmployeeId = employees.Any(item => item.EmployeeId == employeeId);
                if (!checkEmployeeId)
                {
                    return new ResponseMessage<bool>
                    {
                        Data = false,
                        MESSAGE = "Employee Id is not exists",
                        STATUS = false
                    };
                }
                else
                {
                    return new ResponseMessage<bool>
                    {
                        Data = true,
                        MESSAGE = "Employee Id " + employeeId + " is already exists",
                        STATUS = true
                    };
                }
            }
            catch(Exception ex)
            {
                return new ResponseMessage<bool>
                {
                    Data = false,
                    MESSAGE = ex.Message.ToString(),
                    STATUS = false
                };
            }
        }

        public ResponseMessage<string> UpdateEmployee(Employee employee)
        {
            try
            {
                if (employee.EmployeeId == "" || employee.FullName == "" || employee.BirthDate == "")
                {
                    return new ResponseMessage<string>
                    {
                        Data = "",
                        MESSAGE = "One of data cannot be empty or null, Please input a valid data",
                        STATUS = false
                    };
                }

                bool checkEmployeeId = employees.Any(item => item.EmployeeId == employee.EmployeeId);
                if (!checkEmployeeId)
                {
                    return new ResponseMessage<string>
                    {
                        Data = "",
                        MESSAGE = "Employee Id is not exists",
                        STATUS = false
                    };
                }

                employees.Find(u => u.EmployeeId == employee.EmployeeId).FullName = employee.FullName;
                employees.Find(u => u.EmployeeId == employee.EmployeeId).BirthDate = employee.BirthDate;

                return new ResponseMessage<string>
                {
                    Data = "",
                    MESSAGE = "",
                    STATUS = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<string>
                {
                    Data = "",
                    MESSAGE = ex.Message,
                    STATUS = false
                };
            }
        }
    }
}
