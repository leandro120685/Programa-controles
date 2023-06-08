using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QRCoder;

namespace Programa_controles
{
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection("server=PUESTO01; database=Empresa;integrated security=true");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            llenarTabla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ModificarRegistro();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MostrarDatosSeleccionados();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GenerarQRCode();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GuardarQRCode();
        }

        private void llenarTabla()
        {
            string consulta = "select * from control02";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void limpiarCampos()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox5.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox2.Focus();
        }

        private void AgregarRegistro()
        {
            using (SqlConnection conexion = new SqlConnection("server=PUESTO01; database=Empresa;integrated security=true"))
            {
                conexion.Open();
                string consulta = "insert into control02 (Controlo, Fecha, Estado, Tdaql, Oc, Tdlote, Descripcion, Cdmaterial, Observaciones) values (@Controlo, @Fecha, @Estado, @Tdaql, @Oc, @Tdlote, @Descripcion, @Cdmaterial, @Observaciones)";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Controlo", textBox2.Text);
                comando.Parameters.AddWithValue("@Fecha", textBox3.Text);
                comando.Parameters.AddWithValue("@Estado", textBox4.Text);
                comando.Parameters.AddWithValue("@Tdaql", textBox5.Text);
                comando.Parameters.AddWithValue("@Oc", textBox6.Text);
                comando.Parameters.AddWithValue("@Tdlote", textBox7.Text);
                comando.Parameters.AddWithValue("@Descripcion", textBox8.Text);
                comando.Parameters.AddWithValue("@Cdmaterial", textBox9.Text);
                comando.Parameters.AddWithValue("@Observaciones", textBox10.Text);

                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Agregado");
                llenarTabla();
                limpiarCampos();
            }
        }

        private void EliminarRegistro()
        {
            using (SqlConnection conexion = new SqlConnection("server=PUESTO01; database=Empresa;integrated security=true"))
            {
                conexion.Open();
                string consulta = "delete from control02 where Codigo = @Codigo";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Codigo", textBox1.Text);

                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Eliminado");
                llenarTabla();
                limpiarCampos();
            }
        }

        private void ModificarRegistro()
        {
            using (SqlConnection conexion = new SqlConnection("server=PUESTO01; database=Empresa;integrated security=true"))
            {
                conexion.Open();
                string consulta = "update control02 set Controlo = @Controlo, Fecha = @Fecha, Estado = @Estado, Tdaql = @Tdaql, Oc = @Oc, Tdlote = @Tdlote, Descripcion = @Descripcion, Cdmaterial = @Cdmaterial, Observaciones = @Observaciones where Codigo = @Codigo";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Controlo", textBox2.Text);
                comando.Parameters.AddWithValue("@Fecha", textBox3.Text);
                comando.Parameters.AddWithValue("@Estado", textBox4.Text);
                comando.Parameters.AddWithValue("@Tdaql", textBox5.Text);
                comando.Parameters.AddWithValue("@Oc", textBox6.Text);
                comando.Parameters.AddWithValue("@Tdlote", textBox7.Text);
                comando.Parameters.AddWithValue("@Descripcion", textBox8.Text);
                comando.Parameters.AddWithValue("@Cdmaterial", textBox9.Text);
                comando.Parameters.AddWithValue("@Observaciones", textBox10.Text);
                comando.Parameters.AddWithValue("@Codigo", textBox1.Text);

                int cant = comando.ExecuteNonQuery();
                if (cant > 0)
                {
                    MessageBox.Show("Registro Modificado");
                }
                llenarTabla();
                limpiarCampos();
            }
        }

        private void MostrarDatosSeleccionados()
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                textBox1.Text = dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.Rows[selectedRowIndex].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.Rows[selectedRowIndex].Cells[2].Value.ToString();
                textBox8.Text = dataGridView1.Rows[selectedRowIndex].Cells[3].Value.ToString();
                textBox2.Text = dataGridView1.Rows[selectedRowIndex].Cells[4].Value.ToString();
                textBox9.Text = dataGridView1.Rows[selectedRowIndex].Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.Rows[selectedRowIndex].Cells[6].Value.ToString();
                textBox5.Text = dataGridView1.Rows[selectedRowIndex].Cells[7].Value.ToString();
                textBox4.Text = dataGridView1.Rows[selectedRowIndex].Cells[8].Value.ToString();
                textBox10.Text = dataGridView1.Rows[selectedRowIndex].Cells[9].Value.ToString();
            }
        }

        private void GenerarQRCode()
        {
            string contenido = string.Format("Codigo: {1}{0}Controlo: {2}{0}Fecha: {3}{0}Estado: {4}{0}Tdaql: {5}{0}Oc: {6}{0}Tdlote: {7}{0}Descripcion: {8}{0}Cdmaterial: {9}{0}Observaciones: {10}{0}", Environment.NewLine, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text);

            QRCodeGenerator qrGenerador = new QRCodeGenerator();
            QRCodeData qrDatos = qrGenerador.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.H);
            QRCode qrCodigo = new QRCode(qrDatos);

            Bitmap qrImagen = qrCodigo.GetGraphic(3, Color.Black, Color.White, true);
            pbFoto.Image = qrImagen;
        }

        private void GuardarQRCode()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string qrPath = saveFileDialog.FileName;
                Bitmap bitmap = new Bitmap(pbFoto.Image);
                bitmap.Save(qrPath);
                MessageBox.Show("QR Code guardado exitosamente.");


            }

        }
    }
}
