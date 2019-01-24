using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using QLBH.Object;
namespace QLBH.Model
{
    class HoaDonModel
    {
        ConnectToSQL con = new ConnectToSQL();
        SqlCommand cmd = new SqlCommand();

        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            cmd.CommandText = @"select HoaDon.MaHD , HoaDon.NgayDH , NhanVien.TenNV , KhachHang.TenKH , HoaDon.TongTien  from HoaDon, KhachHang , NhanVien  where KhachHang.MaKH = HoaDon.MaKH and NhanVien.MaNV = HoaDon.MaNV";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con.Connection;
            try
            {
                con.OpenConn();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                string mex = ex.Message;
                cmd.Dispose();
                con.CloseConn();
            }
            return dt;
        }

        public bool AddData(HoaDonobj hdObj)
        {
            cmd.CommandText = "insert into HoaDon(MaNV,MaKH) values ('" + hdObj.NguoiLap + "','" + hdObj.KhachHang + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con.Connection;
            try
            {
                con.OpenConn();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                string mex = ex.Message;
                cmd.Dispose();
                con.CloseConn();
            }
            return false;
        }

        public bool DelData(string ma)
        {
            cmd.CommandText = "Delete HoaDon Where MaHD = '" + ma + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con.Connection;
            try
            {
                con.OpenConn();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                string mex = ex.Message;
                cmd.Dispose();
                con.CloseConn();
            }
            return false;
        }
        public DataTable GetID()
        {
            DataTable dt = new DataTable();
            cmd.CommandText = "SELECT IDENT_CURRENT('HoaDon')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con.Connection;
            try
            {
                con.OpenConn();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                string mex = ex.Message;
                cmd.Dispose();
                con.CloseConn();
            }
            return dt;

        }
        public DataSet XuatHoaDon(string SoHD)
        {

            DataSet dtset = new DataSet();
            cmd.CommandText = @"Select PN.MaHD,CT.MaMH,CT.SoLuong,MH.DonGia,CT.ThanhTien,PN.NgayDH,PN.TongTien,MH.TenMH,NV.TenNV,NCC.TenKH
                          From ((HoaDon PN join ChiTietHoaDon CT on PN.MaHD=CT.MaHD) join MatHang MH on CT.MaMH=MH.MaMH) 
                          join KhachHang NCC on PN.MaKH=NCC.MaKH join NhanVien NV on PN.MaNV=NV.MaNV Where PN.MaHD='" + SoHD + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con.Connection;
            try
            {
                con.OpenConn();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtset, "dtHoaDon");
            }
            catch (Exception ex)
            {
                string mex = ex.Message;
                cmd.Dispose();
                con.CloseConn();
            }
            return dtset;
        }
    }
}
