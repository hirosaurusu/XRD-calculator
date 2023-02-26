using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace XRD_ver2
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// datatableのdatasourceとなるdatatable
        /// </summary>
        private DataTable dt = new DataTable();

        private const string COL_NAME = "Material";
        private const string COL_UPDATE = "Update";
        //private static string strCrystal;



        public FormMain()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;

            

            //dgvMaterialにdtを割り当てる
            dgvMaterial.DataSource = dt;


            #region VBの処理を地道にC#に書き換える
            System.IO.DirectoryInfo dirInfo;
            System.IO.FileInfo[] fInfolist = null;

            dt.Columns.Add(COL_NAME);
            //dt.Columns.Add(COL_UPDATE);

            string strCurrentDir = System.IO.Directory.GetCurrentDirectory();
            string matPath = System.IO.Path.Combine(strCurrentDir, "XRD_ParameterData");

            try
            {
                dirInfo = new System.IO.DirectoryInfo(matPath);
                fInfolist = dirInfo.GetFiles();
            }
            catch(System.Exception ex)
            {

            }

            foreach(System.IO.FileInfo fInfo in fInfolist)
            {
                DataRow fiItem = dt.NewRow();
                fiItem[COL_NAME] = fInfo.Name;
                //fiItem[COL_UPDATE] = fInfo.LastWriteTime;
                dt.Rows.Add(fiItem);
            }
            #endregion

        }

       
        /// <summary>
        /// DataGridViewの行を選択したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMaterial_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvMaterial.SelectedRows.Count < 1)
            {
                return;
            }
            int aSelectedIndex = dgvMaterial.Rows[0].Index;
            //string aFname = dgvMaterial[COL_NAME, aSelectedIndex].Value.ToString();
            string aFname = dgvMaterial.CurrentRow.Cells[0].Value.ToString();
            ReadLCandShow(aFname);
        }

        /// <summary>
        /// 指定された材料の読み込みと格子定数の表示
        /// </summary>
        /// <param name="fname"></param>
        private void ReadLCandShow(string fname)
        {
            string stCurrentDir = System.IO.Directory.GetCurrentDirectory();
            string matPath = System.IO.Path.Combine(stCurrentDir, "XRD_ParameterData", fname);

            String[] matFile = System.IO.File.ReadAllLines(matPath);

            txtCrystalStructure.Text = matFile[0];


            string strCrystal = matFile[0];
            //strCrystal = matFile[0];
            switch (strCrystal)
            {
                case "3":
                    txtCrystalStructure.Text = "Cubic";
                    txtLCa.Text = matFile[1];
                    txtLCb.Text = matFile[1];
                    txtLCc.Text = matFile[1];
                    break;
                case "5":
                    txtCrystalStructure.Text = "Hexagonal";
                    txtLCa.Text = matFile[1];
                    txtLCb.Text = matFile[1];
                    txtLCc.Text = matFile[2];
                    break;
                case "6":
                    txtCrystalStructure.Text = "Tetragonal";
                    txtLCa.Text = matFile[1];
                    txtLCb.Text = matFile[1];
                    txtLCc.Text = matFile[2];
                    break;
                default:
                    txtCrystalStructure.Text = "Select material";
                    break;
            }


            /*
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < matFile.Length; i++)
            {
                sb.AppendLine(matFile[i]);
            }
            MessageBox.Show(sb.ToString());
            */
        }

        private void btnXRD_Click(object sender, EventArgs e)
        {
            //左列
            double h, k, l;
            double a;
            double b;
            double c;
            double lambda;
            string strCrystalType = txtCrystalStructure.Text;
            int CryType;

            //（上手いやり方ではない）
            switch(strCrystalType)
            {
                case "Cubic":
                    CryType = 3;
                    break;
                case "Hexagonal":
                    CryType = 5;
                    break;
                case "Tetragonal":
                    CryType = 6;
                    break;
                default:
                    CryType = 3;
                    break;
            }

            

            //力技だけど、とりあえず動くのでこれでやってる
            if ( double.TryParse(txtH1.Text, out h) && double.TryParse(txtK1.Text, out k) && double.TryParse(txtL1.Text, out l) 
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c) 
                && double.TryParse(txtWL1.Text, out lambda) )
            {
                txtP1.Text = CalcEq.calcXRD(CryType,h, k, l, a,b,c, lambda).ToString("F4");
            }
            else
            {
                txtP1.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH2.Text, out h) && double.TryParse(txtK2.Text, out k) && double.TryParse(txtL2.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP2.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP2.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH3.Text, out h) && double.TryParse(txtK3.Text, out k) && double.TryParse(txtL3.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP3.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP3.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH4.Text, out h) && double.TryParse(txtK4.Text, out k) && double.TryParse(txtL4.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP4.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP4.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH5.Text, out h) && double.TryParse(txtK5.Text, out k) && double.TryParse(txtL5.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP5.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP5.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH6.Text, out h) && double.TryParse(txtK6.Text, out k) && double.TryParse(txtL6.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP6.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP6.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH7.Text, out h) && double.TryParse(txtK7.Text, out k) && double.TryParse(txtL7.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP7.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP7.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH8.Text, out h) && double.TryParse(txtK8.Text, out k) && double.TryParse(txtL8.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP8.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP8.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH9.Text, out h) && double.TryParse(txtK9.Text, out k) && double.TryParse(txtL9.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP9.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP9.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH10.Text, out h) && double.TryParse(txtK10.Text, out k) && double.TryParse(txtL10.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL1.Text, out lambda))
            {
                txtP10.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP10.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH11.Text, out h) && double.TryParse(txtK11.Text, out k) && double.TryParse(txtL11.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP11.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP11.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH12.Text, out h) && double.TryParse(txtK12.Text, out k) && double.TryParse(txtL12.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP12.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP12.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH13.Text, out h) && double.TryParse(txtK13.Text, out k) && double.TryParse(txtL13.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP13.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP13.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH14.Text, out h) && double.TryParse(txtK14.Text, out k) && double.TryParse(txtL14.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP14.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP14.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH15.Text, out h) && double.TryParse(txtK15.Text, out k) && double.TryParse(txtL15.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP15.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP15.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH16.Text, out h) && double.TryParse(txtK16.Text, out k) && double.TryParse(txtL16.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP16.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP16.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH17.Text, out h) && double.TryParse(txtK17.Text, out k) && double.TryParse(txtL17.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP17.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP17.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH18.Text, out h) && double.TryParse(txtK18.Text, out k) && double.TryParse(txtL18.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP18.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP18.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH19.Text, out h) && double.TryParse(txtK19.Text, out k) && double.TryParse(txtL19.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP19.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP19.Text = "Error";
            }
            //力技だけど、とりあえず動くのでこれでやってる
            if (double.TryParse(txtH20.Text, out h) && double.TryParse(txtK20.Text, out k) && double.TryParse(txtL20.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c)
                && double.TryParse(txtWL2.Text, out lambda))
            {
                txtP20.Text = CalcEq.calcXRD(CryType, h, k, l, a, b, c, lambda).ToString("F4");
            }
            else
            {
                txtP20.Text = "Error";
            }

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                txtWL1.Text = "1.541838";
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                txtWL1.Text = "1.392218";
            }
            else if(comboBox1.SelectedIndex == 2)
            {
                txtWL1.Text = "1.540562";
            }
            else if(comboBox1.SelectedIndex == 3)
            {
                txtWL1.Text = "1.54439";
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                txtWL1.Text = "1.79026";
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                txtWL1.Text = "1.62079";
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                txtWL1.Text = "1.78896";
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                txtWL1.Text = "1.79285";
            }
            else
            {
                txtWL1.Text = "1.541838";
            }
            btnXRD.PerformClick();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                txtWL2.Text = "1.541838";
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                txtWL2.Text = "1.392218";
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                txtWL2.Text = "1.540562";
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                txtWL2.Text = "1.54439";
            }
            else if (comboBox2.SelectedIndex == 4)
            {
                txtWL2.Text = "1.79026";
            }
            else if (comboBox2.SelectedIndex == 5)
            {
                txtWL2.Text = "1.62079";
            }
            else if (comboBox2.SelectedIndex == 6)
            {
                txtWL2.Text = "1.78896";
            }
            else if (comboBox2.SelectedIndex == 7)
            {
                txtWL2.Text = "1.79285";
            }
            else
            {
                txtWL2.Text = "1.541838";
            }
            btnXRD.PerformClick();
        }

        private void btnPlaneAngle_Click(object sender, EventArgs e)
        {
            double h1, k1, l1, h2, k2, l2;
            double a, b, c;

            string strCrystalType = txtCrystalStructure.Text;
            int CryType;

            //（上手いやり方ではない）
            switch (strCrystalType)
            {
                case "Cubic":
                    CryType = 3;
                    break;
                case "Hexagonal":
                    CryType = 5;
                    break;
                case "Tetragonal":
                    CryType= 6;
                    break;
                default:
                    CryType = 3;
                    break;
            }

            if (double.TryParse(txtP1H.Text, out h1) && double.TryParse(txtP1K.Text, out k1) && double.TryParse(txtP1L.Text, out l1)
                && double.TryParse(txtP2H.Text, out h2) && double.TryParse(txtP2K.Text, out k2) && double.TryParse(txtP2L.Text, out l2)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c))
            {
                txtPlaneAangle.Text = CalcEq.calcAngle(CryType, h1, k1, l1, h2, k2, l2, a, b, c).ToString("F4");
            }
            else
            {
                txtPlaneAangle.Text = "Error";
            }
        }

        private void btnPlaneDistance_Click(object sender, EventArgs e)
        {
            double h, k, l;
            double a, b, c;

            string strCrystalType = txtCrystalStructure.Text;
            int CryType;

            //（上手いやり方ではない）
            switch (strCrystalType)
            {
                case "Cubic":
                    CryType = 3;
                    break;
                case "Hexagonal":
                    CryType = 5;
                    break;
                case "Tetragonal":
                    CryType = 6;
                    break;
                default: 
                    CryType = 3; //とりあえず設定してるけど何の意味もない
                    break;
            }

            if (double.TryParse(txtP1H.Text, out h) && double.TryParse(txtP1K.Text, out k) && double.TryParse(txtP1L.Text, out l)
                && double.TryParse(txtLCa.Text, out a) && double.TryParse(txtLCb.Text, out b) && double.TryParse(txtLCc.Text, out c))
            {
                txtPlaneDistance.Text = CalcEq.calcDistance(CryType, h, k, l, a, b, c).ToString("F4");
            }
            else
            {
                txtPlaneDistance.Text = "Error";
            }
        }

        private void txtH1_Validated(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        

        private void txtH1_TextChanged_1(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK1_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL1_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH2_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK2_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL2_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH3_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK3_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL3_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH4_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK4_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL4_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH5_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK5_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL5_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH6_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK6_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL6_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH7_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK7_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL7_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH8_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK8_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL8_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH9_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK9_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL9_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH10_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK10_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL10_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH11_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK11_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL11_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH12_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK12_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL12_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH13_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK13_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL13_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH14_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK14_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL14_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH15_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK15_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL15_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH16_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK16_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL16_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH17_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK17_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL17_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH18_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK18_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL18_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH19_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK19_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL19_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtH20_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtK20_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtL20_TextChanged(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void dgvMaterial_DoubleClick(object sender, EventArgs e)
        {
            btnXRD.PerformClick();
        }

        private void txtLCa_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnXRD.PerformClick();
            }
        }

        private void txtLCb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnXRD.PerformClick();
            }
        }

        private void txtLCc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnXRD.PerformClick();
            }
        }
    }
}
