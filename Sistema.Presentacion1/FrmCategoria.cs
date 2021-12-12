using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion1
{
    public partial class FrmCategoria : Form
    {
        private string NombreAnterior;

        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NCategoria.Listar();
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total de registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NCategoria.Buscar(TxtBuscar.Text);
                this.Formato();
                LblTotal.Text = "Total de registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[1].Visible = false;
            DgvListado.Columns[2].Width = 150;
            DgvListado.Columns[3].Width = 400;
            DgvListado.Columns[3].HeaderText = "Descripción";
            DgvListado.Columns[4].Width = 100;

        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtDescripcion.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;
            ErrorIcono.Clear();

            DgvListado.Columns[0].Visible = false;
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;
        }

        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeOK(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (TxtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar algunos datos, seran remarcados");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un Nombre");
                }
                else
                {
                    respuesta = NCategoria.Insertar(TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
                    if (respuesta.Equals("OK"))
                    {
                        this.MensajeOK("Se inserto de forma correcta el registro");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();

            TabGeneral.SelectedIndex = 0;
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (TxtNombre.Text == string.Empty || TxtId.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar algunos datos, seran remarcados");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un Nombre");
                }
                else
                {
                    respuesta = NCategoria.Actualizar(Convert.ToInt32(TxtId.Text), this.NombreAnterior, TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
                    if (respuesta.Equals("OK"))
                    {
                        this.MensajeOK("Se actualizo de forma correcta el registro");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;

                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                this.NombreAnterior = TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);

                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre");
            }

        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                BtnActivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;

            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                BtnActivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas eliminar el(los) registro(s)", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NCategoria.Eliminar(Codigo);

                            if (respuesta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas activar el(los) registro(s)", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NCategoria.Activar(Codigo);

                            if (respuesta.Equals("OK"))
                            {
                                this.MensajeOK("Se activo el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas desactivar el(los) registro(s)?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NCategoria.Desactivar(Codigo);

                            if (respuesta.Equals("OK"))
                            {
                                this.MensajeOK("Se desactivo el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    } 
}
