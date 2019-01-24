using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBH.Control;
namespace QLBH.View
{
    public partial class FormInPhieuNhap : Form
    {
        PhieuNhapCtr pn = new PhieuNhapCtr();
        string sopn;
        public FormInPhieuNhap()
        {
            InitializeComponent();
        }
        public FormInPhieuNhap(string SoPN)
        {
            InitializeComponent();
            sopn = SoPN;
        }

        private void FormInPhieuNhap_Load(object sender, EventArgs e)
        {
           
           InPhieuNhap report = new InPhieuNhap();
            report.SetDataSource(pn.XuatPhieuNhapSach(sopn));
            crystalReportViewer1.ReportSource = report;
        }
    }
}
