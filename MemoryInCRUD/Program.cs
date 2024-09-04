using MemoryInCRUD.Interfaces;
using MemoryInCRUD.Models;
using MemoryInCRUD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryInCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService _dataService = new DataService();

            _dataService.GetDataEmployee();
            List<Employee> employees = _dataService.employees;

            createTable(employees);

            bool Continue = true;

            while (Continue)
            {
                Continue = false;
                Console.Write("Input C for Create or E for Edit Data? ");
                string CreateOrEdit = Console.ReadLine(); 

                if(CreateOrEdit == "C")
                {
                    Console.WriteLine("Create new Data");
                    Console.Write("Enter Employee Id : ");
                    string EmployeeId = Console.ReadLine();

                    checkEmployeeId:
                    var check = _dataService.CheckIfExistsEmployee(EmployeeId);

                    if(check.STATUS == true)
                    {
                        Console.Clear();
                        createTable(employees);
                        Console.WriteLine("Create new Data");
                        Console.WriteLine(check.MESSAGE);
                        Console.Write("Enter Employee Id : ");
                        EmployeeId = Console.ReadLine();
                        goto checkEmployeeId;
                    }

                    Console.Write("Enter Full Name : ");
                    string FullName = Console.ReadLine();

                    Console.Write("Enter Birth Date : ");
                    string BirthDate = Console.ReadLine();

                    ValidateDate:
                    var datevalidate = _dataService.DateValidate(BirthDate);

                    if(datevalidate.STATUS == false)
                    {
                        Console.WriteLine(datevalidate.MESSAGE);
                        Console.Write("Enter Birth Date : ");
                        BirthDate = Console.ReadLine();
                        goto ValidateDate;
                    }

                    var employee = new Employee
                    {
                        EmployeeId = EmployeeId,
                        FullName = FullName,
                        BirthDate = BirthDate
                    };

                    var result = _dataService.InsertEmployee(employee);
                    if (result.STATUS == false)
                    {
                        Console.Clear();
                        createTable(_dataService.employees);
                        Console.WriteLine(result.MESSAGE);
                    }
                    else
                    {
                        Console.Clear();
                        createTable(_dataService.employees);
                        Console.Write("Do you want to continue input Data? (Y/N)");
                        string answer = Console.ReadLine();
                        if (answer == "Y")
                        {
                            Continue = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Edit Data");
                    Console.Write("Enter Employee Id : ");
                    string EmployeeId = Console.ReadLine();

                    checkEmployeeId:
                    var check = _dataService.CheckIfExistsEmployee(EmployeeId);

                    if (check.STATUS == false)
                    {
                        Console.Clear();
                        createTable(employees);
                        Console.WriteLine("Edit Data");
                        Console.WriteLine(check.MESSAGE);
                        Console.Write("Enter Employee Id : ");
                        EmployeeId = Console.ReadLine();
                        goto checkEmployeeId;
                    }

                    Console.Write("Enter Full Name : ");
                    string FullName = Console.ReadLine();

                    Console.Write("Enter Birth Date : ");
                    string BirthDate = Console.ReadLine();

                    ValidateDate:
                    var datevalidate = _dataService.DateValidate(BirthDate);

                    if (datevalidate.STATUS == false)
                    {
                        Console.WriteLine(datevalidate.MESSAGE);
                        Console.Write("Enter Birth Date : ");
                        BirthDate = Console.ReadLine();
                        goto ValidateDate;
                    }

                    var employee = new Employee
                    {
                        EmployeeId = EmployeeId,
                        FullName = FullName,
                        BirthDate = BirthDate
                    };

                    var result = _dataService.UpdateEmployee(employee);
                    if (result.STATUS == false)
                    {
                        Console.Clear();
                        createTable(_dataService.employees);
                        Console.WriteLine(result.MESSAGE);
                    }
                    else
                    {
                        Console.Clear();
                        createTable(_dataService.employees);
                        Console.Write("Do you want to continue input Data? (Y/N)");
                        string answer = Console.ReadLine();
                        if (answer == "Y")
                        {
                            Continue = true;
                        }
                    }
                }
            }
        }

        static void createTable(List<Employee> employees)
        {
            int tableWidth = 75;
            string[] header = new string[] { "Employee Id", "Full Name", "Birth Date" };


            PrintLine(tableWidth);
            PrintRow(header, tableWidth);
            PrintLine(tableWidth);
            foreach(var content in employees)
            {
                string[] emp = new string[] { content.EmployeeId, content.FullName, content.BirthDate };
                PrintRow(emp, tableWidth);
            }
            PrintLine(tableWidth);

        }

        static void PrintLine(int tableWidth)
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(string[] columns, int tableWidth)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
