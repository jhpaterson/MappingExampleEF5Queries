using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;      // need to add assembly reference
using MappingExample.Queries;
using LinqKit;


namespace MappingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // set database initializer
            Database.SetInitializer<CompanyContext>(new CompanyContextInitializer());

            // initialise EF Profiler
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            CompanyContext db = new CompanyContext();

            #region QUERIES AND EXPRESSIONS
            var query = from d in db.Departments
                        where d.DepartmentName == "Marketing" && d.Employees.Count() > 0
                        select d;

            var result = query.ToList();

            var singleQuery = db.Departments
                .Where(d => d.DepartmentName == "Marketing")
                .Single();

            var extensionsQuery = db.Departments
                .Where(d => d.DepartmentName == "Marketing" && d.Employees.Count() > 0)
                .ToList();

            var projectQuery = db.Departments
                .Where(d => d.DepartmentName == "Marketing" && d.Employees.Count() > 0)
                .SelectMany(d => d.Employees, (d,e) => new{ Name = e.Name })
                .ToList();

            Expression<Func<Department, bool>> filter1 =
                d => d.DepartmentName == "Marketing";

            Expression<Func<Department, bool>> filter2 =
                d => d.Employees.Count() > 0;

            var filterQuery = db.Departments
                .Where(filter1)
                .Where(filter2)
                .Select(d => d.Employees)
                .ToList();

            #endregion

           
            #region QUERY OBJECT
            //var employeesWithPostCode = new EmployeesWithPostCodeQuery
            //{
            //    PostCode = "G1 1XX",
            //};

            //var query = db.Employees
            //    .Where(employeesWithPostCode.AsExpression());

            //var result = query.ToList();
            #endregion


            #region COMPOSING QUERIES
            //var employeesInDepartment = new EmployeesInDepartmentQuery
            //{
            //    DepartmentName = "Marketing",
            //};

            //var employeesWithPostCode = new EmployeesWithPostCodeQuery
            //{
            //    PostCode = "G1 1XX"
            //};

            //var exactlyTheEmployeesWeWant = PredicateBuilder
            //    .False<Employee>()
            //    .Or(employeesInDepartment.AsExpression())
            //    .And(employeesWithPostCode.AsExpression());

            //var query = db.Employees.AsExpandable()
            //    .Where(exactlyTheEmployeesWeWant);

            //var result = query.ToList();
            #endregion


            #region COMPOSED QUERY OBJECT
            //var exactlyTheEmployeesWeWant = new CustomEmployeeQuery
            //{
            //    PostCode = "G1 1XX",
            //    DepartmentName = "Marketing"
            //}.AsExpression();

            //var query = db.Employees.AsExpandable()
            //    .Where(exactlyTheEmployeesWeWant);

            //var result = query.ToList();
            #endregion


            #region LINQ TO ENTITIES QUERY FETCH STRATEGY
            //var query = db.Departments
            //    .Include(d => d.Employees.Select(e => e.Address));

            ////var query = (from d in db.Departments
            ////             select d)
            ////            .Include(d => d.Employees)

            //var result = query.ToList();
            #endregion


            #region TRANSACTIONS

            //CompanyContext db2 = new CompanyContext();    // second context for multicontext transactions

            //Address goodAddress = new Address
            //{
            //    AddressId = 5,
            //    PropertyName = "Persistence Centre",
            //    PropertyNumber = 101,
            //    PostCode = "G4 2QQ"
            //};

            //Address badAddress = new Address
            //{
            //    AddressId = 6,
            //    PropertyName = "DbContext Campus"
            //};

            //var scope = new
            //    TransactionScope(TransactionScopeOption.RequiresNew,
            //    new TransactionOptions()
            //    {
            //        IsolationLevel = IsolationLevel.ReadUncommitted
            //    });
            //try
            //{
            //    using (scope)
            //    {
            //        db.Addresses.Add(goodAddress);
            //        db.SaveChanges();
            //        db2.Addresses.Add(badAddress);
            //        db2.SaveChanges();
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //var add = db.Addresses.ToList();
            #endregion


            #region FIND
            ////find by PK
            //Employee emp = db.Employees.Find(3);

            //// find by composite PK
            //Address addr = db.Addresses.Find(1, "G2 1YY");

            //// find again - returns same instance without hitting database
            //Employee empAgain = db.Employees.Find(3);

            //// add object to context and find, even though it is not in database yet
            //db.Departments.Add(new Department { DepartmentName = "Purchasing" });      
            //Department dep2 = db.Departments.Find("Purchasing");

            //// alternative way of accessing a DbSet
            //var add = db.Set<Address>().ToList();     
            #endregion


            #region RAW SQL
            //var entityResult = db.Departments.SqlQuery(
            //    @"SELECT * FROM Departments WHERE DepartmentName='Marketing'")
            //    .ToList();

            //var stringResult = db.Database.SqlQuery<string>(
            //    @"SELECT DepartmentName FROM Departments")
            //    .ToList();

            //db.Database.ExecuteSqlCommand(
            //    @"UPDATE Locations SET BuildingName = 'SQL Ranch' WHERE AddressId = 1");

            //var storedProcResult = db.Departments.SqlQuery(
            //    "dbo.GetDeptByName @p0", "Marketing");
            #endregion


            #region UNDERLYING OBJECTCONTEXT
            //var ctx = ((IObjectContextAdapter)db).ObjectContext;
            //ctx.DeleteDatabase();
            #endregion


            #region END
            Console.ReadLine();
            #endregion

        }
    }
}
