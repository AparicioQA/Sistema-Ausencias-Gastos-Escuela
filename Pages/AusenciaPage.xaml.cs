using System;
using System.Windows;
using System.Windows.Controls;
using CentroEducativoPalmarSur.Model;
namespace CentroEducativoPalmarSur.Pages
{
    /// <summary>
    /// Interaction logic for Ausencia.xaml
    /// </summary>
    public partial class AusenciaPage : Page
    {

        private Usuario globalUser;
        public AusenciaPage(Usuario user)
        {
            InitializeComponent();
            globalUser = user;
            string sError = null;
            DataGridAusencia.ItemsSource = new AusenciaDAO().Listar(ref sError);
            if (!string.IsNullOrWhiteSpace(sError))
            {
                MessageBox.Show(sError, "Alert",
                      MessageBoxButton.OK, MessageBoxImage.Error);
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
            DataGridAusencia.ItemsSource = new AusenciaDAO().Listar(ref sError);
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;

            if (!string.IsNullOrWhiteSpace(TxtMotivo.Text) && !string.IsNullOrWhiteSpace(TxtEmpleado.Text) && !string.IsNullOrWhiteSpace(DateBox.Text))
            {
                int emple = 0;

                if (int.TryParse(TxtEmpleado.Text, out emple))
                {
                    if (TxtEmpleado.Text.Length == 9)
                    {
                        Empleado emp = new Empleado(emple);

                        Ausencia ausencia = new Ausencia(TxtMotivo.Text, DateBox.SelectedDate.Value, emp);

                        bool result = new AusenciaDAO().Agregar(ausencia, ref sError);


                        if (string.IsNullOrWhiteSpace(sError))
                        {
                            if (result)
                            {
                                DataGridAusencia.ItemsSource = new AusenciaDAO().Listar(ref sError);
                            }
                            else
                            {
                                MessageBox.Show("Ya existe un colaborador registrado con la Cedula ingresada.", "Alert",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(sError, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("El numero de cedula ingresado debe de ser de 9 caracteres.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error el numero de Cedula contienen caracteres alfabeticos o especiales.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Debe de llenar todos los cuadros de texto", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            Ausencia ausencia = DataGridAusencia.SelectedItem as Ausencia;
            AusenciaDAO ause = new AusenciaDAO();
            bool result = ause.Eliminar(ausencia, ref sError);
    
            if (sError != null)
            {
                MessageBox.Show(sError, "Alert",  MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (result)
                {
                    DataGridAusencia.ItemsSource = new AusenciaDAO().Listar(ref sError);
                }
            }
          
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;

            DataGridAusencia.CommitEdit();
            Ausencia ausencia = DataGridAusencia.SelectedItem as Ausencia;

            if (!string.IsNullOrEmpty(ausencia.Motivo) && !string.IsNullOrEmpty(ausencia.Fecha.ToString()) && !string.IsNullOrEmpty(ausencia.EmpleadoV.Cedula.ToString()))
            {

                if (ausencia.EmpleadoV.Cedula.ToString().Length == 9)
                {
                    bool result = new AusenciaDAO().Actualizar(ausencia, ref sError);

                    if (!string.IsNullOrWhiteSpace(sError))
                    {

                        MessageBox.Show(sError, "Alert",
                      MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("La cedula indicada debe tener 9 digitos.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }
             
            }
            else
            {
                MessageBox.Show("Debe de llenar todos los cuadros de texto", "Alert",  MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DataGridAusencia.ItemsSource = new AusenciaDAO().Listar(ref sError);
        }
    }
}
