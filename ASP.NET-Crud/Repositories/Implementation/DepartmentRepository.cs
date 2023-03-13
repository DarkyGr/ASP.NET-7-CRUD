using ASP.NET_Crud.Models;
using ASP.NET_Crud.Repositories.Contract;
using System.Data;
using System.Data.SqlClient;

namespace ASP.NET_Crud.Repositories.Implementation
{
    public class DepartmentRepository : IGenericRepository<Department>
    {
        private readonly string _SQLString = "";

        public DepartmentRepository(IConfiguration configuration) {
            _SQLString = configuration.GetConnectionString("SQLString");    // Conexion with database
        }

        // Method Get List Departments
        public async Task<List<Department>> List()
        {
            List<Department> _list = new List<Department>();

            using (var conexion = new SqlConnection(_SQLString)) {      // Create conexion
                conexion.Open();    // Open conexion
                SqlCommand cmd = new SqlCommand("sp_listDepartments", conexion);    // Specify command (Storage procedure)
                cmd.CommandType = CommandType.StoredProcedure;      // Specify command type

                // Read stored data
                using (var dr = await cmd.ExecuteReaderAsync()) {
                    while (await dr.ReadAsync()){
                        _list.Add(new Department
                        {
                            id = Convert.ToInt32(dr["d_id"]),
                            name = dr["d_name"].ToString()
                        });
                    }
                }
            }

            return _list;
        }

        public Task<bool> Delete(Department model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(Department model)
        {
            throw new NotImplementedException();
        }        

        public Task<bool> Save(Department model)
        {
            throw new NotImplementedException();
        }
    }
}
