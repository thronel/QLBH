﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.Object;
using QuanLyBanHang.Model;
using QuanLyBanHang.Control;

namespace QuanLyBanHang.View
{
    public partial class HoaDonForm : Form
    {
        public HoaDonForm()
        {
            InitializeComponent();
        }
        HoaDonCtrl hdCtr = new HoaDonCtrl();
        ChiTietHoaDonCtrl ctCtr = new ChiTietHoaDonCtrl();
        HangHoaCtrl hhctr = new HangHoaCtrl();
        DataTable dtDSCT = new System.Data.DataTable();
        int vitriclick = 0;

        private void HoaDonForm_Load(object sender, EventArgs e)
        {
            Dis_Enl(false);
            DataTable dt = new System.Data.DataTable();
            dt = hdCtr.GetData();
            dtgvDSHD.DataSource = dt;
            bingding();
            txtNgayLap.Enabled = false;
        }

        private void bingding()
        {
            txtMa.DataBindings.Clear();
            txtMa.DataBindings.Add("Text", dtgvDSHD.DataSource, "MaHoaDon");
            txtNgayLap.DataBindings.Clear();
            txtNgayLap.DataBindings.Add("Text", dtgvDSHD.DataSource, "NgayLap");
            txtNhanVien.DataBindings.Clear();
            txtNhanVien.DataBindings.Add("Text", dtgvDSHD.DataSource, "TenNhanVien");
            cmbKhachHang.DataBindings.Clear();
            cmbKhachHang.DataBindings.Add("Text", dtgvDSHD.DataSource, "TenKH");
        }

        private void Dis_Enl(bool e)
        {
            txtMa.Enabled = e;
            txtNhanVien.Enabled = e;
            cmbKhachHang.Enabled = e;
            btnThemHD.Enabled = !e;
            btnDel.Enabled = !e;
            btnPrint.Enabled = !e;
            btnSave.Enabled = e;
            btnCancel.Enabled = e;
            btncham.Enabled = e;
            btnThem.Enabled = e;
            btnBot.Enabled = e;
            cmbHH.Enabled = e;
            txtSL.Enabled = e;
        }

        private void LoadcmbKhachHang()
        {
            KhachHangCtrl khctr = new KhachHangCtrl();
            cmbKhachHang.DataSource = khctr.GetData();
            cmbKhachHang.DisplayMember = "TenKH";
            cmbKhachHang.ValueMember = "MaKH";
            cmbKhachHang.SelectedIndex = 0;
        }

        private void LoadcmbHH()
        {
            HangHoaCtrl hhctr = new HangHoaCtrl();
            cmbHH.DataSource = hhctr.GetData();
            cmbHH.DisplayMember = "TenHang";
            cmbHH.ValueMember = "MaHang";

        }

        private void clearData()
        {
            txtMa.Text = "";
            txtNhanVien.Text = "";
            txtNgayLap.Text = DateTime.Now.Date.ToShortDateString();
            LoadcmbKhachHang();
        }

        private void addData(HoaDonObj hdObj)
        {
            hdObj.MaHoaDon = txtMa.Text.Trim();
            hdObj.NgayLap = txtNgayLap.Text.Trim();
            hdObj.NguoiLap = txtNhanVien.Text.Trim();
            hdObj.KhachHang = cmbKhachHang.SelectedValue.ToString();
        }

        private bool checktrung(string mahh)
        {
            for (int i = 0; i < dtDSCT.Rows.Count; i++)
                if (dtDSCT.Rows[i][1].ToString() == mahh)
                    return true;
            return false;
        }

        private void capnhatSL(string mahh, int SL)
        {
            for (int i = 0; i < dtDSCT.Rows.Count; i++)
                if (dtDSCT.Rows[i][1].ToString() == mahh)
                {
                    int soluong = int.Parse(dtDSCT.Rows[i][3].ToString()) + SL;
                    dtDSCT.Rows[i][3] = soluong;
                    double dongia = double.Parse(dtDSCT.Rows[i][2].ToString());
                    dtDSCT.Rows[i][4] = (dongia * soluong).ToString();
                    break;
                }
        }

        private bool kiemtraSL(string mahh, int sl)
        {
            DataTable dt = new DataTable();
            dt = hhctr.GetData("Where MaHang = '" + cmbHH.SelectedValue.ToString() + "' and SoLuong>= " + sl);
            if (dt.Rows.Count > 0)
                return true;
            return false;
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new System.Data.DataTable();
                dt = ctCtr.GetData(txtMa.Text.Trim());
                dtgvDSHH.DataSource = dt;

            }
            catch
            {
                dtgvDSHH.DataSource = null;
            }
            //bingding1();
        }

        private void dtgvDSHH_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            Dis_Enl(true);
            clearData();
            LoadcmbHH();
            LoadcmbKhachHang();

            dtDSCT.Rows.Clear();
            dtDSCT.Columns.Add("MaHoaDon");
            dtDSCT.Columns.Add("HangHoa");
            dtDSCT.Columns.Add("DonGia");
            dtDSCT.Columns.Add("SoLuong");
            dtDSCT.Columns.Add("ThanhTien");
        }

        private void btncham_Click(object sender, EventArgs e)
        {
            txtNgayLap.Enabled = true;
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
            HoaDonForm_Load(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HoaDonObj hdObj = new HoaDonObj();
            addData(hdObj);
            if (hdCtr.AddData(hdObj))
            {
                if (ctCtr.AddData(dtDSCT) && hhctr.UpdSL(dtDSCT))
                    MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Thêm chi tiết không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Thêm hóa đơn không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            HoaDonForm_Load(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn hủy thao tác đang làm?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                HoaDonForm_Load(sender, e);
            else
                return;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMa.Text))
            {
                if (kiemtraSL(cmbHH.SelectedValue.ToString(), int.Parse(txtSL.Text.Trim())))
                {
                    if (!checktrung(cmbHH.SelectedValue.ToString()))
                    {
                        DataRow dr = dtDSCT.NewRow();
                        dr[0] = txtMa.Text.Trim();
                        dr[1] = cmbHH.SelectedValue.ToString();
                        dr[2] = txtDonGia.Text;
                        dr[3] = txtSL.Text;
                        dr[4] = (double.Parse(txtDonGia.Text) * int.Parse(txtSL.Text)).ToString();
                        dtDSCT.Rows.Add(dr);
                    }
                    else
                    {
                        capnhatSL(cmbHH.SelectedValue.ToString(), int.Parse(txtSL.Text));
                    }
                    dtgvDSHH.DataSource = dtDSCT;
                }
                else
                {
                    MessageBox.Show("Số lượng không dủ", "Lỗi");
                    txtSL.Focus();
                }
            }
            else
            {
                MessageBox.Show("Mã hóa đơn không được trống", "Lỗi");
                txtMa.Focus();
            }
        }

        private void btnBot_Click(object sender, EventArgs e)
        {
            if (vitriclick < dtDSCT.Rows.Count)
            {
                dtDSCT.Rows.RemoveAt(vitriclick);
                dtgvDSHH.DataSource = dtDSCT;
            }
        }

        private void cmbHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            cmbHH.Text = "Vui lòng chọn sản phẩm";
            dt = hhctr.GetData("Where MaHang = '" + cmbHH.SelectedValue.ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                double gia = double.Parse(dt.Rows[0][2].ToString());

                txtDonGia.Text = (gia * 1.1).ToString();

                lbThanhTien.Text = (double.Parse(txtDonGia.Text) * int.Parse(txtSL.Text)).ToString();
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGia.Text != "" && txtDonGia.Text != null)
            lbThanhTien.Text = (double.Parse(txtDonGia.Text) * int.Parse(txtSL.Text)).ToString();
        }

        private void dtgvDSHH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vitriclick = e.RowIndex;
        }
    }
}
