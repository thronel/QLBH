﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Object
{
    class KhachHangObj
    {
        string ma, ten, gioitinh, diachi, sdt, namsinh;

        public string DienThoai
        {
            get { return sdt; }
            set { sdt = value; }
        }

        public string DiaChi
        {
            get { return diachi; }
            set { diachi = value; }
        }

        public string GioiTinh
        {
            get { return gioitinh; }
            set { gioitinh = value; }
        }

        public string TenKhachHang
        {
            get { return ten; }
            set { ten = value; }
        }

        public string MaKhachHang
        {
            get { return ma; }
            set { ma = value; }
        }

        public string NamSinh
        {
            get { return namsinh; }
            set { namsinh = value; }
        }

        public KhachHangObj() { }
        public KhachHangObj(string ma, string ten, string gioitinh, string namsinh, string diachi, string sdt)
        {
            this.ma = ma;
            this.ten = ten;
            this.gioitinh = gioitinh;
            this.namsinh = namsinh;
            this.diachi = diachi;
            this.sdt = sdt;
        }
    }
}
