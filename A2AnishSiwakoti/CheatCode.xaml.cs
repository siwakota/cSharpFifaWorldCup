using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace A2AnishSiwakoti
{
    /// <summary>
    /// Interaction logic for CheatCode.xaml
    /// </summary>
    public partial class CheatCode : Window
    {
        public CheatCode()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        // Logic to populate the combobox from the teams table
        void InitializeComboBox()
        {
            List<string> TeamNames = new List<string>();

            using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
            {
                string query = "SELECT TeamName from Teams";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();

                table.Load(reader);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    TeamNames.Add(table.Rows[i]["TeamName"].ToString());
                }
            }
            cmbTeamNames.ItemsSource = TeamNames;
        }

        // Logic to update the teams table with cheat team
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTeamNames.SelectedItem != null)
            {
                using (SqlConnection conn = new SqlConnection(Data.ConnectionStr))
                {
                    // Since there can be only 1 cheat team, resetting the teams table in order to avoid error
                    string query = "Update Teams SET CheatTeam = 0";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    string query1 = "Update Teams SET CheatTeam = 1 Where TeamName = @teamName";
                    cmd.Parameters.AddWithValue("teamName", cmbTeamNames.SelectedValue.ToString());

                    cmd.CommandText = query1;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(cmbTeamNames.SelectedValue.ToString() + " is selected as the Cheat Team.");
                    }
                    else
                    {
                        MessageBox.Show("No Cheat Team is Selected.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No Selection Made.");
            }
            CloseWindow();

        }

        //Closes cheat code window on cancel button click
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        //Method to close the window
        void CloseWindow()
        {
            this.Close();
        }
    }
}
