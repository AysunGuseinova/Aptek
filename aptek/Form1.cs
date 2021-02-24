using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace aptek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public SqlConnection con = new SqlConnection("data source=SEYMUR-PC\\SQLEXPRESS;initial catalog=aptek;integrated security=SSPI");
        SqlCommand com;
        SqlDataAdapter da;
        DataSet ds;
        int setir, sutun;
        int i, j;
        int R;
        int K;

        public void goster(string w)
        {
            con.Open();
            da = new SqlDataAdapter(w, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        public void goster1(string w)
        {
            con.Open();
            da = new SqlDataAdapter(w, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
        }

        public void goster2(string w)
        {
            con.Open();
            da = new SqlDataAdapter(w, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            con.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aptekDataSet3.elaqeli' table. You can move, or remove it, as needed.
            this.elaqeliTableAdapter.Fill(this.aptekDataSet3.elaqeli);
            // TODO: This line of code loads data into the 'aptekDataSet2.forma' table. You can move, or remove it, as needed.
            this.formaTableAdapter.Fill(this.aptekDataSet2.forma);
            // TODO: This line of code loads data into the 'aptekDataSet1.nov' table. You can move, or remove it, as needed.
            this.novTableAdapter.Fill(this.aptekDataSet1.nov);
            // TODO: This line of code loads data into the 'aptekDataSet.esas' table. You can move, or remove it, as needed.
            this.esasTableAdapter.Fill(this.aptekDataSet.esas);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkKhaki;
         

            //ALERT!!!!!!!
            
            alertToolStripMenuItem_Click(sender,e);
            if (dataGridView3.RowCount > 0)
                menuStrip1.Items[2].BackColor = Color.Red;
            else menuStrip1.Items[2].BackColor = DefaultBackColor; 
            ///

            dataGridView1.Left = 8;
            dataGridView1.Top = this.Height - dataGridView1.Height - 50;
            dataGridView1.Width = this.Width - 35;


        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("insert into esas(barkod,ad,nov,olke,forma,dose,miqdar,tarix1,tarix2,qiymet,say)values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','"+comboBox6.Text+"','" + textBox3.Text + "','" + textBox4.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "','"+textBox5.Text+"','" + textBox6.Text + "','" + textBox7.Text + "')", con);
            com.ExecuteNonQuery();
            con.Close();
            

            goster("select* from  esas");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime T = dateTimePicker1.Value;
            T = T.AddMonths(18);
            textBox5.Text = T.ToShortDateString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Visible = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Visible = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox4.Visible = checkBox3.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string f = "where ", f1 = "", f2 = "", f3 = "";
            if (checkBox1.Checked) f1 = "ad like '" + textBox9.Text + "'";
            if (checkBox2.Checked) f2 = "nov like '" + comboBox3.Text + "'";
            if (checkBox3.Checked) f3 = "olke like '" + comboBox4.Text + "'";

            if (f1 != "")
            {
                if (f == "where ") f = f + f1; else f = f + " and " + f1;
            }
            if (f2 != "")
            {
                if (f == "where ") f = f + f2; else f = f + " and " + f2;
            }
            if (f3 != "")
            {
                if (f == "where ") f = f + f3; else f = f + " and " + f3;
            }

            if (f == "where ") goster("Select* from esas");
            else
                goster("select * from esas  " + f);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            R = e.RowIndex;
            con.Open();
            da = new SqlDataAdapter("select* from elaqeli where barkod like '" + dataGridView1[0, R].Value.ToString() + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
            groupBox4.Visible = true;

        }


        

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string name = textBox8.Text;
            int B = name.Length;
            if (B > 0)
            {
                char k1 = name[B - 1]; int kod = Convert.ToInt16(k1); kod++;
                char k2 = Convert.ToChar(kod);
                name = name.Remove(B - 1, 1);
                name = name.Insert(B - 1, Convert.ToString(k2));
                goster("Select* from esas where ad>='" + textBox8.Text + "'and ad<'" + name + "'");
            }
            else
            {
                goster("Select* from esas");
            }
       
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
            textBox8.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            con.Open();
            com = new SqlCommand("delete from esas where barkod like '" + comboBox5.Text + "'", con);
            com.ExecuteNonQuery();
            con.Close();


            con.Open();
            com = new SqlCommand("delete from esas where ad like '" + textBox8.Text + "'", con);
            com.ExecuteNonQuery();
            con.Close();
            DialogResult a = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo);
            switch (a)
            {
                case DialogResult.Yes: break;
                case DialogResult.No: break;
            }

            goster("select* from esas");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
             
            if ((e.KeyChar>='a' && e.KeyChar<='z')||(e.KeyChar>='A' && e.KeyChar<='Z')||(e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '.') || (e.KeyChar == 8))
                textBox1.ReadOnly = false;
            else textBox1.ReadOnly = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
             
            if (e.KeyValue == 13)
            {
                if (textBox1.TextLength == 0)
                    MessageBox.Show("Empty");
                else textBox2.Focus();
            }
        
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') ||(e.KeyChar>='0' && e.KeyChar<='9') || (e.KeyChar == '-') || (e.KeyChar == 8))
                textBox2.ReadOnly = false;
            else textBox2.ReadOnly = true;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (textBox2.TextLength == 0)
                    MessageBox.Show("Empty");
                else comboBox1.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '.') || (e.KeyChar == 8))
                textBox3.ReadOnly = false;
            else textBox3.ReadOnly = true;
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (textBox3.TextLength == 0)
                    MessageBox.Show("Empty");
                else textBox4.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '.') || (e.KeyChar == 8))
                textBox4.ReadOnly = false;
            else textBox4.ReadOnly = true;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (textBox4.TextLength == 0)
                    MessageBox.Show("Empty");
                else dateTimePicker1.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '/') || (e.KeyChar == 8))
                textBox5.ReadOnly = false;
            else textBox5.ReadOnly = true;
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (textBox5.TextLength == 0)
                    MessageBox.Show("Empty");
                else textBox6.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '.') || (e.KeyChar == 8))
                textBox6.ReadOnly = false;
            else textBox6.ReadOnly = true;
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (textBox6.TextLength == 0)
                    MessageBox.Show("Empty");
                else textBox7.Focus();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9')  || (e.KeyChar == 8))
                textBox7.ReadOnly = false;
            else textBox7.ReadOnly = true;
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                if (textBox7.TextLength == 0)
                    MessageBox.Show("Empty");
                else button1.Focus();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void alertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime T = DateTime.Now;
            string []B=new string[10];
            int i=-1,k;
            /*con.Open();
            da = new SqlDataAdapter("select* from esas where tarix2+10='"+T+"'",con);
             ds = new DataSet();
            da.Fill(ds);
            con.Close();
             * */
            con.Open();
            com = new SqlCommand("select* from esas",con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
               DateTime t=Convert.ToDateTime(dr["tarix2"].ToString());
               //MessageBox.Show(t.AddDays(10).ToShortDateString()+"   "+T.ToShortDateString());
               if (t.AddDays(10) >= T) { i++; B[i] = dr["barkod"].ToString(); }
            }
            con.Close();
            k=i;
            string P = "";
            for (i = 0; i <= k; i++)
            {

                if (i<k)
            P+="barkod like '"+B[i]+"' or ";
                else
                    P+="barkod like '"+B[i]+"'";
            }
            //MessageBox.Show(P);
            //MessageBox.Show("select* from esas where "+P);
                con.Open();
                da = new SqlDataAdapter("select* from esas where "+P, con);
                ds = new DataSet();
                da.Fill(ds);
                con.Close();
            

            dataGridView3.DataSource = ds.Tables[0];
            if (menuStrip1.Items[2].BackColor == Color.Red)
               // dataGridView3.Visible = true;
            groupBox5.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            groupBox5.Visible = false;
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
           
            con.Open();
            com = new SqlCommand("delete from esas where barkod like '" + dataGridView3[j,i].Value.ToString() + "'", con);
            com.ExecuteNonQuery();
            con.Close();

            DialogResult a = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo);
            switch (a)
            {
                case DialogResult.Yes: break;
                case DialogResult.No: break;
            }
            goster2("select* from esas");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            int say = 0;
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select* from  esas  where ad like '" + textBox9.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();

            say = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[10].ToString());
            if (say >= Convert.ToInt32(textBox10.Text))
            {
               //MessageBox.Show(say.ToString());
                con.Open();
               
                say -= Convert.ToInt32(textBox10.Text);
                com = new SqlCommand("update  esas  set say = " + say + " where ad like '" + textBox9.Text + "'", con);
                com.ExecuteNonQuery();
                con.Close();
            }
            else MessageBox.Show("O qeder mal yoxdur!");
            goster("select* from esas");
        }

    }
        
        
    
}
