using ASP.NET_Crud.Models;
using ASP.NET_Crud.Repositories.Contract;
using System.Data;
using System.Data.SqlClient;

namespace ASP.NET_Crud.Repositories.Implementation
{
    public class EmployeeRepository : IGenericRepository<Employee>
    {
        private readonly string _SQLString = "";

        public EmployeeRepository(IConfiguration configuration) {
            _SQLString = configuration.GetConnectionString("SQLString");    // Conexion with database
        }

        // Method Get List Employees
        public async Task<List<Employee>> List()
        {
            List<Employee> _list = new List<Employee>();

            using (var conexion = new SqlConnection(_SQLString))
            {      // Create conexion
                conexion.Open();    // Open conexion
                SqlCommand cmd = new SqlCommand("sp_listEmployees", conexion);    // Specify command (Storage procedure)
                cmd.CommandType = CommandType.StoredProcedure;      // Specify command type

                // Read stored data
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _list.Add(new Employee
                        {
                            id = Convert.ToInt32(dr["e_id"]),
                            name = dr["e_name"].ToString(),
                            departmentRef = new Department() {
                                id = Convert.ToInt32(dr["d_id"]),
                                name = dr["d_name"].ToString()
                            },
                            salary = Convert.ToInt32(dr["salary"]),
                            contractDate = dr["contract_date"].ToString()
                        });
                    }
                }
            }

            return _list;
        }

        // Method to save employee
        public async Task<bool> Save(Employee model)
        {
            using (var conexion = new SqlConnection(_SQLString)) {
                conexion.Open();    // Open conexion
                SqlCommand cmd = new SqlCommand("sp_saveEmployee", conexion);    // Specify command (Storage procedure)
                
                // Add parameters whit value
                cmd.Parameters.AddWithValue("e_name", model.name);
                cmd.Parameters.AddWithValue("d_id", model.departmentRef);
                cmd.Parameters.AddWithValue("salry", model.salary);
                cmd.Parameters.AddWithValue("contract_date", model.contractDate);

                cmd.CommandType = CommandType.StoredProcedure;      // Specify command type

                int affected_rows = await cmd.ExecuteNonQueryAsync();

                if (affected_rows > 0){return true;}
                else { return false; }
            }
        }

        public async Task<bool> Edit(Employee model)
        {
            using (var conexion = new SqlConnection(_SQLString))
            {
                conexion.Open();    // Open conexion
                SqlCommand cmd = new SqlCommand("sp_editEmployee", conexion);    // Specify command (Storage procedure)

                // Add parameters whit value
                cmd.Parameters.AddWithValue("e_id", model.id);
                cmd.Parameters.AddWithValue("e_name", model.name);
                cmd.Parameters.AddWithValue("d_id", model.departmentRef);
                cmd.Parameters.AddWithValue("salry", model.salary);
                cmd.Parameters.AddWithValue("contract_date", model.contractDate);

                cmd.CommandType = CommandType.StoredProcedure;      // Specify command type

                int affected_rows = await cmd.ExecuteNonQueryAsync();

                if (affected_rows > 0) { return true; }
                else { return false; }
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var conexion = new SqlConnection(_SQLString))
            {
                conexion.Open();    // Open conexion
                SqlCommand cmd = new SqlCommand("sp_deleteEmployee", conexion);    // Specify command (Storage procedure)

                // Add parameters whit value
                cmd.Parameters.AddWithValue("e_id", id);

                cmd.CommandType = CommandType.StoredProcedure;      // Specify command type

                int affected_rows = await cmd.ExecuteNonQueryAsync();

                if (affected_rows > 0) { return true; }
                else { return false; }
            }
        }

                  
    }
}
