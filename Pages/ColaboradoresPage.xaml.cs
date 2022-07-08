using System;
using System.Windows;
using System.Windows.Controls;
using CentroEducativoPalmarSur.Model;
using CentroEducativoPalmarSur.Helper;
namespace CentroEducativoPalmarSur.Pages
{
    /// <summary>
    /// Interaction logic for Colaboradores.xaml
    /// </summary>
    public partial class ColaboradoresPage : Page
    {
        Usuario globalUser;
      
        public ColaboradoresPage(Usuario user)
        {
            InitializeComponent();
            globalUser = user;
            string sError = null;
            GridColaboradores.ItemsSource = new EmpleadoDAO().Listar(ref sError);
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
            GridColaboradores.ItemsSource = new EmpleadoDAO().Listar(ref sError);
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;

            if (!string.IsNullOrWhiteSpace(TxtCedula.Text) && !string.IsNullOrWhiteSpace(TxtNombre.Text) && !string.IsNullOrWhiteSpace(TxtApe1.Text) 
                && !string.IsNullOrWhiteSpace(TxtApe2.Text) && !string.IsNullOrWhiteSpace(TxtDireccion.Text) && !string.IsNullOrWhiteSpace(txtTelefono.Text)
                && !string.IsNullOrWhiteSpace(DateBox.Text))
            {
                int cedula = 0, telf = 0;
                if(int.TryParse(TxtCedula.Text, out cedula) && int.TryParse(txtTelefono.Text, out telf) && !Verificaciones.TieneNumeros(TxtNombre.Text) &&
                    !Verificaciones.TieneNumeros(TxtApe1.Text) && !Verificaciones.TieneNumeros(TxtApe2.Text))
                {
                    if (TxtCedula.Text.Length == 9 && txtTelefono.Text.Length == 8)
                    {
                        Empleado emp = new Empleado(cedula, TxtNombre.Text, TxtApe1.Text, TxtApe2.Text, TxtDireccion.Text, DateBox.SelectedDate.Value,
                        telf);

                        bool result = new EmpleadoDAO().Agregar(emp, ref sError);

                        if (string.IsNullOrWhiteSpace(sError))
                        {
                            if (result)
                            {
                                Actualizar();
                            }
                            else
                            {
                                MessageBox.Show("Ya existe un colaborador registrado con la Cedula ingresada.", "Alert",
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
                        MessageBox.Show("El numero de cedula ingresado debe de ser de 9 caracteres.\n" +
                            "El numero de telefono debe de ser de 8 caracteres", "Alert",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error el numero de Telefono o Cedula contienen caracteres alfabeticos o especiales.\n" +
                        "El Nombre y Apelidos no pueden llevar caracteres numericos", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
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

            GridColaboradores.CommitEdit();
            Empleado emp = GridColaboradores.SelectedItem as Empleado;

            if (!string.IsNullOrEmpty(emp.Telefono.ToString()) && !string.IsNullOrWhiteSpace(emp.Nombre) &&
                !string.IsNullOrWhiteSpace(emp.Apellido1) && !string.IsNullOrWhiteSpace(emp.Apellido2) && !string.IsNullOrWhiteSpace(emp.Direccion))
            {

                if (!Verificaciones.TieneNumeros(emp.Nombre) && !Verificaciones.TieneNumeros(emp.Apellido1) && !Verificaciones.TieneNumeros(emp.Apellido2))
                {
                    bool result = new EmpleadoDAO().Actualizar(emp, ref sError);
                    
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
                }
                else
                {
                   
                    MessageBox.Show("No se admiten caracteres numericos en el Nombre y/o Apellidos", "Alert",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                   
                }
            }
            else
            {
                MessageBox.Show("Debe de llenar todos los cuadros de texto", "Alert",
             MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Actualizar();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            Empleado empleado = GridColaboradores.SelectedItem as Empleado;
            EmpleadoDAO emp = new EmpleadoDAO();
            bool result = emp.Eliminar(empleado, ref sError);
            if(sError == null)
            {
                if (result)
                {
                    MessageBox.Show($"Se elimino a {empleado.Cedula}", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
            }
            else
            {
                MessageBox.Show(sError, "Alert",
                  MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Actualizar();
        }
    }
}
