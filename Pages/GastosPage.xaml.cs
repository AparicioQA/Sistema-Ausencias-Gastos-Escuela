using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using CentroEducativoPalmarSur.Model;

namespace CentroEducativoPalmarSur.Pages
{
    /// <summary>
    /// Interaction logic for GastosPage.xaml
    /// </summary>
    public partial class GastosPage : Page
    {
        List<TipoGasto> ListaTGastos;
        Usuario globalUser;
        public GastosPage(Usuario usuario)
        {
            InitializeComponent();
            globalUser = usuario;
            string sError = null;
            ListaTGastos = new TipoGastoDAO().Listar(ref sError);
            TiposGastoRegistro.ItemsSource = ListaTGastos;
            TiposGastoRegistro.DisplayMemberPath = "Nombre";
            TiposGastoRegistro.SelectedValuePath = "Id";
            DataGridGastos.ItemsSource = new GastosDAO().Listar(ref sError);
            ListaTiposGastosActualiza.ItemsSource = ListaTGastos;

            if (!string.IsNullOrWhiteSpace(sError))
            {
                MessageBox.Show(sError, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!globalUser.EsAdmin)
            {
                BtnAgregar.IsEnabled = false;
                BtnActualizar.Visibility = Visibility.Hidden;
                BtnEliminar.Visibility = Visibility.Hidden;

            }

        }

        public void Actualizar()
        {
            string sError = null;
            ListaTGastos = new TipoGastoDAO().Listar(ref sError);
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            Decimal monto;
            DateTime fecha;
            if (!string.IsNullOrWhiteSpace(Txtmonto.Text) && !string.IsNullOrWhiteSpace(TiposGastoRegistro.Text)
                && BoxFecha.SelectedDate.HasValue )
            {

                if (Decimal.TryParse(Txtmonto.Text, out monto) &&
                    DateTime.TryParse(BoxFecha.SelectedDate.Value.ToString(), out fecha))
                {
                    TipoGasto tipo = TiposGastoRegistro.SelectedItem as TipoGasto;
                    Decimal valor = Decimal.Parse(Txtmonto.Text, CultureInfo.InvariantCulture);
                    Gasto gasto = new Gasto(0, fecha, valor, tipo, globalUser);
                    bool result = new GastosDAO().Agregar(gasto, ref sError);
                    if (string.IsNullOrWhiteSpace(sError))
                    {
                        if (result)
                        {
                            DataGridGastos.ItemsSource = new GastosDAO().Listar(ref sError);
                        }
                        else
                        {
                            MessageBox.Show("La fecha o el monto no son validos.\nNo se registro el gasto", "Alert",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(sError, "Alert",
                     MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("La fecha o el monto no son validos.\nNo se registro el gasto", "Alert",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe de llenar todos los cuadros de texto", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            DataGridGastos.CommitEdit();

            Gasto gasto = (Gasto)DataGridGastos.SelectedItem;
            gasto.User = globalUser;
            bool result = new GastosDAO().Actualizar(gasto, ref sError);
            MessageBox.Show(gasto.Fecha.ToString());
            if (string.IsNullOrWhiteSpace(sError))
            {
                if (result)
                {
                    DataGridGastos.ItemsSource = new GastosDAO().Listar(ref sError);
                }
                else
                {
                    MessageBox.Show("La fecha o el monto no son validos.\nNo se registro el gasto", "Alert",
            MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(sError, "Alert",
             MessageBoxButton.OK, MessageBoxImage.Error);
            }

            DataGridGastos.ItemsSource = new GastosDAO().Listar(ref sError);

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            Gasto gasto = (Gasto)DataGridGastos.SelectedItem;
           
            bool result = new GastosDAO().Eliminar(gasto, ref sError);

            if (sError != null)
            {
                MessageBox.Show(sError, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(!result)
            {
                MessageBox.Show("No se Elimino.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DataGridGastos.ItemsSource = new GastosDAO().Listar(ref sError);
          
        }
    }
}
