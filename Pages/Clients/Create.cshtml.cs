using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.Data.SqlClient;

namespace Performingcrud.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public StudentsInfo StudentsInfo = new StudentsInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            StudentsInfo.stid = Request.Form["stid"];
            StudentsInfo.sname = Request.Form["sname"];
            StudentsInfo.sage = Request.Form["sage"];
            StudentsInfo.sdegree = Request.Form["sdegree"];
            StudentsInfo.sphno = Request.Form["sphno"];


            if(StudentsInfo.stid.Length==0 || StudentsInfo.sname.Length==0 || StudentsInfo.sage .Length == 0 || StudentsInfo.sdegree.Length == 0 || StudentsInfo.sphno.Length == 0)
            {
                errorMessage = "All The fields are reuqired to be Filled for moving forward";
                return;
            }

            try
            {
                String connectionString = "Data Source=C11-T1CTZPD2JVB\\SQLEXPRESS;Initial Catalog=abhi;Integrated Security=True;Trust Server Certificate=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO student1 (stid, sname, sage, sdegree, sphno) " +
             "VALUES (@stid, @sname, @sage, @sdegree, @sphno);";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@stid", StudentsInfo.stid);
                        command.Parameters.AddWithValue("@sname", StudentsInfo.sname);
                        command.Parameters.AddWithValue("@sage", StudentsInfo.sage);
                        command.Parameters.AddWithValue("@sdegree", StudentsInfo.sdegree);
                        command.Parameters.AddWithValue("@sphno", StudentsInfo.sphno);


                        command.ExecuteNonQuery();
                    }

                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }


            StudentsInfo.stid = "";
            StudentsInfo.sname ="";
            StudentsInfo.sage = "";
            StudentsInfo.sdegree = "";
            StudentsInfo.sphno = "";
            successMessage = "The Students Details are added Successfully";

        }
    }

}
