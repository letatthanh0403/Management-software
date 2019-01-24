﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBH.Control;
using QLBH.Object;

namespace QLBH.View
{
    public partial class HoaDon : UserControl
    {
        HoaDonCtrl hdCtr = new HoaDonCtrl();
        ChiTietHDCtr ctCtr = new ChiTietHDCtr();
        HangHoaCtr hhctr = new HangHoaCtr();
        NhanVienCtr nvctr = new NhanVienCtr();
        DataTable dtDSCT = new System.Data.DataTable();
        int vitriclick = 0;
        public static HoaDon formhd = new HoaDon();
        public HoaDon()
        {
            InitializeComponent();
        }
        private void clearData()
        {
            DataTable dt = new DataTable();
            dt = hdCtr.GetID();
            int ma = int.Parse(dt.Rows[0][0].ToString()) + 1;
            txtMa.Text = ma.ToString();
            txtMa.Focus();
            txtMa.Enabled = false;
            txtTen.Text = bientoancuc.tennv;
            txtTen.Enabled = false;
            txtNgayLap.Text = DateTime.Now.Date.ToShortDateString();
            LoadcmbKhachHang();
            txtDonGia.Enabled = false;
        }
        private void Dis_Enl(bool e)
        {
            txtMa.Enabled = e;
            txtTen.Enabled = e;
            cmbKhachHang.Enabled = e;
            btnAdd.Enabled = !e;
            btnBot.Enabled = !e;
           
            btnSave.Enabled = e;
            btnCancel.Enabled = e;
           
            btnThem.Enabled = e;
            btnBot.Enabled = e;
            cmbHH.Enabled = e;
            txtSL.Enabled = e;
        }
        private void bingding()
        {
            txtMa.DataBindings.Clear();
            txtMa.DataBindings.Add("Text", dtgvDSHD.DataSource, "MaHD");
            txtNgayLap.DataBindings.Clear();
            txtNgayLap.DataBindings.Add("Text", dtgvDSHD.DataSource, "NgayDH");
            txtTen.DataBindings.Clear();
            txtTen.DataBindings.Add("Text", dtgvDSHD.DataSource, "TenNV");
            cmbKhachHang.DataBindings.Clear();
            cmbKhachHang.DataBindings.Add("Text", dtgvDSHD.DataSource, "TenKH");
          

        }
     private void bing()
        {
            cmbHH.DataBindings.Clear();
            cmbHH.DataBindings.Add("Text", dtgvDSHH.DataSource, "HangHoa");
            txtSL.DataBindings.Clear();
            txtSL.DataBindings.Add("Text", dtgvDSHH.DataSource, "SoLuong");
            txtDonGia.DataBindings.Clear();
            txtDonGia.DataBindings.Add("Text", dtgvDSHH.DataSource, "DonGia");
            lbThanhTien.DataBindings.Clear();
            lbThanhTien.DataBindings.Add("Text", dtgvDSHH.DataSource, "ThanhTien");
        }
        private void LoadcmbKhachHang()
        {
            KhachHangCtr khctr = new KhachHangCtr();
            cmbKhachHang.DataSource = khctr.GetData();
            cmbKhachHang.DisplayMember = "TenKH";
            cmbKhachHang.ValueMember = "MaKH";
            cmbKhachHang.SelectedIndex = 0;
        }
  

        private void LoadcmbHH()
        {
            HangHoaCtr hhctr = new HangHoaCtr();
            cmbHH.DataSource = hhctr.GetData();
            cmbHH.DisplayMember = "TenMH";
            cmbHH.ValueMember = "MaMH";
          

        }
        private void addData(HoaDonobj hdObj)
        {
            hdObj.NguoiLap = bientoancuc.bienxy;
            hdObj.KhachHang = cmbKhachHang.SelectedValue.ToString();
        }
        private void addData2(ChiTietHDobj cthdobj)
        {
            cthdobj.MaHangHoa = cmbHH.SelectedValue.ToString();
            cthdobj.MaHoaDon = txtMa.Text.Trim();
            cthdobj.SoLuong= int.Parse(txtSL.Text);
                    }
      
        private void HoaDon_Load(object sender, EventArgs e)
        {
            Dis_Enl(false);
            DataTable dt = new System.Data.DataTable();
            dt = hdCtr.GetData();
            dtgvDSHD.DataSource = dt;
            bingding();
            txtNgayLap.Enabled = false;
            LoadcmbHH();
            LoadcmbKhachHang();
          
        }

     
        private bool kiemtraSL(string mahh, int sl)
        {
            DataTable dt = new DataTable();
            dt = hhctr.GetData("Where MaMH = '" + cmbHH.SelectedValue.ToString() + "' and SoLuong>= " + sl);
            if (dt.Rows.Count > 0)
                return true;
            return false;
        }
        private bool  kiemtratrung(string mahh)
        {
            for (int i = 0; i < dtDSCT.Rows.Count; i++)
                if (dtDSCT.Rows[i][1].ToString() == mahh)
                    return true;
            return false;
        }
       

        private void nvcbb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void load()
        {
            DataTable dt = new System.Data.DataTable();
            dt = ctCtr.GetData(txtMa.Text.Trim());
            dtgvDSHH.DataSource = dt;
            bing();
        }
        private void txtMa_TextChanged(object sender, EventArgs e)
        {
                DataTable dt = new System.Data.DataTable();
                dt = ctCtr.GetData(txtMa.Text.Trim());
                dtgvDSHH.DataSource = dt;
                bing();


        }

        private void cmbHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = hhctr.GetData("Where MaMH = '" + cmbHH.SelectedValue.ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                double gia = double.Parse(dt.Rows[0][3].ToString());
                txtDonGia.Text = (gia * 1).ToString();
                lbThanhTien.Text = (double.Parse(txtDonGia.Text.ToString())*int.Parse(txtSL.Text.ToString())).ToString();
               
            }

        }

      

        private void btnDel_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (hdCtr.DelData(txtMa.Text.Trim()))
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Xóa không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            HoaDon_Load(sender, e);
        }

        private void btnBot_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa sản phẩm này khỏi hóa đơn ?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (ctCtr.DelData(txtMa.Text.Trim(),cmbHH.SelectedValue.ToString()))
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Xóa không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            load();
            HoaDon_Load(sender, e);
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            lbThanhTien.Text = (double.Parse(txtDonGia.Text.ToString()) * int.Parse(txtSL.Text.ToString())).ToString();
        }

        private void cmbHH_TextChanged(object sender, EventArgs e)
        {
     
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Dis_Enl(true);
            clearData();
            LoadcmbHH();
            LoadcmbKhachHang();
            dtDSCT.Rows.Clear();
            dtDSCT.Columns.Clear();
            dtDSCT.Columns.Add("MaHD");
            dtDSCT.Columns.Add("MaMH");
            dtDSCT.Columns.Add("TênMH");
            dtDSCT.Columns.Add("So luong");
            dtDSCT.Columns.Add("Đơn Giá");
            dtDSCT.Columns.Add("Thành Tiền");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!kiemtratrung(cmbHH.SelectedValue.ToString()))
            {
                if (kiemtraSL(cmbHH.SelectedValue.ToString(), int.Parse(txtSL.Text)))
                {
                    DataRow dr = dtDSCT.NewRow();
                    dr[0] = txtMa.Text.Trim();
                    dr[1] = cmbHH.SelectedValue.ToString();
                    dr[2] = cmbHH.Text;
                    dr[3] = txtSL.Text;
                    dr[4] = txtDonGia.Text;
                    dr[5] = (double.Parse(txtDonGia.Text) * int.Parse(txtSL.Text)).ToString();
                    dtDSCT.Rows.Add(dr);
                }
                else
                {
                    MessageBox.Show("Không đủ số lượng", "Lỗi");
                }
            }
          else
            {
                MessageBox.Show("Trùng", "Lỗi");
            }
            dtgvDSHH.DataSource = dtDSCT;
        }

        private void btnBot_Click_1(object sender, EventArgs e)
        {
            if (vitriclick < dtDSCT.Rows.Count)
            {
                dtDSCT.Rows.RemoveAt(vitriclick);
                dtgvDSHH.DataSource = dtDSCT;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn hủy thao tác đang làm?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                HoaDon_Load(sender, e);
            else
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

           HoaDonobj hdObj = new HoaDonobj();
            addData(hdObj);
            if (hdCtr.AddData(hdObj))
            {
                if (ctCtr.AddData(dtDSCT))
                    MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Thêm chi tiết không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Thêm hóa đơn không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            HoaDon_Load(sender, e);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            FormInHoaDon frm = new FormInHoaDon(txtMa.Text);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
