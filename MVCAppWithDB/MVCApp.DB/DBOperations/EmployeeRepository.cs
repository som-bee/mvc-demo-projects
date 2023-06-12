using MVCApp.DB;
using MVCApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCApp.DB.DBOperations
{
    public class EmployeeRepository
    {
        public int AddEmployee(EmployeeModel model)
        {
 

            using(var context = new EmployeeDBEntities())
            {
                Employee employee = new Employee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                
                };
                if(model.Address != null)
                {
                    employee.Address = new Address()
                    {
                        Details = model.Address.Details,
                        State = model.Address.State,
                        Country = model.Address.Country
                    };
                }
                context.Employee.Add(employee);
                context.SaveChanges();
                return employee.Id;
            }
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            using (var context = new EmployeeDBEntities())
            {
                var result = context.Employee.Select(
                    employee => new EmployeeModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        AddressId = employee.AddressId,
                        //using navigational properties
                        Address = new AddressModel()
                        {
                            Id = employee.Address.Id,
                            Details = employee.Address.Details,
                            State = employee.Address.State,
                            Country = employee.Address.Country
                        }
                    }

                    ).ToList();
                return result;              
            }
           
        }

        public EmployeeModel GetEmployee(int id)
        {
            using ( var context = new EmployeeDBEntities())
            {
                var employeeDetails = context.Employee.Where(
                    x => x.Id == id)
                    .Select(
                         employee => new EmployeeModel()
                         {
                             Id = employee.Id,
                             FirstName = employee.FirstName,
                             LastName = employee.LastName,
                             Email = employee.Email,
                             AddressId = employee.AddressId,
                             //using navigational properties
                             Address = new AddressModel()
                             {
                                 Id = employee.Address.Id,
                                 Details = employee.Address.Details,
                                 State = employee.Address.State,
                                 Country = employee.Address.Country
                             }
                         })
                    .FirstOrDefault();
                return employeeDetails;
            };
        }
        public bool DeleteEmployee(int id)
        {
            using (var context = new EmployeeDBEntities())
            {
                var employee = context.Employee.FirstOrDefault( x => x.Id == id);
                if (employee != null)
                {
                    context.Employee.Remove(employee);
                    context.SaveChanges();
                    return true;
                }
                 
                return false;
            };
        }

        public bool UpdateEmployee(int id, EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities())
            {
                var employee = context.Employee.FirstOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;

                    context.SaveChanges();
                    return true;
                }

                return false;
            };
        }
    }
}
