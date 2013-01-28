using System;
using System.Linq;
using System.Linq.Expressions;

namespace MappingExample.Queries
{
    public class EmployeesWithPostCodeQuery
    {
        public string PostCode { get; set; }

        public Expression<Func<Employee, bool>> AsExpression()
        {
            return (e => e.Address.PostCode == PostCode);
        }
    }
}
