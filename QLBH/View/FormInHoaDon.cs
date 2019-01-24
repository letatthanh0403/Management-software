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
    public partial class FormInHoaDon : Form
    {
        HoaDonCtrl hd = new HoaDonCtrl();
        String mahd;
        public FormInHoaDon()
        {
            InitializeComponent();
        }
        public FormInHoaDon(String MAHD)
        {
            InitializeComponent();
            mahd = MAHD;
        }

        private void FormInHoaDon_Load(object sender, EventArgs e)
        {
            InHoaDon report = new InHoaDon();
            report.SetDataSource(hd.XuatHD(mahd));
            crystalReportViewer1.ReportSource = report;
        }
    }
}
