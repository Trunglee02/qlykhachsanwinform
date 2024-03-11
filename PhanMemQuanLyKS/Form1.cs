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
using System.Net.NetworkInformation;

namespace PhanMemQuanLyKS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string sql;
        string chuoiketnoi = @"Data Source=TRUNGLE\LEVANTRUNG;Initial Catalog=QLKhachSan;Integrated Security=True";
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulie;
        int i = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet11.PHONG' table. You can move, or remove it, as needed.
            this.pHONGTableAdapter.Fill(this.dataSet11.PHONG);
            
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

        private void btnTKMaP_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = @"Select MaKH, HoTen, NgayDen, MaPhong, DiaChi From KHACHHANG Where (MaPhong like '%"+comboBox1.Text+"%')";
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

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = @"Select MaKH, HoTen, NgayDen, MaPhong, DiaChi From KHACHHANG Where (HoTen like '%" + textBox1.Text + "%')";
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

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            ketnoi.Open();
            sql = @"SELECT PHONG.MaPhong, KHACHHANG.HoTen, DATEDIFF(day, KHACHHANG.NgayDen, GETDATE()), DATEDIFF(day, KHACHHANG.NgayDen, GETDATE()) * PHONG.DonGia 
FROM     PHONG INNER JOIN KHACHHANG ON PHONG.MaPhong = KHACHHANG.MaPhong
WHERE    (KHACHHANG.MaPhong = N'" + comboBox1.Text + @"') AND (KHACHHANG.HoTen = N'" + textBox1.Text + @"') ";

            thuchien = new SqlCommand(sql, ketnoi);
            docdulie = thuchien.ExecuteReader();
            
            while (docdulie.Read())
            {
                lblphong.Text = docdulie[0].ToString();
                lblluutru.Text = docdulie[2].ToString();
                lblthanhtoan.Text = docdulie[3].ToString();
                i++;
            }
            ketnoi.Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult f =MessageBox.Show("Bạn có muốn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(f == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.ShowDialog(); // Hiển thị Form3 và chờ cho đến khi nó được đóng lại

            // Sau khi Form3 đóng, tải lại dữ liệu từ cơ sở dữ liệu
            hienthi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ketnoi.Open();
            sql = @"SELECT SUM(DATEDIFF(day, KHACHHANG.NgayDen, GETDATE()) * PHONG.DonGia) AS TongTienThu
            FROM PHONG INNER JOIN KHACHHANG ON PHONG.MaPhong = KHACHHANG.MaPhong";
            thuchien = new SqlCommand(sql, ketnoi);
            object result = thuchien.ExecuteScalar();
            ketnoi.Close();

            if (result != null && result != DBNull.Value)
            {
                decimal tongTienThu = Convert.ToDecimal(result);
                MessageBox.Show("Tổng tiền thu của khách hàng: " + tongTienThu.ToString("N0") + " đồng", "Thống kê tiền thu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu tiền thu để thống kê", "Thống kê tiền thu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; // Xóa nội dung của TextBox tìm kiếm
            lblphong.Text = "";
            lblluutru.Text = "";
            lblthanhtoan.Text = "";

            hienthi(); // Load lại dữ liệu cho ListView
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();

            f.ShowDialog();
        }
    }
}
