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
    public partial class Form4 : Form
    {
        private string chuoiketnoi = @"Data Source=TRUNGLE\LEVANTRUNG;Initial Catalog=QLKhachSan;Integrated Security=True";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadRoomList();
        }
        private void LoadRoomList()
        {
            listView1.Items.Clear();

            using (SqlConnection ketnoi = new SqlConnection(chuoiketnoi))
            {
                string sql = "SELECT MaPhong FROM PHONG";
                SqlCommand thuchien = new SqlCommand(sql, ketnoi);

                ketnoi.Open();

                SqlDataReader docdulie = thuchien.ExecuteReader();

                while (docdulie.Read())
                {
                    string maPhong = docdulie["MaPhong"].ToString();

                    // Kiểm tra trạng thái phòng
                    string trangThai = GetRoomStatus(maPhong);

                    ListViewItem item = new ListViewItem(new string[] { maPhong, trangThai });

                    listView1.Items.Add(item);
                }

                ketnoi.Close();
            }
        }

        private string GetRoomStatus(string maPhong)
        {
            using (SqlConnection ketnoi = new SqlConnection(chuoiketnoi))
            {
                string sql = "SELECT COUNT(*) FROM KHACHHANG WHERE MaPhong = @MaPhong";
                SqlCommand thuchien = new SqlCommand(sql, ketnoi);
                thuchien.Parameters.AddWithValue("@MaPhong", maPhong);

                ketnoi.Open();

                int khachHangCount = (int)thuchien.ExecuteScalar();

                ketnoi.Close();

                if (khachHangCount > 0)
                {
                    return "Đã có khách";
                }
                else
                {
                    return "Trống";
                }
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string maPhong = listView1.SelectedItems[0].Text;
                string trangThai = listView1.SelectedItems[0].SubItems[1].Text;

                MessageBox.Show("Phòng " + maPhong + " - Trạng thái: " + trangThai);
            }
        }
    }
}
