using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MappingExample
{
    public class Department
    {
        [Key]                  
        public virtual string DepartmentName { get; set; }

        public virtual List<Employee> Employees { get; set; }
       
    }
}
