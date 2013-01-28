using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;

namespace MappingExample.Queries
{
    public class CustomEmployeeQuery
    {
        public string PostCode { get; set; }
        public string DepartmentName { get; set; }

        public Expression<Func<Employee, bool>> AsExpression()
        {
            var postCodePredicate = new EmployeesWithPostCodeQuery
            {
               PostCode = "G1 1XX"
            };
            var departmentPredicate = new EmployeesInDepartmentQuery
            {
                DepartmentName = "Marketing"
            };
            var predicate = PredicateBuilder
                .False<Employee>()
                .Or(postCodePredicate.AsExpression())
                .And(departmentPredicate.AsExpression());
            return predicate;
        }
    }
}
