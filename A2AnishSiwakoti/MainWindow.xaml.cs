using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace A2AnishSiwakoti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadAllGroupList();
            DisableButtons();
            EnableButton(btnDrawTeamsR16);
            EnableButton(btnCheatCode);
        }

        // Method to disable all the buttons in the application
        void DisableButtons()
        {
            btnDrawTeamsR16.IsEnabled = false;
            btnPlayRounds.IsEnabled = false;
            btnDrawTeamsQF.IsEnabled = false;
            btnPlayQF.IsEnabled = false;
            btnDrawTeamsSF.IsEnabled = false;
            btnPlaySF.IsEnabled = false;
            btnDrawTeamsF.IsEnabled = false;
            btnPlayF.IsEnabled = false;
            btnCheatCode.IsEnabled = false;
        }

        //Method to enable a specific button
        void EnableButton(Button b)
        {
            b.IsEnabled = true;
        }


        string cheatGroupName;          // This string variable stores the group name of the cheat team
        int counter = 0;                // This counter is set up to choose cheat team

        // Method to initialize the database. This method is called at application startup to initialize the database to its initial state removing all the changes made in the previous application run
        void InitializeDatabase()
        {
            using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
            {
                string q1 = "Update Teams SET RoundOf16Winner = @Val, QuarterFinalWinner = @Val, SemiFinalWinner = @Val, FinalWinner = @Val, CheatTeam = @Val";
                SqlCommand cmd1 = new SqlCommand(q1, conn);
                cmd1.Parameters.AddWithValue("Val", "0");

                string q2 = "TRUNCATE TABLE Matches";
                SqlCommand cmd = new SqlCommand(q2, conn);

                conn.Open();

                cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }
        }

        // Display all the teams from the team table into specific listboxes
        void LoadAllGroupList()
        {
            List<string> groupNames = new List<string>()
            { "A","B","C","D","E","F","G","H"};

            List<ListBox> allListBoxes = new List<ListBox>()
            {lstGrpA,lstGrpB,lstGrpC,lstGrpD,lstGrpE,lstGrpF,lstGrpG,lstGrpH};

            for (int i = 0; i < allListBoxes.Count; i++)
            {
                using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
                {
                    string query = "Select TeamName from Teams where GroupName = @GrpName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("GrpName", groupNames[i]);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable tblGroups = new DataTable();
                    tblGroups.Load(reader);
                    allListBoxes[i].ItemsSource = tblGroups.DefaultView;
                    allListBoxes[i].DisplayMemberPath = "TeamName";
                }
            }

        }

        // This method takes in 2 parameters, textbox and a string. And this returns a winning team from list into the Round of 16 match.
        string UniqueTeamName(TextBox textbox, string groupName)
        {
            Random r1 = new Random();

            int choice = r1.Next(1, 5);
            int index = r1.Next(0, 2);

            string query = "";

            if ((CheckCheatTeam() != "" && groupName.Equals(cheatGroupName.Replace(" ", ""))))
            {
                if (counter == 0)
                {
                    ++counter;
                    textbox.Text = CheckCheatTeam();
                }
                else
                {
                    counter = 3;
                }
            }
            if ((counter == 0 || counter == 3))
            {
                // Here the random values returned will be ranging from 1-4
                // So Probability of getting each number is 0.25.
                if (choice == 1)
                {
                    // This case has probability of 0.25. So there is 25% chance for teams with winning factor 0.3 to be selected.
                    query = "SELECT TeamName from Teams where GroupName=@GrpName AND WinningFactor = 0.3";
                }
                else
                {
                    //This case has probabily of remaining 0.75. More probabilty, more chances for teams with winning factor of 0.7 to be selected.
                    query = "SELECT TeamName from Teams where GroupName=@GrpName AND WinningFactor = 0.7";
                }

                using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("GrpName", groupName);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable tblGroups = new DataTable();
                    tblGroups.Load(reader);

                    textbox.Text = tblGroups.Rows[index]["TeamName"].ToString();
                }
            }
            return textbox.Text;
        }

        // This method has 2 parameters both are textboxes. This method returns the goal scored by a specified team in the textbox
        string GenerateGoals(TextBox teamName, TextBox teamGoals)
        {
            Random random = new Random();
            DataTable tblWF = new DataTable();

            using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
            {
                string query = "SELECT WinningFactor from Teams where TeamName = @tName";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("tName", teamName.Text);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                tblWF.Load(reader);


                // This logic is to set to score 2 or 3 goals. Since the loop runs until and unless the goal scored is not same. So cheat team always wins
                if (teamName.Text.Equals(CheckCheatTeam()))
                {
                    teamGoals.Text = random.Next(3, 5).ToString();
                }

                else
                {
                    // Teams with winning factor 0.7 can score 1, 2 or 3 goals.
                    if (tblWF.Rows[0].ToString().Equals("0.7"))
                    {
                        teamGoals.Text = random.Next(1, 4).ToString();
                    }
                    else
                    {
                        // Teams with winning factor 0.3 can score 0,1,2 or 3.
                        // This means there is chance for teams with lower winning factor to have greater goals.
                        teamGoals.Text = random.Next(0, 4).ToString();
                    }
                }
                /* tblWF.Clear();*/
            }
            return teamGoals.Text;
        }


        // To return the teamId of the winner team from teams table
        int ReturnMatchWinnerId(int matchNumber)
        {
            using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
            {
                string query = "SELECT WinningTeamId FROM Matches WHERE MatchNumber = @matchNumber;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("matchNumber", matchNumber);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable tbl = new DataTable();
                tbl.Load(reader);
                return (int)tbl.Rows[0]["WinningTeamId"];
            }
        }


        // Storing each match details into the database.
        void MatchDetailstoDB(int matchNum, TextBox textBox1, TextBox team1Goal, TextBox textBox2, TextBox team2Goal, string stage)
        {
            int val = 0;

            List<TextBox> textBoxes = new List<TextBox>()
            { textBox1, textBox2 };

            DataTable tbl = new DataTable();

            stage = stage.Replace(" ", "");

            using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
            {
                string query1;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                conn.Open();

                foreach (TextBox t in textBoxes)
                {
                    string query = "Select * from Teams where TeamName = @teamName;";

                    cmd.CommandText = query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("teamName", t.Text);

                    SqlDataReader reader = cmd.ExecuteReader();
                    tbl.Load(reader);
                    reader.Close();
                }

                if (int.Parse(team1Goal.Text) > int.Parse(team2Goal.Text))
                {
                    query1 = "UPDATE Teams SET " + stage + "Winner = 1 where TeamName = @team1";
                }
                else
                {
                    query1 = "UPDATE Teams SET " + stage + "Winner = 1 where TeamName = @team2";

                    val = 1;

                }

                cmd.CommandText = query1;
                cmd.Parameters.AddWithValue("team1", textBox1.Text);
                cmd.Parameters.AddWithValue("team2", textBox2.Text);
                cmd.ExecuteNonQuery();

                string query2 = "INSERT INTO Matches (MatchNumber, Stage, TeamOneId, TeamTwoId, TeamOneGoals, TeamTwoGoals, WinningTeamId) VALUES (@matchNum, @stage , @t1Id, @t2Id, @t1Goals, @t2Goals, @winTeam)";

                cmd.CommandText = query2;

                cmd.Parameters.AddWithValue("matchNum", matchNum);
                cmd.Parameters.AddWithValue("stage", stage);
                cmd.Parameters.AddWithValue("t1Id", tbl.Rows[0]["TeamId"]);
                cmd.Parameters.AddWithValue("t2Id", tbl.Rows[1]["TeamId"]);
                cmd.Parameters.AddWithValue("t1Goals", int.Parse(team1Goal.Text));
                cmd.Parameters.AddWithValue("t2Goals", int.Parse(team2Goal.Text));

                if (val == 0)
                {
                    cmd.Parameters.AddWithValue("winTeam", tbl.Rows[0]["TeamId"]);
                }
                else
                {
                    cmd.Parameters.AddWithValue("winTeam", tbl.Rows[1]["TeamId"]);
                }

                cmd.ExecuteNonQuery();

            }
        }


        // Logic to change the color of the textboxes on the basis of goal scored by the teams
        void ChangeColor(TextBox textBox1, TextBox textBox2)
        {
            if (int.Parse(textBox1.Text) > int.Parse(textBox2.Text))
            {
                textBox1.Background = new SolidColorBrush(Colors.LightGreen);
                textBox2.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                textBox2.Background = new SolidColorBrush(Colors.LightGreen);
                textBox1.Background = new SolidColorBrush(Colors.Red);
            }
        }

        // Logic to check the cheat teams in the database.
        string CheckCheatTeam()
        {
            string cheatTeamName = "";

            using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
            {

                string query = "SELECT * from Teams where CheatTeam = 1";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable tbl = new DataTable();
                tbl.Load(reader);

                if (tbl.Rows.Count > 0)
                {
                    cheatTeamName = tbl.Rows[0]["TeamName"].ToString();
                    cheatGroupName = tbl.Rows[0]["GroupName"].ToString();  //Global string variable stores the group name of the cheat team
                }
            }
            return cheatTeamName;
        }

        // Logic to populate the textboxes in the Round of 16 tab on Draw Teams button click
        private void btnDrawTeams_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            CheckCheatTeam();

            // This list contains all the textboxes that hold the team names in the round of 16 tab.
            List<TextBox> rOf16TxtTeamNames = new List<TextBox>()
            { txtMatch11, txtMatch32, txtMatch31, txtMatch12,txtMatch21,txtMatch42, txtMatch41,txtMatch22, txtMatch51, txtMatch72,txtMatch71, txtMatch52,txtMatch61,txtMatch82,txtMatch81, txtMatch62};

            // List containing the group names for all the teams in the teams table.
            List<string> rOf16TeamGroups = new List<string>()
            { "A","B","C","D","E","F","G","H"};


            for (int i = 0; i < rOf16TxtTeamNames.Count; i += 2)
            {
                //Keeps on fetching team names from the teams table until they are not same in 2 different textfields
                while (UniqueTeamName(rOf16TxtTeamNames[i], rOf16TeamGroups[i / 2]).Equals(UniqueTeamName(rOf16TxtTeamNames[i + 1], rOf16TeamGroups[(i + 1) / 2])))
                {
                    rOf16TxtTeamNames[i].Text = UniqueTeamName(rOf16TxtTeamNames[i], rOf16TeamGroups[i / 2]);
                    rOf16TxtTeamNames[i + 1].Text = UniqueTeamName(rOf16TxtTeamNames[i + 1], rOf16TeamGroups[(i + 1) / 2]);
                }
            }
            EnableButton(btnPlayRounds);
        }

        //Logic to generate goals into the textboxes
        private void btnPlayRounds_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();

            // This list contains all the textboxes that hold the team names in the round of 16 tab.

            List<TextBox> rOf16TxtTeamNames = new List<TextBox>()
            {txtMatch11, txtMatch12, txtMatch21, txtMatch22, txtMatch31, txtMatch32, txtMatch41, txtMatch42, txtMatch51, txtMatch52, txtMatch61, txtMatch62, txtMatch71, txtMatch72, txtMatch81, txtMatch82 };

            // This list contains all the textboxes that hold the team goals in the round of 16 tab.

            List<TextBox> rOf16TxtGoals = new List<TextBox>()
            { txtGoalM1T1,txtGoalM1T2, txtGoalM2T1, txtGoalM2T2, txtGoalM3T1, txtGoalM3T2, txtGoalM4T1,txtGoalM4T2, txtGoalM5T1,txtGoalM5T2, txtGoalM6T1, txtGoalM6T2, txtGoalM7T1, txtGoalM7T2, txtGoalM8T1, txtGoalM8T2};


            for (int i = 0; i < rOf16TxtTeamNames.Count; i += 2)
            {
                // Loop to generate random and different goals scored by the two teams
                while (GenerateGoals(rOf16TxtTeamNames[i], rOf16TxtGoals[i]).Equals(GenerateGoals(rOf16TxtTeamNames[i + 1], rOf16TxtGoals[i + 1])))
                {
                    rOf16TxtGoals[i].Text = GenerateGoals(rOf16TxtTeamNames[i], rOf16TxtGoals[i]);
                    rOf16TxtGoals[i + 1].Text = GenerateGoals(rOf16TxtTeamNames[i + 1], rOf16TxtGoals[i + 1]);
                }
                // Method called inside this for loop to work for all the textboxes in the tab
                ChangeColor(rOf16TxtGoals[i], rOf16TxtGoals[i + 1]);

                //Method called inside the loop to store the match details one by one
                MatchDetailstoDB(((i / 2) + 1), rOf16TxtTeamNames[i], rOf16TxtGoals[i], rOf16TxtTeamNames[i + 1], rOf16TxtGoals[i + 1], "Round Of 16");
            }
            EnableButton(btnDrawTeamsQF);


        }

        // Button to select winners in the round of 16 matches and insert their names in respective textboxes
        private void btnDrawTeamsQF_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();

            //List of textboxes containing the textboxes for team names in Quarter Final Match
            List<TextBox> qfTxtTeamNames = new List<TextBox>()
            {
                txtMatch91, txtMatch92, txtMatch101, txtMatch102, txtMatch111, txtMatch112, txtMatch121, txtMatch122
            };

            DataTable tbl = new DataTable();

            for (int i = 0; i < qfTxtTeamNames.Count; i++)
            {
                using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
                {
                    string query = "SELECT * FROM Teams WHERE TeamId = @WinnerTeam;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("WinnerTeam", ReturnMatchWinnerId(i + 1));

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    tbl.Load(reader);

                    qfTxtTeamNames[i].Text = tbl.Rows[i]["TeamName"].ToString();
                }
            }


            EnableButton(btnPlayQF);
        }

        // Logic to generate goals for each in quarter final match
        private void btnPlayQF_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();

            List<TextBox> qfTxtTeamNames = new List<TextBox>()
            {
                txtMatch91, txtMatch92, txtMatch101, txtMatch102, txtMatch111, txtMatch112, txtMatch121, txtMatch122
            };

            List<TextBox> qfTxtGoals = new List<TextBox>()
            {
                txtGoalM9T1,txtGoalM9T2, txtGoalM10T1, txtGoalM10T2, txtGoalM11T1, txtGoalM11T2, txtGoalM12T1, txtGoalM12T2
            };

            for (int i = 0; i < qfTxtTeamNames.Count; i += 2)
            {
                // Loop to generate random and different goals scored by the two teams
                while (GenerateGoals(qfTxtTeamNames[i], qfTxtGoals[i]).Equals(GenerateGoals(qfTxtTeamNames[i + 1], qfTxtGoals[i + 1])))
                {
                    qfTxtGoals[i].Text = GenerateGoals(qfTxtTeamNames[i], qfTxtGoals[i]);
                    qfTxtGoals[i + 1].Text = GenerateGoals(qfTxtTeamNames[i + 1], qfTxtGoals[i + 1]);
                }

                ChangeColor(qfTxtGoals[i], qfTxtGoals[i + 1]);

                MatchDetailstoDB((i / 2) + 9, qfTxtTeamNames[i], qfTxtGoals[i], qfTxtTeamNames[i + 1], qfTxtGoals[i + 1], "Quarter Final");
            }

            EnableButton(btnDrawTeamsSF);
        }

        // Button to select winners in the quarter final matches and insert their names in respective textboxes
        private void btnDrawTeamsSF_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();

            List<TextBox> sfTxtTeamNames = new List<TextBox>()
            {
                txtMatch131, txtMatch132, txtMatch141, txtMatch142
            };

            DataTable tbl = new DataTable();

            for (int i = 0; i < sfTxtTeamNames.Count; i++)
            {
                using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
                {
                    string query = "SELECT * FROM Teams WHERE TeamId = @WinnerTeam;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("WinnerTeam", ReturnMatchWinnerId(i + 9));

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    tbl.Load(reader);

                    sfTxtTeamNames[i].Text = tbl.Rows[i]["TeamName"].ToString();
                }
            }
            EnableButton(btnPlaySF);
        }

        // Logic to generate goals for each in semi final match
        private void btnPlaySF_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();

            List<TextBox> sfTxtTeamNames = new List<TextBox>()
            {
                txtMatch131, txtMatch132, txtMatch141, txtMatch142
            };
            List<TextBox> sfTxtGoals = new List<TextBox>()
            {
                txtGoalM13T1, txtGoalM13T2, txtGoalM14T1, txtGoalM14T2
            };

            for (int i = 0; i < sfTxtTeamNames.Count; i += 2)
            {
                while (GenerateGoals(sfTxtTeamNames[i], sfTxtGoals[i]).Equals(GenerateGoals(sfTxtTeamNames[i + 1], sfTxtGoals[i + 1])))
                {
                    sfTxtGoals[i].Text = GenerateGoals(sfTxtTeamNames[i], sfTxtGoals[i]);
                    sfTxtGoals[i + 1].Text = GenerateGoals(sfTxtTeamNames[i + 1], sfTxtGoals[i + 1]);
                }

                ChangeColor(sfTxtGoals[i], sfTxtGoals[i + 1]);
                MatchDetailstoDB((i / 2) + 13, sfTxtTeamNames[i], sfTxtGoals[i], sfTxtTeamNames[i + 1], sfTxtGoals[i + 1], "Semi Final");
            }
            EnableButton(btnDrawTeamsF);
        }

        // Button to select winners in the semi final matches and insert their names in respective textboxes
        private void btnDrawTeamsF_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();

            List<TextBox> fTxtTeamNames = new List<TextBox>()
            {
                txtMatch151, txtMatch152
            };

            DataTable tbl = new DataTable();

            for (int i = 0; i < fTxtTeamNames.Count; i++)
            {
                using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
                {
                    string query = "SELECT * FROM Teams WHERE TeamId = @WinnerTeam;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("WinnerTeam", ReturnMatchWinnerId(i + 13));

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    tbl.Load(reader);

                    fTxtTeamNames[i].Text = tbl.Rows[i]["TeamName"].ToString();
                }
            }

            EnableButton(btnPlayF);
        }


        // Logic to generate goals for each in the final match
        private void btnPlayF_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();

            List<TextBox> fTxtTeamNames = new List<TextBox>()
            {
                txtMatch151, txtMatch152
            };

            List<TextBox> fTxtGoals = new List<TextBox>()
            {
                txtGoalM15T1, txtGoalM15T2
            };

            for (int i = 0; i < fTxtTeamNames.Count; i += 2)
            {
                while (GenerateGoals(fTxtTeamNames[i], fTxtGoals[i]).Equals(GenerateGoals(fTxtTeamNames[i + 1], fTxtGoals[i + 1])))
                {
                    fTxtGoals[i].Text = GenerateGoals(fTxtTeamNames[i], fTxtGoals[i]);
                    fTxtGoals[i + 1].Text = GenerateGoals(fTxtTeamNames[i + 1], fTxtGoals[i + 1]);
                }

                ChangeColor(fTxtGoals[i], fTxtGoals[i + 1]);
                MatchDetailstoDB((i / 2) + 15, fTxtTeamNames[i], fTxtGoals[i], fTxtTeamNames[i + 1], fTxtGoals[i + 1], "Final");
            }

            lblMessage.Content = "FIFA WORLD CUP WINNER";

            if (int.Parse(txtGoalM15T1.Text) > (int.Parse(txtGoalM15T2.Text)))
            {
                fLblTeamName.Content = txtMatch151.Text;
            }
            else
            {
                fLblTeamName.Content = txtMatch152.Text;
            }
        }

        //Opens cheatcode window on click
        private void btnCheatCode_Click(object sender, RoutedEventArgs e)
        {
            CheatCode window = new CheatCode();
            window.Show();
        }
    }
}