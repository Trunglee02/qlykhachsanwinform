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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PhanMemQuanLyKS
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        string sql;
        string chuoiketnoi = @"Data Source=TRUNGLE\LEVANTRUNG;Initial Catalog=QLKhachSan;Integrated Security=True";
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulie;
        int i = 0;

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.PHONG' table. You can move, or remove it, as needed.
            this.pHONGTableAdapter.Fill(this.dataSet1.PHONG);
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        public void hienthi()
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = @"Select MaKH, HoTen, NgayDen, MaPhong, DiaChi From KHACHHANG";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulie = thuchien.ExecuteReader();
            i = 0;
            while (docdulie.Read())
            {
                listView1.Items.Add(docdulie[0].ToString());
                listView1.Items[i].SubItems.Add(docdulie[1].ToString());
                DateTime ngayDen = (DateTime)docdulie[2]; // Ép kiểu sang DateTime
    listView1.Items[i].SubItems.Add(ngayDen.ToShortDateString());
                listView1.Items[i].SubItems.Add(docdulie[3].ToString());
                listView1.Items[i].SubItems.Add(docdulie[4].ToString());
                i++;
            }
            ketnoi.Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            comboBox2.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        /*private bool KiemTraTrungMaPhong(string maPhong)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems[3].Text == maPhong)
                {
                    return true;
                }
            }
            return false;
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            /*string maPhong = comboBox2.Text;

            if (KiemTraTrungMaPhong(maPhong))
            {
                MessageBox.Show("Mã phòng đã tồn tại trong danh sách khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            listView1.Items.Clear();
            ketnoi.Open();
            sql = @"Insert Into KHACHHANG(MaKH, HoTen, NgayDen, MaPhong, DiaChi)
         VALUES       (N'" + textBox1.Text + @"', N'" + textBox2.Text + @"', N'" + dateTimePicker1.Value + @"', N'" 
                + comboBox2.Text + @"', N'" + textBox3.Text + @"')";
            thuchien = new SqlCommand(sql,ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = @"Delete FROM KHACHHANG  Where (MaKH = N'" + textBox1.Text + @"') ";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = @"UPDATE KHACHHANG SET MaKH = N'" + textBox1.Text + @"', HoTen = N'" + textBox2.Text + @"', 
            NgayDen = N'" + dateTimePicker1.Value + @"', MaPhong = N'" + comboBox2.Text + @"', DiaChi = N'" + textBox3.Text + @"'
WHERE       (MaPhong = N'" + comboBox2.Text + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
                this.Close(); // Đóng Form con
                Form1 form1 = new Form1();
                form1.Show(); // Hiển thị Form cha
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
