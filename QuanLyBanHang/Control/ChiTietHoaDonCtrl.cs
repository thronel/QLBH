using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLyBanHang.Model;
using QuanLyBanHang.Object;

namespace QuanLyBanHang.Control
{
    class ChiTietHoaDonCtrl
    {
        ChiTietHoaDonMod ctMod = new ChiTietHoaDonMod();
        public DataTable GetData(string ma)
        {
            return ctMod.GetData(ma);
        }
        public bool AddData(DataTable dt)
        {
            return ctMod.AddData(dt);
        }
        public bool DelData(string ma)
        {
            return ctMod.DelData(ma);
        }
    }
}
