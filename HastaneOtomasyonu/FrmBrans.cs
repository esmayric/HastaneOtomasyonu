using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace HastaneOtomasyonu
{
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        AccessBaglantisi bgl = new AccessBaglantisi();
        public string TC;
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM TblBrans", bgl.baglanti());
            da.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            labelTC.Text = TC;
        }
        private void btnBransEkle_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("insert into TblBrans (Brans) values (@b1)", bgl.baglanti());
            cmd.Parameters.AddWithValue("@b1", textBoxBrans.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Brans Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = e.RowIndex;
            if (secilen >= 0 && secilen < dataGridView1.Rows.Count)
            {
                txtbransID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                textBoxBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            }
        }
        private void btnBransSil_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd1 = new OleDbCommand("DELETE FROM TblBrans WHERE ID=@b1", bgl.baglanti());
            cmd1.Parameters.AddWithValue("@b1", txtbransID.Text);
            cmd1.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Branş Silindi"); 
        }
        private void btnBransGuncelle_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("UPDATE TblBrans SET Brans = @p1 WHERE ID = @p2", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", textBoxBrans.Text);
            cmd.Parameters.AddWithValue("@p2", txtbransID.Text);

            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Branş Güncellendi");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FrmSekreterDetay detay = new FrmSekreterDetay();
            detay.tc = labelTC.Text;
            detay.Show();
            this.Hide();
        }
    }
}
