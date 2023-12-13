
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace WebAPIDemo1.DataAccessLayer
{
    public class DAL
    {
       // private static IConfiguration _configuration;
        //public DAL(IConfiguration configuration)
        //{
        //    _configuration = configuration;

        //}
       // SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Constr").ToString());
        SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-1LE553D;Database=TEST;Integrated Security=SSPI;");
        String connectionString = "Server=DESKTOP-1LE553D;Database=TEST;Integrated Security=SSPI;";
        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            string query = "SELECT * FROM Students";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Student");
                DataTable studentTable = dataSet.Tables["Student"];
                if (studentTable != null)
                {
                    foreach (DataRow row in studentTable.Rows)
                    {
                        students.Add(new Student()
                        {
                            Name = row["Name"].ToString(),
                            Age = Convert.ToInt32(row["Age"]),
                            Major = row["Major"].ToString(),
                            GPA = Convert.ToDouble(row["GPA"]) // Assuming GPA is a double or float
                        });
                    }
                }
            }

            return students;
        }
        public void InsertStudent(Student student)
        {
            string query = "INSERT INTO Students (Name, Age, Major, GPA) VALUES (@Name, @Age, @Major, @GPA)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Major", student.Major);
                cmd.Parameters.AddWithValue("@GPA", student.GPA);                

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
