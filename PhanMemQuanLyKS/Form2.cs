using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyKS
{
    public partial class Form2 : Form
    {
        #region Variabales
        public static string connectionString = @"Data Source=TRUNGLE\LEVANTRUNG;Initial Catalog=QLKhachSan;Integrated Security=True";
        public static string LoaiTK = "-1";
        #endregion
        public Form2()
        {
            InitializeComponent();
            lblError.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 

                lblError.Text = "";
                if (textBox1.Text != null && textBox1.Text.Trim() != "") { }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản");
                    textBox1.Focus();
                    return;
                }


                if (textBox2.Text != null && textBox2.Text.Trim() != "") { }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu");
                    textBox2.Focus();
                    return;
                }
               

                
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string TenTK = textBox1.Text.Trim();
                string MatKhau = textBox2.Text.Trim();
                string query = "SELECT * FROM DANGNHAP Where TenTK ='" + TenTK + "' AND MatKhau= '" + MatKhau + "'";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Form1 f = new Form1();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    lblError.Text = "Tài khoản hoặc mật khẩu chưa chính xác, vui lòng kiểm tra lại";
                }

            }

            catch (Exception ex) {
                lblError.Text = ex.Message;
            }
            
        }
        private void lblError_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
           



