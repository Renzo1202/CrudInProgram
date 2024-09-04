using MemoryInCRUD.Models;
using System.Collections.Generic;

namespace MemoryInCRUD.Interfaces
{
    public interface IDataService
    {
        List<Employee> employees { get; set; }
        void GetDataEmployee();
        ResponseMessage<string> InsertEmployee(Employee employee);
        ResponseMessage<string> UpdateEmployee(Employee employee);
        ResponseMessage<bool> CheckIfExistsEmployee(string employeeId);
        ResponseMessage<bool> DateValidate(string BirthDate);
    }
}
