using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanlyNhanSu_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += new EventHandler(Form_load);
            btnThem.Click += new EventHandler(them);
            btnSua.Click += new EventHandler(Sua);
            btnXoa.Click += new EventHandler(Xoa);

            dataGridView.CellClick += new DataGridViewCellEventHandler(Data_Click); // su kien click dong du lieu
        }

        private void Data_Click(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView.CurrentCell.RowIndex;// Dong duoc chon tren datagridview
            txtMaNV.Text = dataGridView.Rows[i].Cells[0].Value.ToString();
            txtHoTen.Text = dataGridView.Rows[i].Cells[1].Value.ToString();
            dtpNgaySinh.Text = dataGridView.Rows[i].Cells[2].Value.ToString();
            string gt = dataGridView.Rows[i].Cells[3].Value.ToString();
            if (gt.Equals("True"))
                rdNam.Checked = true;
            else
                rdNu.Checked = true;
            txtHeSoLuong.Text = dataGridView.Rows[i].Cells[4].Value.ToString();
            txtSoDT.Text = dataGridView.Rows[i].Cells[5].Value.ToString();
            cboTenPhong.SelectedItem = dataGridView.Rows[i].Cells[6].Value.ToString();
            cboChucVu.SelectedItem = dataGridView.Rows[i].Cells[7].Value.ToString();

        }

        private void Xoa(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                int i = dataGridView.CurrentCell.RowIndex;
                int ma = Convert.ToInt32(dataGridView.Rows[i].Cells[0].Value.ToString());
                string sql = string.Format("DELETE FROM DSNV WHERE MaNV = '{0}'", ma);
                DataProvider.moKetNoi();
                DataProvider.updateData(sql);
                DataProvider.dongKetNoi();
                MessageBox.Show("Xóa Thành công");
                loadDSNV();
            }
        }

        private void Sua(object sender, EventArgs e)
        {
            string sql = string.Format("UPDATE DSNV SET HoTen=@ht, NgaySinh=@ns, GioiTinh=@gt, " +
                "SoDT=@soDT, HeSoLuong=@hsl, MaPhong=@maPhong, MaChucVu=@maCV WHERE MaNV='{0}'", txtMaNV.Text);
            string[] name = { "@ht", "@ns", "@gt", "@soDT", "@hsl", "@maPhong", "@maCV" };

            bool gt = true;
            if (rdNu.Checked == true)
                gt = false;
            object[] value = { txtHoTen.Text, dtpNgaySinh.Value, gt, txtSoDT.Text, float.Parse(txtHeSoLuong.Text), cboTenPhong.SelectedValue.ToString(), cboChucVu.SelectedValue.ToString() };

            DataProvider.moKetNoi();
            DataProvider.updateData(sql, value, name);
            MessageBox.Show("Sửa Thành công");
            DataProvider.dongKetNoi();
            loadDSNV();
        }

        private void them(object sender, EventArgs e)
        {
           // string sql1 = string.Format( "SELECT COUNT (*) FROM DSNV WHERE maNV ='{0}'", txtMaNV.Text);
           // if((DataProvider.checkData(sql1)==0) && (isNumber(txtHeSoLuong.Text)))
            {
                string sql = "INSERT INTO DSNV (HoTen, NgaySinh, GioiTinh, soDT, HeSoLuong,MaPhong,MaChucVu)," +
               "VALUES(@ht, @ns,@gt,@soDT,@hsl,@maPhong,@maCV)";
                string[] name = { "@ht", "@ns", "@gt", "@soDT", "@hsl", "@maPhong", "@maCV" };

                bool gt = true;
                if (rdNu.Checked == true)
                    gt = false;
                object[] value = { txtHoTen.Text, dtpNgaySinh.Value, gt, txtSoDT.Text, float.Parse(txtHeSoLuong.Text), cboTenPhong.SelectedValue.ToString(), cboChucVu.SelectedValue.ToString() };

                DataProvider.moKetNoi();
                DataProvider.updateData(sql, value, name);
                DataProvider.dongKetNoi();
                loadDSNV();
            }
        }

        public  bool isNumber(string value)
        {
            bool ktra;
            float result;
            ktra = float.TryParse(value, out result);
            return ktra;
        }
        #region
        //Load Dữ liệu phòng ban 
        public void loadPB()
        {
            string sql = "SELECT * FROM  DMPHONG";
            //Lấy dữ liệu từ DataSet đổ lên Combobox
            cboTenPhong.DataSource = DataProvider.GetTable(sql);
            cboTenPhong.DisplayMember = "TenPhong";
            cboTenPhong.ValueMember = "MaPhong";
           
        }

        //Load Dữ liệu Chuc vu
        public void loadChucVu()
        {
            string sql = "SELECT * FROM  CHUCVU";
            //Lấy dữ liệu từ DataSet đổ lên Combobox
            cboChucVu.DataSource = DataProvider.GetTable(sql);  
            cboChucVu.DisplayMember = "TenChucVu";
            cboChucVu.ValueMember = "MaChucVu";
        
        }

        //Load Dữ liệu DSNV 
        public void loadDSNV()
        {
            string sql = "SELECT * FROM  DSNV";
          // string sql = "SELECT A.MaNV, A.HoTen, A.NgaySinh, A.GioiTinh, "


            //Lấy dữ liệu từ DataSet đổ lên DataGridView
            dataGridView.DataSource = DataProvider.GetTable(sql);
        }
        #endregion
        private void Form_load(object sender, EventArgs e)
        {
            DataProvider.moKetNoi();
            loadChucVu();
            loadPB();
            loadDSNV();
            DataProvider.dongKetNoi();
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            DataProvider.moKetNoi();
            loadChucVu();
            loadPB();
            loadDSNV();
            DataProvider.dongKetNoi();
        }
    }
}
