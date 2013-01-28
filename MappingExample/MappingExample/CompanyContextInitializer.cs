using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace MappingExample
{
    public class CompanyContextInitializer : DropCreateDatabaseAlways<CompanyContext>
    {
        protected override void Seed(CompanyContext context)
        {
            base.Seed(context);

            Address ad1 = new Address
            {
                AddressId = 1,
                PropertyName = "Code-First Quay",
                PropertyNumber = 10,
                PostCode = "G2 1YY"
            };

            Address ad2 = new Address
            {
                AddressId = 2,
                PropertyName = "ORM Mansions",
                PropertyNumber = 11,
                PostCode = "G3 0TT"
            };

            // Create Address objects
            Address ad3 = new Address
            {
                AddressId = 3,
                PropertyName = "LINQ Tower",
                PropertyNumber = 99,
                PostCode = "G1 1XX"
            };

            Address ad4 = new Address
            {
                AddressId = 4,
                PropertyName = "EF House",
                PropertyNumber = 101,
                PostCode = "G4 0XX"
            };

            HourlyPaidEmployee emp1 = new HourlyPaidEmployee
            {
                Name = "Jenson",
                Username = "jenson",
                PhoneNumber = "9876",
                Address = ad1
            };

            SalariedEmployee emp2 = new SalariedEmployee
            {
                Name = "Checo",
                Username = "checo",
                PhoneNumber = "5432",
                Address = ad2,
                PayGrade = 1
            };

            HourlyPaidEmployee emp3 = new HourlyPaidEmployee
            {
                Name = "Fernando",
                Username = "fernando",
                PhoneNumber = "1234",
                Address = ad3,
            };

            SalariedEmployee emp4 = new SalariedEmployee
            {
                Name = "Felipe",
                Username = "felipe",
                PhoneNumber = "5678",
                Address = ad3,
                PayGrade = 5
            };

            SalariedEmployee emp5 = new SalariedEmployee
            {
                Name = "Seb",
                Username = "seb",
                PhoneNumber = "1468",
                Address = ad4,
                PayGrade = 9
            };

            Department dep1 = new Department
            {
                DepartmentName = "Marketing",
                Employees = new List<Employee>{
                    emp1,
                    emp3
                }
            };

            Department dep2 = new Department
            {
                DepartmentName = "Sales",
                Employees = new List<Employee>{
                    emp2,
                    emp4,
                    emp5
                }
            };

            context.Departments.Add(dep1);
            context.Departments.Add(dep2);

            context.SaveChanges();

        }
    }
}
