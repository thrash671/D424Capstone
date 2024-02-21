using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Data.SqlClient;

namespace EbayProgram
{
    public partial class About : Page
    {
        string cs = "server=thwackgolf.database.windows.net,1433; Database=ebay_cards; uid=thrashj5; pwd=Thwack671!";
        bool deleteClicked = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string displayQuery = "SELECT card_num AS 'Item Number', card_name AS 'Item Name', collection_num AS 'Collection Num', CONCAT('$', listed_value) AS 'Listed Value', CONCAT('$', sold_value) AS 'Sold Value', style_listed AS 'Style Listed', CONCAT('$', shipping) AS 'Shipping Cost' FROM cards";

                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand(displayQuery, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                GridView1.DataSource = ds;
                GridView1.DataBind();

                string getCustomerId = "select card_num from cards";

                SqlConnection con2 = new SqlConnection(cs);
                con2.Open();
                SqlCommand cmd2 = new SqlCommand(getCustomerId, con2);
                SqlDataReader rdr = cmd2.ExecuteReader();
                while (rdr.Read())
                {
                    string name = rdr.GetString(Convert.ToInt16("card_num")); //GetString(\"card_num\");

					listBoxFill.Items.Add(name);
                }
                cmd.Dispose();
                rdr.Close();
                con.Close();
            }
        }

        protected void btnTotalActiveCards_Click(object sender, EventArgs e)
        {
            string displayActive = "SELECT COUNT(*) from cards WHERE sold_value IS NULL";

            SqlConnection con = new SqlConnection(cs);
            SqlCommand MyCommand2 = new SqlCommand(displayActive, con);
            con.Open();

            SqlDataAdapter MyAdapter = new SqlDataAdapter();
            MyAdapter.SelectCommand = MyCommand2;

            txtTotalActiveCards.Text = MyCommand2.ExecuteScalar().ToString();

            con.Close();

        }

        protected void btnDisplayTotal_Click(object sender, EventArgs e)
        {
            string displayTotal = "SELECT CONCAT('$',SUM(sold_value)) FROM cards";

            SqlConnection con = new SqlConnection(cs);
            SqlCommand MyCommand2 = new SqlCommand(displayTotal, con);
            con.Open();

            SqlDataAdapter MyAdapter = new SqlDataAdapter();
            MyAdapter.SelectCommand = MyCommand2;

            txtTotalSold.Text = MyCommand2.ExecuteScalar().ToString();

            con.Close();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            string displayQuery = "SELECT card_num AS 'Item Number', card_name AS 'Item Name', collection_num AS 'Collection Num', CONCAT('$', listed_value) AS 'Listed Value', CONCAT('$', sold_value) AS 'Sold Value', style_listed AS 'Style Listed', CONCAT('$', shipping) AS 'Shipping Cost' FROM cards";

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(displayQuery, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCardName.Text == "" || TxtCollectionNum.Text == "" || txtListValue.Text == "" || txtListStyle.Text == "" || txtShipping.Text == "")
            {
                string message = "Please Enter a Value in all fields";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return;
            }

            SqlConnection con = new SqlConnection(cs);

            string saveQuery = "INSERT INTO cards(card_name, collection_num, listed_value, sold_value, style_listed, shipping) VALUES (@card_name, @collection_num, @listed_value, @sold_value, @style_listed, @shipping)";
            con.Open();
            SqlCommand cmd = new SqlCommand(saveQuery, con);

            cmd.Parameters.AddWithValue("@card_name", txtCardName.Text);
            cmd.Parameters.AddWithValue("@collection_num", TxtCollectionNum.Text);
            cmd.Parameters.AddWithValue("@listed_value", Convert.ToDecimal(txtListValue.Text));
            cmd.Parameters.AddWithValue("@sold_value", string.IsNullOrEmpty(txtSoldValue.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtSoldValue.Text));
            cmd.Parameters.AddWithValue("@style_listed", txtListStyle.Text);
            cmd.Parameters.AddWithValue("@shipping", Convert.ToDecimal(txtShipping.Text));

            cmd.ExecuteNonQuery();
            con.Close();

            ClearTextBoxes();
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            deleteClicked = true;

            string deleteQuery = "DELETE FROM cards WHERE card_num = @card_num";

            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand(deleteQuery, con);
            cmd.Parameters.AddWithValue("@card_num", SqlDbType.Int).Value = listBoxFill.SelectedValue;

            cmd.ExecuteNonQuery();
            con.Close();

            ClearTextBoxes();
        }
        
            protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCardName.Text == "" || TxtCollectionNum.Text == "" || txtListValue.Text == "" || txtListStyle.Text == "" || txtShipping.Text == "")
            {
                string message = "Please Enter a Value in all fields";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return;
            }

            string updateQuery = "UPDATE cards SET card_name = @card_name, collection_num = @collection_num, listed_value = @listed_value, sold_value = @sold_value, style_listed = @style_listed, shipping = @shipping WHERE card_num = @card_num";

            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand(updateQuery, con);

            cmd.Parameters.AddWithValue("@card_num", SqlDbType.Int).Value = listBoxFill.SelectedValue;
            cmd.Parameters.AddWithValue("@card_name", txtCardName.Text);
            cmd.Parameters.AddWithValue("@collection_num", TxtCollectionNum.Text);
            cmd.Parameters.AddWithValue("@listed_value", Convert.ToDecimal(txtListValue.Text));
            cmd.Parameters.AddWithValue("@sold_value", string.IsNullOrEmpty(txtSoldValue.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtSoldValue.Text));
            cmd.Parameters.AddWithValue("@style_listed", txtListStyle.Text);
            cmd.Parameters.AddWithValue("@shipping", Convert.ToDecimal(txtShipping.Text));

            cmd.ExecuteNonQuery();
            con.Close();

            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            txtCardName.Text = "";
            TxtCollectionNum.Text = "";
            txtListValue.Text = "";
            txtSoldValue.Text = "";
            txtListStyle.Text = "";
            txtCardName.Text = "";
            txtShipping.Text = "";

            //Brings the active text box to top most box
            txtCardName.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (listBoxFill.SelectedValue == "")
            {
                string message = "Please Enter an Item Number";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return;
            }

            SqlConnection con = new SqlConnection(cs);
            string displaySelectedName = "SELECT card_name, collection_num, listed_value, sold_value, style_listed, shipping FROM cards WHERE card_num = '" + listBoxFill.SelectedValue + "'";

            SqlDataAdapter da = new SqlDataAdapter(displaySelectedName, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            txtCardName.Text = dt.Rows[0][0] + "";
            TxtCollectionNum.Text = dt.Rows[0][1] + "";
            txtListValue.Text = dt.Rows[0][2] + "";
            txtSoldValue.Text = dt.Rows[0][3] + "";
            txtListStyle.Text = dt.Rows[0][4] + "";
            txtShipping.Text = dt.Rows[0][5] + "";
        }

        protected void btnDisplaySearch_Click(object sender, EventArgs e)
        {
            string cardNameQuery = "SELECT card_num AS 'Item Number', card_name AS 'Item Name', collection_num AS 'Collection Num', CONCAT('$', listed_value) AS 'Listed Value', CONCAT('$', sold_value) AS 'Sold Value', style_listed AS 'Style Listed', CONCAT('$', shipping) AS 'Shipping Cost' FROM cards WHERE card_name LIKE CONCAT('%', @card_name, '%')";
            string collectionNumQuery = "SELECT card_num AS 'Item Number', card_name AS 'Item Name', collection_num AS 'Collection Num', CONCAT('$', listed_value) AS 'Listed Value', CONCAT('$', sold_value) AS 'Sold Value', style_listed AS 'Style Listed', CONCAT('$', shipping) AS 'Shipping Cost' FROM cards WHERE collection_num = @collection_num";         
            string soldCardsQuery = "SELECT card_num AS 'Item Number', card_name AS 'Item Name', collection_num AS 'Collection Num', CONCAT('$', listed_value) AS 'Listed Value', CONCAT('$', sold_value) AS 'Sold Value', style_listed AS 'Style Listed', CONCAT('$', shipping) AS 'Shipping Cost' FROM cards WHERE sold_value IS NOT NULL;";
            string unsoldCardsQuery = "SELECT card_num AS 'Item Number', card_name AS 'Item Name', collection_num AS 'Collection Num', CONCAT('$', listed_value) AS 'Listed Value', CONCAT('$', sold_value) AS 'Sold Value', style_listed AS 'Style Listed', CONCAT('$', shipping) AS 'Shipping Cost' FROM cards WHERE sold_value IS NULL;";

            if (DropDownList1.Text == "Item Name")
            {
                ComboBox("@card_name", cardNameQuery);
            }
            else if (DropDownList1.Text == "Collection Number")
            {
                ComboBox("@collection_num", collectionNumQuery);
            }
            else if (DropDownList1.Text == "Sold Items")
            {
                DisplayTable(soldCardsQuery);
            }
            else if (DropDownList1.Text == "Unsold Items")
            {
                DisplayTable(unsoldCardsQuery);
            }

            txtSearchBox.Text = "";
        }

        private void ComboBox(string param, string query)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand MyCommand2 = new SqlCommand(query, con);

            MyCommand2.Parameters.AddWithValue(param, txtSearchBox.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(MyCommand2);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        private void DisplayTable(string displayQuery)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(displayQuery, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
}