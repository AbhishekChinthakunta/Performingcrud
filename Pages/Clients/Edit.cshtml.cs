
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Performingcrud.Pages.Clients
{
    public class EditModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        public StudentsInfo StudentsInfo = new StudentsInfo();
        public void OnGet()
        {
            string stid = Request.Query["stid"];

            try
            {
                string connetionString = "Data Source=C11-T1CTZPD2JVB\\SQLEXPRESS;Initial Catalog=abhi;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connetionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM student1 WHERE stid=@stid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@stid", stid);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                StudentsInfo.stid = "" + reader.GetInt32(0);
                                StudentsInfo.sname = reader.GetString(1);
                                StudentsInfo.sage = "" + reader.GetInt32(2);
                                StudentsInfo.sdegree = reader.GetString(3);
                                StudentsInfo.sphno = "" + reader.GetInt32(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            StudentsInfo.stid = Request.Form["stid"];
            StudentsInfo.sname = Request.Form["sname"];
            StudentsInfo.sage = Request.Form["sage"];
            StudentsInfo.sdegree = Request.Form["sdegree"];
            StudentsInfo.sphno = Request.Form["sphno"];


            if (StudentsInfo.stid.Length == 0 || StudentsInfo.sname.Length == 0 || StudentsInfo.sage.Length == 0 || StudentsInfo.sdegree.Length == 0 || StudentsInfo.sphno.Length == 0)
            {
                errorMessage = "All The fields are reuqired to be Filled for moving forward";
                return;
            }

            try
            {
                string connectionString = "Data Source=C11-T1CTZPD2JVB\\SQLEXPRESS;Initial Catalog=abhi;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE student1 " +
                                 "SET stid=@stid, sname=@sname, sage=@sage, sdegree=@sdegree, sphno=@sphno " +
                                 "WHERE stid = @stid";


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
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            Response.Redirect("/Clients/Index");

        }
    }
}
