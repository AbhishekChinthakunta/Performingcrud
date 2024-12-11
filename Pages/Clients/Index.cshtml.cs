
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Performingcrud.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<StudentsInfo> listStudents = new List<StudentsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=C11-T1CTZPD2JVB\\SQLEXPRESS;Initial Catalog=abhi;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM student1";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsInfo studentsInfo = new StudentsInfo();
                                studentsInfo.stid = "" + reader.GetInt32(0);
                                studentsInfo.sname = reader.GetString(1);
                                studentsInfo.sage =  "" + reader.GetInt32(2);
                                studentsInfo.sdegree = reader.GetString(3);
                                studentsInfo.sphno = "" + reader.GetInt32(4);
                                
                           




                                listStudents.Add(studentsInfo);

                                 
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
     
    public class StudentsInfo()
    {
        public String? stid;
        public String? sname;
        public String? sage;
        public String? sdegree;
        public String? sphno;
        
        
    }
}
   