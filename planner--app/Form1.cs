using Microsoft.VisualBasic.ApplicationServices;
using System.Data.SqlClient;
using System.Drawing.Imaging;

namespace planner__app
{
    public partial class HomePage : Form
    {
        bool hasId;
        private const int Rows = 31;
        private const int Columns = 9;
        private const string settingsFile = "checkboxstate.txt";
        private List<CheckBox> checkboxes = new List<CheckBox>();

        public HomePage()
        {
            hasId = false;
            InitializeComponent();
            LoadPictures();
            this.FormClosing += HomePage_FormClosing;
        }
        public HomePage(string id)
            : this()
        {
            hasId = true;
            int userId = Log_in_form.Id;
            richTextBox1.Text = GetIdeas(userId);
            richTextBox2.Text = GetInspirations(userId);

            richTextBox3.Text = GetMission(userId);
            richTextBox4.Text = GetWelcomeThings(userId);
            richTextBox5.Text = GetLeftThings(userId);
            richTextBox6.Text = GetSpentMoreTimeOn(userId);
            richTextBox7.Text = GetHappyThings(userId);
            richTextBox8.Text = GetAffirmations(userId);

            richTextBox9.Text = GetPersonal(userId);
            richTextBox10.Text = GetFamilyHome(userId);
            richTextBox11.Text = GetHealth(userId);
            richTextBox12.Text = GetCareerFinances(userId);
            richTextBox13.Text = GetFunTravel(userId);
            richTextBox14.Text = GetOther(userId);

            textBox1.Text = GetGoal1(userId);
            richTextBox23.Text = GetSteps1(userId);
            textBox2.Text = GetGoal2(userId);
            richTextBox22.Text = GetSteps2(userId);

            textBox3.Text = GetHabbit1(userId);
            textBox4.Text = GetHabbit2(userId);
            textBox5.Text = GetHabbit3(userId);
            textBox6.Text = GetHabbit4(userId);
            textBox7.Text = GetHabbit5(userId);
            textBox8.Text = GetHabbit6(userId);
            textBox11.Text = GetHabbit7(userId);
            textBox10.Text = GetHabbit8(userId);
            textBox9.Text = GetHabbit9(userId);

            GetSettings();

            richTextBox15.Text = GetIncome(userId);
            richTextBox16.Text = GetExpense(userId);
            textBox12.Text = GetIncomes(userId);
            textBox13.Text = GetExpenses(userId);
            textBox14.Text = GetSaved(userId);

            textBox15.Text = GetRecipient1(userId);
            textBox16.Text = GetOccassion1(userId);
            textBox17.Text = GetPresent1(userId);
            textBox20.Text = GetRecipient2(userId);
            textBox19.Text = GetOccassion2(userId);
            textBox18.Text = GetPresent2(userId);
            textBox23.Text = GetRecipient3(userId);
            textBox22.Text = GetOccassion3(userId);
            textBox21.Text = GetPresent3(userId);
            textBox29.Text = GetRecipient4(userId);
            textBox28.Text = GetOccassion4(userId);
            textBox27.Text = GetPresent4(userId);
            textBox26.Text = GetRecipient5(userId);
            textBox25.Text = GetOccassion5(userId);
            textBox24.Text = GetPresent5(userId);

            richTextBox17.Text = GetBreakfast(userId);
            richTextBox18.Text = GetLunch(userId);
            richTextBox20.Text = GetDinner(userId);
            richTextBox19.Text = GetSnack(userId);

            richTextBox21.Text = GetBooks(userId);

            richTextBox24.Text = GetMoviesAndSeries(userId);

            richTextBox25.Text = GetPlacesToVisit(userId);

            richTextBox26.Text = GetThingsToTry(userId);

            richTextBox27.Text = GetImportantDates(userId);

            richTextBox32.Text = GetMy_focus(userId);
            richTextBox28.Text = GetPersonal_goals(userId);
            richTextBox29.Text = GetCareer_goals(userId);
            richTextBox30.Text = GetImportant(userId);
            richTextBox31.Text = GetNotes1(userId);

            richTextBox33.Text = GetMonthlyCalendar(userId);

            richTextBox34.Text = GetToDoList(userId);
            richTextBox35.Text = GetNotes2(userId);

            richTextBox36.Text = GetAchievedGoals1(userId);
            richTextBox37.Text = GetNotes3(userId);

            richTextBox39.Text = GetAchievedGoals2(userId);
            richTextBox38.Text = GetNotes4(userId);
        }
        private void LoadPictures()
        {
            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string selectQuery = "SELECT Picture1, Picture2, Picture3 FROM Ideas_Inspirations WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) // Assuming there's only one row for each user_id
                    {
                        byte[] image1Bytes = (byte[])reader["Picture1"];
                        byte[] image2Bytes = (byte[])reader["Picture2"];
                        byte[] image3Bytes = (byte[])reader["Picture3"];

                        // Convert byte arrays back to images
                        pictureBox1.Image = ByteArrayToImage(image1Bytes);
                        pictureBox2.Image = ByteArrayToImage(image2Bytes);
                        pictureBox3.Image = ByteArrayToImage(image3Bytes);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading pictures: " + ex.Message);
                }
            }
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        private string GetIdeas(int id)
        {
            string ideas = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT ideas FROM Ideas_Inspirations WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ideas = reader.GetString(0);
                        }
                    }
                }
            }

            return ideas;
        }

        private string GetInspirations(int id)
        {
            string inspirations = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT inspirations FROM Ideas_Inspirations WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            inspirations = reader.GetString(0);
                        }
                    }
                }
            }

            return inspirations;
        }
        private string GetMission(int id)
        {
            string mission = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT mission FROM My_focus WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mission = reader.GetString(0);
                        }
                    }
                }
            }

            return mission;
        }
        private string GetWelcomeThings(int id)
        {
            string welcome_things = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT welcome_things FROM My_focus WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            welcome_things = reader.GetString(0);
                        }
                    }
                }
            }

            return welcome_things;
        }
        private string GetLeftThings(int id)
        {
            string left_things = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT left_things FROM My_focus WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            left_things = reader.GetString(0);
                        }
                    }
                }
            }

            return left_things;
        }

        private string GetSpentMoreTimeOn(int id)
        {
            string spent_more_time_on = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT spent_more_time_on FROM My_focus WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            spent_more_time_on = reader.GetString(0);
                        }
                    }
                }
            }

            return spent_more_time_on;
        }

        private string GetHappyThings(int id)
        {
            string happy_things = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT happy_things FROM My_focus WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            happy_things = reader.GetString(0);
                        }
                    }
                }
            }

            return happy_things;
        }

        private string GetAffirmations(int id)
        {
            string affirmations = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT affirmations FROM My_focus WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            affirmations = reader.GetString(0);
                        }
                    }
                }
            }

            return affirmations;
        }

        private string GetPersonal(int id)
        {
            string personal = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT personal FROM My_goals WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            personal = reader.GetString(0);
                        }
                    }
                }
            }

            return personal;
        }
        private string GetFamilyHome(int id)
        {
            string family_home = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT family_home FROM My_goals WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            family_home = reader.GetString(0);
                        }
                    }
                }
            }

            return family_home;
        }
        private string GetHealth(int id)
        {
            string health = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT health FROM My_goals WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            health = reader.GetString(0);
                        }
                    }
                }
            }

            return health;
        }

        private string GetCareerFinances(int id)
        {
            string career_finances = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT career_finances FROM My_goals WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            career_finances = reader.GetString(0);
                        }
                    }
                }
            }

            return career_finances;
        }

        private string GetFunTravel(int id)
        {
            string fun_travel = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT fun_travel FROM My_goals WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fun_travel = reader.GetString(0);
                        }
                    }
                }
            }

            return fun_travel;
        }

        private string GetOther(int id)
        {
            string other = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT other FROM My_goals WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            other = reader.GetString(0);
                        }
                    }
                }
            }

            return other;
        }
        private string GetGoal1(int id)
        {
            string goal1 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT goal1 FROM My_plan_for_action WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            goal1 = reader.GetString(0);
                        }
                    }
                }
            }

            return goal1;
        }
        private string GetSteps1(int id)
        {
            string steps1 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT steps1 FROM My_plan_for_action WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            steps1 = reader.GetString(0);
                        }
                    }
                }
            }

            return steps1;
        }
        private string GetGoal2(int id)
        {
            string goal2 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT goal2 FROM My_plan_for_action WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            goal2 = reader.GetString(0);
                        }
                    }
                }
            }

            return goal2;
        }
        private string GetSteps2(int id)
        {
            string steps2 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT steps2 FROM My_plan_for_action WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            steps2 = reader.GetString(0);
                        }
                    }
                }
            }

            return steps2;
        }
        private string GetHabbit1(int id)
        {
            string habbit1 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit1 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit1 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit1;
        }
        private string GetHabbit2(int id)
        {
            string habbit2 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit2 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit2 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit2;
        }
        private string GetHabbit3(int id)
        {
            string habbit3 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit3 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit3 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit3;
        }
        private string GetHabbit4(int id)
        {
            string habbit4 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit4 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit4 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit4;
        }
        private string GetHabbit5(int id)
        {
            string habbit5 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit5 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit5 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit5;
        }
        private string GetHabbit6(int id)
        {
            string habbit6 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit6 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit6 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit6;
        }
        private string GetHabbit7(int id)
        {
            string habbit7 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit7 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit7 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit7;
        }
        private string GetHabbit8(int id)
        {
            string habbit8 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit8 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit8 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit8;
        }
        private string GetHabbit9(int id)
        {
            string habbit9 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT habbit9 FROM Habbit_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habbit9 = reader.GetString(0);
                        }
                    }
                }
            }

            return habbit9;
        }
        private string GetIncome(int id)
        {
            string income = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT income FROM Budget_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            income = reader.GetString(0);
                        }
                    }
                }
            }

            return income;
        }
        private string GetExpense(int id)
        {
            string expense = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT expense FROM Budget_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            expense = reader.GetString(0);
                        }
                    }
                }
            }

            return expense;
        }
        private string GetIncomes(int id)
        {
            string incomes = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT incomes FROM Budget_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            incomes = reader.GetString(0);
                        }
                    }
                }
            }

            return incomes;
        }
        private string GetExpenses(int id)
        {
            string expenses = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT expenses FROM Budget_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            expenses = reader.GetString(0);
                        }
                    }
                }
            }

            return expenses;
        }
        private string GetSaved(int id)
        {
            string saved = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT saved FROM Budget_tracker WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            saved = reader.GetString(0);
                        }
                    }
                }
            }

            return saved;
        }
        private string GetRecipient1(int id)
        {
            string recipient1 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT recipient1 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recipient1 = reader.GetString(0);
                        }
                    }
                }
            }

            return recipient1;
        }
        private string GetOccassion1(int id)
        {
            string occassion1 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT occassion1 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            occassion1 = reader.GetString(0);
                        }
                    }
                }
            }

            return occassion1;
        }
        private string GetPresent1(int id)
        {
            string present1 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT present1 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            present1 = reader.GetString(0);
                        }
                    }
                }
            }

            return present1;
        }
        private string GetRecipient2(int id)
        {
            string recipient2 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT recipient2 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recipient2 = reader.GetString(0);
                        }
                    }
                }
            }

            return recipient2;
        }
        private string GetOccassion2(int id)
        {
            string occassion2 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT occassion2 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            occassion2 = reader.GetString(0);
                        }
                    }
                }
            }

            return occassion2;
        }
        private string GetPresent2(int id)
        {
            string present2 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT present2 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            present2 = reader.GetString(0);
                        }
                    }
                }
            }

            return present2;
        }
        private string GetRecipient3(int id)
        {
            string recipient3 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT recipient3 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recipient3 = reader.GetString(0);
                        }
                    }
                }
            }

            return recipient3;
        }
        private string GetOccassion3(int id)
        {
            string occassion3 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT occassion3 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            occassion3 = reader.GetString(0);
                        }
                    }
                }
            }

            return occassion3;
        }
        private string GetPresent3(int id)
        {
            string present3 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT present3 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            present3 = reader.GetString(0);
                        }
                    }
                }
            }

            return present3;
        }
        private string GetRecipient4(int id)
        {
            string recipient4 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT recipient4 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recipient4 = reader.GetString(0);
                        }
                    }
                }
            }

            return recipient4;
        }
        private string GetOccassion4(int id)
        {
            string occassion4 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT occassion4 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            occassion4 = reader.GetString(0);
                        }
                    }
                }
            }

            return occassion4;
        }
        private string GetPresent4(int id)
        {
            string present4 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT present4 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            present4 = reader.GetString(0);
                        }
                    }
                }
            }

            return present4;
        }
        private string GetRecipient5(int id)
        {
            string recipient5 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT recipient5 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recipient5 = reader.GetString(0);
                        }
                    }
                }
            }

            return recipient5;
        }
        private string GetOccassion5(int id)
        {
            string occassion5 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT occassion5 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            occassion5 = reader.GetString(0);
                        }
                    }
                }
            }

            return occassion5;
        }
        private string GetPresent5(int id)
        {
            string present5 = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT present5 FROM Ideas_for_presents WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            present5 = reader.GetString(0);
                        }
                    }
                }
            }

            return present5;
        }
        private string GetBreakfast(int id)
        {
            string breakfast = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT breakfast FROM Menu_ideas WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            breakfast = reader.GetString(0);
                        }
                    }
                }
            }

            return breakfast;
        }
        private string GetLunch(int id)
        {
            string lunch = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT lunch FROM Menu_ideas WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lunch = reader.GetString(0);
                        }
                    }
                }
            }

            return lunch;
        }
        private string GetDinner(int id)
        {
            string dinner = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT dinner FROM Menu_ideas WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dinner = reader.GetString(0);
                        }
                    }
                }
            }

            return dinner;
        }
        private string GetSnack(int id)
        {
            string snack = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT snack FROM Menu_ideas WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            snack = reader.GetString(0);
                        }
                    }
                }
            }

            return snack;
        }
        private string GetBooks(int id)
        {
            string books = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT books FROM Books WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            books = reader.GetString(0);
                        }
                    }
                }
            }

            return books;
        }

        private string GetMoviesAndSeries(int id)
        {
            string movies_and_series = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT movies_and_series FROM Movies_and_series WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            movies_and_series = reader.GetString(0);
                        }
                    }
                }
            }

            return movies_and_series;
        }

        private string GetPlacesToVisit(int id)
        {
            string places_to_visit = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT places_to_visit FROM Places_to_visit WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            places_to_visit = reader.GetString(0);
                        }
                    }
                }
            }

            return places_to_visit;
        }
        private string GetThingsToTry(int id)
        {
            string things_to_try = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT things_to_try FROM Things_to_try WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            things_to_try = reader.GetString(0);
                        }
                    }
                }
            }

            return things_to_try;
        }
        private string GetImportantDates(int id)
        {
            string important_dates = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT important_dates FROM Important_dates WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            important_dates = reader.GetString(0);
                        }
                    }
                }
            }

            return important_dates;
        }
        private string GetMy_focus(int id)
        {
            string my_focus = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT my_focus FROM Monthly_plan WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            my_focus = reader.GetString(0);
                        }
                    }
                }
            }

            return my_focus;
        }
        private string GetPersonal_goals(int id)
        {
            string personal_goals = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT personal_goals FROM Monthly_plan WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            personal_goals = reader.GetString(0);
                        }
                    }
                }
            }

            return personal_goals;
        }
        private string GetCareer_goals(int id)
        {
            string career_goals = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT career_goals FROM Monthly_plan WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            career_goals = reader.GetString(0);
                        }
                    }
                }
            }

            return career_goals;
        }
        private string GetImportant(int id)
        {
            string important = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT important FROM Monthly_plan WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            important = reader.GetString(0);
                        }
                    }
                }
            }

            return important;
        }
        private string GetNotes1(int id)
        {
            string notes = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT notes FROM Monthly_plan WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            notes = reader.GetString(0);
                        }
                    }
                }
            }

            return notes;
        }
        private string GetMonthlyCalendar(int id)
        {
            string monthly_calendar = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT monthly_calendar FROM Monthly_calendar WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            monthly_calendar = reader.GetString(0);
                        }
                    }
                }
            }

            return monthly_calendar;
        }
        private string GetToDoList(int id)
        {
            string to_do_list = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT to_do_list FROM Daily_planner WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            to_do_list = reader.GetString(0);
                        }
                    }
                }
            }

            return to_do_list;
        }
        private string GetNotes2(int id)
        {
            string notes = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT notes FROM Daily_planner WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            notes = reader.GetString(0);
                        }
                    }
                }
            }

            return notes;
        }
        private string GetAchievedGoals1(int id)
        {
            string achieved_goals = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT achieved_goals FROM Monthly_review WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            achieved_goals = reader.GetString(0);
                        }
                    }
                }
            }

            return achieved_goals;
        }
        private string GetNotes3(int id)
        {
            string notes = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT notes FROM Monthly_review WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            notes = reader.GetString(0);
                        }
                    }
                }
            }

            return notes;
        }
        private string GetAchievedGoals2(int id)
        {
            string achieved_goals = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT achieved_goals FROM Yearly_review WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            achieved_goals = reader.GetString(0);
                        }
                    }
                }
            }

            return achieved_goals;
        }
        private string GetNotes4(int id)
        {
            string notes = string.Empty;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string query = "SELECT notes FROM Yearly_review WHERE user_id = @UserId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            notes = reader.GetString(0);
                        }
                    }
                }
            }

            return notes;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set initial directory and filter for image files
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Set the chosen image as the background image of Form1
                    string selectedImagePath = openFileDialog.FileName;
                    Image selectedImage = Image.FromFile(selectedImagePath);

                    pictureBox1.Image = selectedImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not load image from file. " + ex.Message);
                }
            }
        }

        private List<CheckBox> checkBoxList = new List<CheckBox>();

        private void HomePage_Load(object sender, EventArgs e)
        {
            // Add all checkboxes to the list
            // Loop to add all checkboxes to the list
            for (int i = 1; i <= 279; i++)
            {
                // Find the checkbox by name using Controls collection
                CheckBox checkBox = this.Controls.Find("checkBox" + i.ToString(), true).FirstOrDefault() as CheckBox;
                if (checkBox != null)
                {
                    checkBoxList.Add(checkBox);
                }
            }

            // Load settings for all checkboxes
            GetSettings();

            GetDateTimePickerSettings();

        }
        public void GetDateTimePickerSettings()
        {
            if (Properties.Settings.Default.DateTimePickerValue1 != null)
            {
                dateTimePicker1.Value = (DateTime)Properties.Settings.Default.DateTimePickerValue1;
            }

            // Load settings for DateTimePicker2 from application settings
            if (Properties.Settings.Default.DateTimePickerValue2 != null)
            {
                dateTimePicker2.Value = (DateTime)Properties.Settings.Default.DateTimePickerValue2;
            }
        }
        public void GetSettings()
        {
            // Load settings for each checkbox in the list
            for (int i = 0; i < checkBoxList.Count; i++)
            {
                // Use the Settings indexer to access the settings by name
                checkBoxList[i].Checked = Properties.Settings.Default["CheckBox" + (i + 1).ToString()] != null ?
                                            (bool)Properties.Settings.Default["CheckBox" + (i + 1).ToString()] :
                                            false;
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Habbit_tracker_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox38_TextChanged(object sender, EventArgs e)
        {
        }

        private void richTextBox39_TextChanged(object sender, EventArgs e)
        {
        }

        private void label61_Click(object sender, EventArgs e)
        {
        }

        private void Save_data_button_Click(object sender, EventArgs e)
        {
            string ideas = richTextBox1.Text;
            string inspirations = richTextBox2.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string insertQuery = "INSERT INTO Ideas_Inspirations (ideas, inspirations,user_id) VALUES (@Ideas, @Inspirations,@UserId)";

            string updateQuery = "UPDATE Ideas_Inspirations SET ideas = @Ideas, inspirations = @Inspirations WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Ideas", ideas);
                insertCommand.Parameters.AddWithValue("@Inspirations", inspirations);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Ideas", ideas);
                updateCommand.Parameters.AddWithValue("@Inspirations", inspirations);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        rowsAffected = updateCommand.ExecuteNonQuery();
                    }

                    if (rowsAffected > 0)
                        MessageBox.Show("Content saved successfully!");
                    else
                        MessageBox.Show("No changes were made.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }


        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg); // You can change the ImageFormat as per your requirement
                return ms.ToArray();
            }
        }
        private void Ideas_and_inpirations_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set initial directory and filter for image files
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Set the chosen image as the background image of Form1
                    string selectedImagePath = openFileDialog.FileName;
                    Image selectedImage = Image.FromFile(selectedImagePath);

                    pictureBox2.Image = selectedImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not load image from file. " + ex.Message);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Save_pictures_button_Click(object sender, EventArgs e)
        {
            // Get the images from the picture boxes
            byte[] image1 = ImageToByteArray(pictureBox1.Image);
            byte[] image2 = ImageToByteArray(pictureBox2.Image);
            byte[] image3 = ImageToByteArray(pictureBox3.Image);

            // Execute SQL query to insert/update the images into the database
            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";
            string selectQuery = "SELECT COUNT(*) FROM Ideas_Inspirations WHERE user_id = @UserId";
            string updateQuery = "UPDATE Ideas_Inspirations SET Picture1 = @Image1, Picture2 = @Image2, Picture3 = @Image3 WHERE user_id = @UserId";
            string insertQuery = "INSERT INTO Ideas_Inspirations (user_id, Picture1, Picture2, Picture3) VALUES (@UserId, @Image1, @Image2, @Image3)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                int existingRows = (int)selectCommand.ExecuteScalar();

                if (existingRows > 0)
                {
                    // If row exists, update the existing row
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);
                    updateCommand.Parameters.AddWithValue("@Image1", image1);
                    updateCommand.Parameters.AddWithValue("@Image2", image2);
                    updateCommand.Parameters.AddWithValue("@Image3", image3);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Images updated successfully!");
                    else
                        MessageBox.Show("No changes were made.");
                }
                else
                {
                    // If no row exists, insert a new row
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);
                    insertCommand.Parameters.AddWithValue("@Image1", image1);
                    insertCommand.Parameters.AddWithValue("@Image2", image2);
                    insertCommand.Parameters.AddWithValue("@Image3", image3);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Images saved successfully!");
                    else
                        MessageBox.Show("No changes were made.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mission = richTextBox3.Text;
            string welcome_things = richTextBox4.Text;
            string left_things = richTextBox5.Text;
            string spent_more_time_on = richTextBox6.Text;
            string happy_things = richTextBox7.Text;
            string affirmations = richTextBox8.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM My_focus WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO My_focus (mission, welcome_things, left_things, spent_more_time_on, happy_things, affirmations, user_id) VALUES (@Mission, @Welcome_things, @Left_things, @Spent_more_time_on, @Happy_things, @Affirmations, @UserId)";

            string updateQuery = "UPDATE My_focus SET mission = @Mission, welcome_things = @Welcome_things, left_things = @Left_things, spent_more_time_on = @Spent_more_time_on, happy_things = @Happy_things, affirmations = @Affirmations WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Mission", mission);
                insertCommand.Parameters.AddWithValue("@Welcome_things", welcome_things);
                insertCommand.Parameters.AddWithValue("@Left_things", left_things);
                insertCommand.Parameters.AddWithValue("@Spent_more_time_on", spent_more_time_on);
                insertCommand.Parameters.AddWithValue("@Happy_things", happy_things);
                insertCommand.Parameters.AddWithValue("@Affirmations", affirmations);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Mission", mission);
                updateCommand.Parameters.AddWithValue("@Welcome_things", welcome_things);
                updateCommand.Parameters.AddWithValue("@Left_things", left_things);
                updateCommand.Parameters.AddWithValue("@Spent_more_time_on", spent_more_time_on);
                updateCommand.Parameters.AddWithValue("@Happy_things", happy_things);
                updateCommand.Parameters.AddWithValue("@Affirmations", affirmations);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string personal = richTextBox9.Text;
            string family_home = richTextBox10.Text;
            string health = richTextBox11.Text;
            string career_finances = richTextBox12.Text;
            string fun_travel = richTextBox13.Text;
            string other = richTextBox14.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM My_goals WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO My_goals (personal, family_home, health, career_finances, fun_travel, other, user_id) VALUES (@Personal, @Family_Home, @Health, @Career_Finances, @Fun_Travel, @Other, @UserId)";

            string updateQuery = "UPDATE My_goals SET personal = @Personal, family_home = @Family_Home, health = @Health, career_finances = @Career_Finances, fun_travel = @Fun_Travel, other = @Other WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Personal", personal);
                insertCommand.Parameters.AddWithValue("@Family_Home", family_home);
                insertCommand.Parameters.AddWithValue("@Health", health);
                insertCommand.Parameters.AddWithValue("@Career_Finances", career_finances);
                insertCommand.Parameters.AddWithValue("@Fun_Travel", fun_travel);
                insertCommand.Parameters.AddWithValue("@Other", other);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Personal", personal);
                updateCommand.Parameters.AddWithValue("@Family_Home", family_home);
                updateCommand.Parameters.AddWithValue("@Health", health);
                updateCommand.Parameters.AddWithValue("@Career_Finances", career_finances);
                updateCommand.Parameters.AddWithValue("@Fun_Travel", fun_travel);
                updateCommand.Parameters.AddWithValue("@Other", other);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string goal1 = textBox1.Text;
            string steps1 = richTextBox23.Text;
            string goal2 = textBox2.Text;
            string steps2 = richTextBox22.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM My_plan_for_action WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO My_plan_for_action (goal1, steps1, goal2, steps2, user_id) VALUES (@Goal1, @Steps1, @Goal2, @Steps2, @UserId)";

            string updateQuery = "UPDATE My_plan_for_action SET goal1 = @Goal1, steps1 = @Steps1, goal2 = @Goal2, steps2 = @Steps2 WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Goal1", goal1);
                insertCommand.Parameters.AddWithValue("@Steps1", steps1);
                insertCommand.Parameters.AddWithValue("@Goal2", goal2);
                insertCommand.Parameters.AddWithValue("@Steps2", steps2);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Goal1", goal1);
                updateCommand.Parameters.AddWithValue("@Steps1", steps1);
                updateCommand.Parameters.AddWithValue("@Goal2", goal2);
                updateCommand.Parameters.AddWithValue("@Steps2", steps2);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveSettings();
            string habbit1 = textBox3.Text;
            string habbit2 = textBox4.Text;
            string habbit3 = textBox5.Text;
            string habbit4 = textBox6.Text;
            string habbit5 = textBox7.Text;
            string habbit6 = textBox8.Text;
            string habbit7 = textBox11.Text;
            string habbit8 = textBox10.Text;
            string habbit9 = textBox9.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Habbit_tracker WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Habbit_tracker (habbit1, habbit2, habbit3, habbit4, habbit5, habbit6, habbit7, habbit8, habbit9, user_id) VALUES (@Habbit1, @Habbit2, @Habbit3, @Habbit4, @Habbit5, @Habbit6, @Habbit7, @Habbit8, @Habbit9, @UserId)";

            string updateQuery = "UPDATE Habbit_tracker SET habbit1 = @Habbit1, habbit2 = @Habbit2, habbit3 = @Habbit3, habbit4 = @Habbit4, habbit5 = @Habbit5, habbit6 = @Habbit6, habbit7 = @Habbit7, habbit8 = @Habbit8, habbit9 = @Habbit9 WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Habbit1", habbit1);
                insertCommand.Parameters.AddWithValue("@Habbit2", habbit2);
                insertCommand.Parameters.AddWithValue("@Habbit3", habbit3);
                insertCommand.Parameters.AddWithValue("@Habbit4", habbit4);
                insertCommand.Parameters.AddWithValue("@Habbit5", habbit5);
                insertCommand.Parameters.AddWithValue("@Habbit6", habbit6);
                insertCommand.Parameters.AddWithValue("@Habbit7", habbit7);
                insertCommand.Parameters.AddWithValue("@Habbit8", habbit8);
                insertCommand.Parameters.AddWithValue("@Habbit9", habbit9);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Habbit1", habbit1);
                updateCommand.Parameters.AddWithValue("@Habbit2", habbit2);
                updateCommand.Parameters.AddWithValue("@Habbit3", habbit3);
                updateCommand.Parameters.AddWithValue("@Habbit4", habbit4);
                updateCommand.Parameters.AddWithValue("@Habbit5", habbit5);
                updateCommand.Parameters.AddWithValue("@Habbit6", habbit6);
                updateCommand.Parameters.AddWithValue("@Habbit7", habbit7);
                updateCommand.Parameters.AddWithValue("@Habbit8", habbit8);
                updateCommand.Parameters.AddWithValue("@Habbit9", habbit9);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }
        private void HomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save settings for all checkboxes
            SaveSettings();
            // Save settings for the DateTimePicker controls
            SaveDateTimePickerSettings();
        }
        public void SaveDateTimePickerSettings()
        {
            // Save selected value of DateTimePicker1 to application settings
            Properties.Settings.Default.DateTimePickerValue1 = dateTimePicker1.Value;

            // Save selected value of DateTimePicker2 to application settings
            Properties.Settings.Default.DateTimePickerValue2 = dateTimePicker2.Value;

            Properties.Settings.Default.Save();
        }
        public void SaveSettings()
        {
            // Save settings for each checkbox in the list
            for (int i = 0; i < checkBoxList.Count; i++)
            {
                // Use the Settings indexer to access the settings by name
                Properties.Settings.Default["CheckBox" + (i + 1).ToString()] = checkBoxList[i].Checked;
            }
            Properties.Settings.Default.Save();
        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                // Find the index of the checkbox in the list
                int index = checkBoxList.IndexOf(checkBox);
                if (index != -1)
                {
                    // Save the state of the checkbox
                    Properties.Settings.Default["CheckBox" + (index + 1)] = checkBox.Checked;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string income = richTextBox15.Text;
            string expense = richTextBox16.Text;
            string incomes = textBox12.Text;
            string expenses = textBox13.Text;
            string saved = textBox14.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Budget_tracker WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Budget_tracker (income, expense, incomes, expenses, saved, user_id) VALUES (@Income, @Expense, @Incomes, @Expenses, @Saved, @UserId)";

            string updateQuery = "UPDATE Budget_tracker SET income = @Income, expense = @Expense, incomes = @Incomes, expenses = @Expenses, saved = @Saved WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Income", income);
                insertCommand.Parameters.AddWithValue("@Expense", expense);
                insertCommand.Parameters.AddWithValue("@Incomes", incomes);
                insertCommand.Parameters.AddWithValue("@Expenses", expenses);
                insertCommand.Parameters.AddWithValue("@Saved", saved);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Income", income);
                updateCommand.Parameters.AddWithValue("@Expense", expense);
                updateCommand.Parameters.AddWithValue("@Incomes", incomes);
                updateCommand.Parameters.AddWithValue("@Expenses", expenses);
                updateCommand.Parameters.AddWithValue("@Saved", saved);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string recipient1 = textBox15.Text;
            string occassion1 = textBox16.Text;
            string present1 = textBox17.Text;
            string recipient2 = textBox20.Text;
            string occassion2 = textBox19.Text;
            string present2 = textBox18.Text;
            string recipient3 = textBox23.Text;
            string occassion3 = textBox22.Text;
            string present3 = textBox21.Text;
            string recipient4 = textBox29.Text;
            string occassion4 = textBox28.Text;
            string present4 = textBox27.Text;
            string recipient5 = textBox26.Text;
            string occassion5 = textBox25.Text;
            string present5 = textBox24.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Ideas_for_presents WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Ideas_for_presents (recipient1, occassion1, present1, recipient2, occassion2, present2, recipient3, occassion3, present3, recipient4, occassion4, present4, recipient5, occassion5, present5, user_id) VALUES (@Recipient1, @Occassion1, @Present1, @Recipient2, @Occassion2, @Present2, @Recipient3, @Occassion3, @Present3, @Recipient4, @Occassion4, @Present4, @Recipient5, @Occassion5, @Present5, @UserId)";

            string updateQuery = "UPDATE Ideas_for_presents SET recipient1 = @Recipient1, occassion1 = @Occassion1, present1 = @Present1, recipient2 = @Recipient2, occassion2 = @Occassion2, present2 = @Present2, recipient3 = @Recipient3, occassion3 = @Occassion3, present3 = @Present3, recipient4 = @Recipient4, occassion4 = @Occassion4, present4 = @Present4, recipient5 = @Recipient5, occassion5 = @Occassion5, present5 = @Present5 WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Recipient1", recipient1);
                insertCommand.Parameters.AddWithValue("@Occassion1", occassion1);
                insertCommand.Parameters.AddWithValue("@Present1", present1);
                insertCommand.Parameters.AddWithValue("@Recipient2", recipient2);
                insertCommand.Parameters.AddWithValue("@Occassion2", occassion2);
                insertCommand.Parameters.AddWithValue("@Present2", present2);
                insertCommand.Parameters.AddWithValue("@Recipient3", recipient3);
                insertCommand.Parameters.AddWithValue("@Occassion3", occassion3);
                insertCommand.Parameters.AddWithValue("@Present3", present3);
                insertCommand.Parameters.AddWithValue("@Recipient4", recipient4);
                insertCommand.Parameters.AddWithValue("@Occassion4", occassion4);
                insertCommand.Parameters.AddWithValue("@Present4", present4);
                insertCommand.Parameters.AddWithValue("@Recipient5", recipient5);
                insertCommand.Parameters.AddWithValue("@Occassion5", occassion5);
                insertCommand.Parameters.AddWithValue("@Present5", present5);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Recipient1", recipient1);
                updateCommand.Parameters.AddWithValue("@Occassion1", occassion1);
                updateCommand.Parameters.AddWithValue("@Present1", present1);
                updateCommand.Parameters.AddWithValue("@Recipient2", recipient2);
                updateCommand.Parameters.AddWithValue("@Occassion2", occassion2);
                updateCommand.Parameters.AddWithValue("@Present2", present2);
                updateCommand.Parameters.AddWithValue("@Recipient3", recipient3);
                updateCommand.Parameters.AddWithValue("@Occassion3", occassion3);
                updateCommand.Parameters.AddWithValue("@Present3", present3);
                updateCommand.Parameters.AddWithValue("@Recipient4", recipient4);
                updateCommand.Parameters.AddWithValue("@Occassion4", occassion4);
                updateCommand.Parameters.AddWithValue("@Present4", present4);
                updateCommand.Parameters.AddWithValue("@Recipient5", recipient5);
                updateCommand.Parameters.AddWithValue("@Occassion5", occassion5);
                updateCommand.Parameters.AddWithValue("@Present5", present5);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string breakfast = richTextBox17.Text;
            string lunch = richTextBox18.Text;
            string dinner = richTextBox20.Text;
            string snack = richTextBox19.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Menu_ideas WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Menu_ideas (breakfast, lunch, dinner, snack, user_id) VALUES (@Breakfast, @Lunch, @Dinner, @Snack, @UserId)";

            string updateQuery = "UPDATE Menu_ideas SET breakfast = @Breakfast, lunch = @Lunch, dinner = @Dinner, Snack = @Snack WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Breakfast", breakfast);
                insertCommand.Parameters.AddWithValue("@Lunch", lunch);
                insertCommand.Parameters.AddWithValue("@Dinner", dinner);
                insertCommand.Parameters.AddWithValue("@Snack", snack);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Breakfast", breakfast);
                updateCommand.Parameters.AddWithValue("@Lunch", lunch);
                updateCommand.Parameters.AddWithValue("@Dinner", dinner);
                updateCommand.Parameters.AddWithValue("@Snack", snack);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string books = richTextBox21.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Books WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Books (books, user_id) VALUES (@Books, @UserId)";

            string updateQuery = "UPDATE Books SET books = @Books WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Books", books);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Books", books);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string movies_and_series = richTextBox24.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Movies_and_series WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Movies_and_series (movies_and_series, user_id) VALUES (@Movies_and_series, @UserId)";

            string updateQuery = "UPDATE Movies_and_series SET movies_and_series = @Movies_and_series WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Movies_and_series", movies_and_series);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Movies_and_series", movies_and_series);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string places_to_visit = richTextBox25.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Places_to_visit WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Places_to_visit (places_to_visit, user_id) VALUES (@Places_to_visit, @UserId)";

            string updateQuery = "UPDATE Places_to_visit SET places_to_visit = @Places_to_visit WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Places_to_visit", places_to_visit);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Places_to_visit", places_to_visit);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string things_to_try = richTextBox26.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Things_to_try WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Things_to_try (things_to_try, user_id) VALUES (@Things_to_try, @UserId)";

            string updateQuery = "UPDATE Things_to_try SET things_to_try = @Things_to_try WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Things_to_try", things_to_try);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Things_to_try", things_to_try);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void Important_dates_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string important_dates = richTextBox27.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Important_dates WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Important_dates (important_dates, user_id) VALUES (@Important_dates, @UserId)";

            string updateQuery = "UPDATE Important_dates SET important_dates = @Important_dates WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Important_dates", important_dates);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Important_dates", important_dates);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string my_focus = richTextBox32.Text;
            string personal_goals = richTextBox28.Text;
            string career_goals = richTextBox29.Text;
            string important = richTextBox30.Text;
            string notes = richTextBox31.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Monthly_plan WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Monthly_plan (my_focus, personal_goals, career_goals, important, notes, user_id) VALUES (@My_focus, @Personal_goals, @Career_goals, @Important, @Notes, @UserId)";

            string updateQuery = "UPDATE Monthly_plan SET my_focus = @My_focus, personal_goals = @Personal_goals, career_goals = @Career_goals, important = @Important, notes = @Notes WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@My_focus", my_focus);
                insertCommand.Parameters.AddWithValue("@Personal_goals", personal_goals);
                insertCommand.Parameters.AddWithValue("@Career_goals", career_goals);
                insertCommand.Parameters.AddWithValue("@Important", important);
                insertCommand.Parameters.AddWithValue("@Notes", notes);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@My_focus", my_focus);
                updateCommand.Parameters.AddWithValue("@Personal_goals", personal_goals);
                updateCommand.Parameters.AddWithValue("@Career_goals", career_goals);
                updateCommand.Parameters.AddWithValue("@Important", important);
                updateCommand.Parameters.AddWithValue("@Notes", notes);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string monthly_calendar = richTextBox33.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Monthly_calendar WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Monthly_calendar (monthly_calendar, user_id) VALUES (@Monthly_calendar, @UserId)";

            string updateQuery = "UPDATE Monthly_calendar SET monthly_calendar = @Monthly_calendar WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Monthly_calendar", monthly_calendar);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Monthly_calendar", monthly_calendar);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string to_do_list = richTextBox34.Text;
            string notes = richTextBox35.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Daily_planner WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Daily_planner (to_do_list, notes, user_id) VALUES (@To_do_list, @Notes, @UserId)";

            string updateQuery = "UPDATE Daily_planner SET to_do_list = @To_do_list, notes = @Notes WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@To_do_list", to_do_list);
                insertCommand.Parameters.AddWithValue("@Notes", notes);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@To_do_list", to_do_list);
                updateCommand.Parameters.AddWithValue("@Notes", notes);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string achieved_goals = richTextBox36.Text;
            string notes = richTextBox37.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Monthly_review WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Monthly_review (achieved_goals, notes, user_id) VALUES (@Achieved_goals, @Notes, @UserId)";

            string updateQuery = "UPDATE Monthly_review SET achieved_goals = @Achieved_goals, notes = @Notes WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Achieved_goals", achieved_goals);
                insertCommand.Parameters.AddWithValue("@Notes", notes);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Achieved_goals", achieved_goals);
                updateCommand.Parameters.AddWithValue("@Notes", notes);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string achieved_goals = richTextBox39.Text;
            string notes = richTextBox38.Text;

            string connectionString = "Data Source=MONI;Initial Catalog=OrganiseMe;Integrated Security=True";

            string selectQuery = "SELECT COUNT(*) FROM Yearly_review WHERE user_id = @UserId";

            string insertQuery = "INSERT INTO Yearly_review (achieved_goals, notes, user_id) VALUES (@Achieved_goals, @Notes, @UserId)";

            string updateQuery = "UPDATE Yearly_review SET achieved_goals = @Achieved_goals, notes = @Notes WHERE user_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Achieved_goals", achieved_goals);
                insertCommand.Parameters.AddWithValue("@Notes", notes);
                insertCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Achieved_goals", achieved_goals);
                updateCommand.Parameters.AddWithValue("@Notes", notes);
                updateCommand.Parameters.AddWithValue("@UserId", Log_in_form.Id);

                try
                {
                    connection.Open();
                    int count = (int)selectCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content updated successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Content saved successfully!");
                        else
                            MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving content: " + ex.Message);
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // Show confirmation dialog
            DialogResult result = MessageBox.Show("Are you sure you want to uncheck all checkboxes?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check if the user clicked Yes
            if (result == DialogResult.Yes)
            {
                // If yes, uncheck all checkboxes
                UncheckAllCheckBoxes();
            }
        }
        private void UncheckAllCheckBoxes()
        {
            foreach (CheckBox checkBox in checkBoxList)
            {
                checkBox.Checked = false;
            }
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set initial directory and filter for image files
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Set the chosen image as the background image of Form1
                    string selectedImagePath = openFileDialog.FileName;
                    Image selectedImage = Image.FromFile(selectedImagePath);

                    pictureBox3.Image = selectedImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not load image from file. " + ex.Message);
                }
            }
        }
    }
}