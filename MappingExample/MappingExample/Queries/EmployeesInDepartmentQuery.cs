using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MappingExample.Queries
{
    public class EmployeesInDepartmentQuery
    {
        public string DepartmentName { get; set; }

        public Expression<Func<Employee, bool>> AsExpression()
        {
            return (e => e.DepartmentName  == DepartmentName);
        }

    }
}
