using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using CentroEducativoPalmarSur.Model;
namespace CentroEducativoPalmarSur.Pages
{
    /// <summary>
    /// Interaction logic for PresupuestoPage.xaml
    /// </summary>
    public partial class PresupuestoPage : Page
    {
        private Usuario globalUser;
        public PresupuestoPage(Usuario user)
        {
            InitializeComponent();
            globalUser = user;
            string sError = null;
            DataGridPresupuesto.ItemsSource = new PresupuestoDAO().Listar(ref sError);
            if (!string.IsNullOrWhiteSpace(sError))
            {
                MessageBox.Show(sError, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!globalUser.EsAdmin)
            {
                BtnAgregar.IsEnabled = false;
                BtnActualizar.Visibility = Visibility.Hidden;
            
            }
        }

        public void Actualizar()
        {
            string sError = null;
            DataGridPresupuesto.ItemsSource = new PresupuestoDAO().Listar(ref sError);
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            if (!string.IsNullOrEmpty(TxtMonto.Text) && !string.IsNullOrEmpty(TxtYear.Text))
            {
                Decimal monto =0;
                int year;

                if (Decimal.TryParse(TxtMonto.Text, out monto) && int.TryParse(TxtYear.Text, out year))
                {
                    if(year >= 2010 && year <= 3000 && monto >= 0)
                    {
                        Decimal value = Decimal.Parse(TxtMonto.Text, CultureInfo.InvariantCulture);

                        Presupuesto presu = new Presupuesto(-1, year, value);
                        bool result = new PresupuestoDAO().Agregar(presu, ref sError);
                        if (string.IsNullOrWhiteSpace(sError) && result)
                        {
                            DataGridPresupuesto.ItemsSource = new PresupuestoDAO().Listar(ref sError);
                        }
                        else
                        {
                            MessageBox.Show(sError, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Valores no aceptados", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Solo se aceptan caracteres numericos", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
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
            DataGridPresupuesto.CommitEdit();

            Presupuesto presu = (Presupuesto)DataGridPresupuesto.SelectedItem;

            bool result = new PresupuestoDAO().Modificar(presu, ref sError);
            if (!string.IsNullOrWhiteSpace(sError))
            {
                MessageBox.Show(sError, "Alert",
              MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!result)
                {
                    MessageBox.Show("No se actualizaron los datos", "Alert",
                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            DataGridPresupuesto.ItemsSource = new PresupuestoDAO().Listar(ref sError);
        }

    
    }
}
