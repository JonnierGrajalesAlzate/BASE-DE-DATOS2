using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROCESO_CRUD
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }
         
        private static string cadena = @"Data Source=.\mydatabse.db;Version=3;";

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string lcUsuario = txtUsuario.Text;
            int lnClave = int.Parse(txtClave.Text);  
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT COUNT(1) FROM Persona WHERE nombre=@nombre AND clave=@clave";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombre", lcUsuario);
                    cmd.Parameters.AddWithValue("@clave", lnClave);

                    int lnCelda= Convert.ToInt32(cmd.ExecuteScalar());

                    if (lnCelda == 1)
                    {
                        frmAplicacion frmApp = new frmAplicacion();
                        frmApp.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("La cuenta no existe.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al conectarse a la base de datos: {ex.Message}");
                }
            }
        }

        private void txtClaveKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            frm_BaseDatos frmBD = new frm_BaseDatos();
            frmBD.Show();
        }
    }
}
