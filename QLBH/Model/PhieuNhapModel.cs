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
    class PhieuNhapModel
    {
        ConnectToSQL con = new ConnectToSQL();
        SqlCommand cmd = new SqlCommand();

        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            cmd.CommandText = @"select pn.MaPN, pn.NgayNhap, nv.TenNV, ncc.TenNCC,pn.TongTien from PhieuNhapHang pn, NhanVien nv, NhaCungCap ncc where ncc.MaNCC = pn.MaNCC and nv.MaNV = pn.MaNV";
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
        public  bool UpdateTT(string SoPN, long TongTien)
        {       
            cmd.CommandText = "Update  PhieuNhapHang set TongTien='" + TongTien.ToString() + "' Where MAPN='" + SoPN + "'";
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
            cmd.CommandText = "SELECT IDENT_CURRENT('PhieuNhapHang')";
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
        public bool AddData(PhieuNhapobj pnObj)
        {
            cmd.CommandText = "insert into PhieuNhapHang(MaNV,MaNCC,TongTien) values ('" + pnObj.Nguoilap + "','" + pnObj.Ncc + "','" + pnObj.Tongtien + "')";
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
            cmd.CommandText = "Delete tb_HoaDon Where MaHD = '" + ma + "'";
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
        public  DataSet XuatPhieuNhapSach(string SoPN)
        {
         
            DataSet dtset = new DataSet();
            cmd.CommandText = @"Select PN.MaPN,CT.MaMH,CT.SoLuong,CT.DonGia,CT.ThanhTien,PN.NgayNhap,PN.TongTien,MH.TenMH,NV.TenNV,NCC.TenNCC
                          From ((PhieuNhapHang PN join ChiTietPhieuNhap CT on PN.MaPN=CT.MaPN) join MatHang MH on CT.MaMH=MH.MaMH) 
                          join NhaCungCap NCC on PN.MaNCC=NCC.MaNCC join NhanVien NV on PN.MaNV=NV.MaNV Where PN.MaPN='" + SoPN + "'" ;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con.Connection;
            try
            {
                con.OpenConn();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtset, "dtPhieuNhap");
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
