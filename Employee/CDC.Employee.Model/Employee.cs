using System;

namespace CDC.Employee.Model
{
    public class Employee
    {
        public Guid GlobalEmployeeId { get; set; }
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? HiredDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
