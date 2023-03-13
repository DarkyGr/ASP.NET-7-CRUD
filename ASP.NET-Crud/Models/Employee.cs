namespace ASP.NET_Crud.Models
{
    public class Employee
    {
        public int id { get; set; }

        public string name { get; set; }

        public Department departmentRef { get; set; }

        public int salary { get; set; }

        public string contractDate { get; set; }
    }
}
