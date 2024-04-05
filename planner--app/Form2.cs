using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

namespace planner__app
{
    public partial class Log_in_form : Form
    {
        public static int Id
        {
            get;
            private set;
        }
        public Log_in_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            int userIdFromDatabase = GetUserIdFromDatabase(username, password);

            if (userIdFromDatabase != -1)
            {
                Log_in_form.Id = userIdFromDatabase;
                MessageBox.Show("Log in successful!");
                HomePage form1 = new HomePage(Log_in_form.Id.ToString());
                form1.Show();
            }
            else
            {
                label4.Text = "Password incorrect!";
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next();

            string ConnectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string username = textBox1.Text;
                string password = textBox2.Text;

                string query = "INSERT INTO Users(username,password,user_id) VALUES(@Username, @Password,@Id)";

                // Use parameterized query to prevent SQL injection
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Id", randomNumber);


                    // Password strength checking
                    if (username.Contains(" ") || username.Contains("@"))
                    {
                        label2.Text = "You can't use this username! No spaces or @ sign allowed!";
                    }
                    else if (password.Contains(" ") || password.Length < 8 || password.Length > 16 ||
                             !password.Any(char.IsUpper) || !password.Any(char.IsLower) ||
                             !password.Any(char.IsDigit) || !Regex.IsMatch(password, @"[!@#$%^&*()_+[\]{};:'\|,.<>?]"))
                    {
                        label4.Text = "You can't use this password! No spaces allowed!\n" +
                                      "The password must be between 8 and 16 characters long!\n" +
                                      "The password must contain at least one capital and one\nlower letter, one number and one special sign.";
                    }
                    else
                    {
                        // Check if username already exists in the database
                        string checkQuery = "SELECT COUNT(*) FROM Users WHERE username = @Username";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                        {
                            checkCmd.Parameters.AddWithValue("@Username", username);
                            int count = (int)checkCmd.ExecuteScalar();
                            if (count > 0)
                            {
                                label2.Text = "Account with this username already\nexists!";
                            }
                            else
                            {
                                // Add user to database
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Data has been saved!");

                                HomePage form1 = new HomePage();
                                form1.Show();

                                int userIdFromDatabase = GetUserIdFromDatabase(username, password);
                                Log_in_form.Id = userIdFromDatabase;
                            }
                        }
                    }
                }
            }
        }
        public static int GetUserIdFromDatabase(string username, string password)
        {
            int userId = -1; // Default value if user ID is not found

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT user_id FROM Users WHERE username = @Username AND password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log or display error)
                    Console.WriteLine("Error retrieving user ID: " + ex.Message);
                }
            }

            return userId;
        }
    }
}
