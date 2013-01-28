using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MappingExample.Queries
{
    class DepartmentsWithNameQuery
    {
        public string DepartmentName { get; set; }

        public List<Expression<Func<Department, object>>> IncludePaths { get; set; }

        public Expression<Func<Department, bool>> AsExpression()
        {
            return (d => d.DepartmentName == DepartmentName);
        }

        public IQueryable<Department> GetQuery(IQueryable<Department> query)
        {
            foreach (var p in IncludePaths)
            {
                query = query.Include(p);
            }
            return query;
        }
    }
}
