using System;
using System.Windows;
using System.Windows.Controls;
using CentroEducativoPalmarSur.Model;
namespace CentroEducativoPalmarSur.Pages
{
    /// <summary>
    /// Interaction logic for UsuariosPage.xaml
    /// </summary>
    public partial class UsuariosPage : Page
    {

        Usuario globalUser;

        public UsuariosPage(Usuario user)
        {
            InitializeComponent();
            globalUser = user;
            string sError = null;
            DataGridUsuarios.ItemsSource = new UsuarioDAO().Listar(ref sError, globalUser);
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
            DataGridUsuarios.ItemsSource = new UsuarioDAO().Listar(ref sError, globalUser);
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;

            if (!string.IsNullOrWhiteSpace(TxtNombre.Text) && !string.IsNullOrWhiteSpace(TxtClave.Text) && !string.IsNullOrWhiteSpace(TxtRespuesta.Text))
            {
               if (TxtClave.Text.Length == 8)
                    {
                    Usuario usuario = new Usuario(TxtNombre.Text, TxtRespuesta.Text, false, true);

                        bool result = new UsuarioDAO().CrearInvitado(usuario, TxtClave.Text, ref sError);

                        if (string.IsNullOrWhiteSpace(sError))
                        {
                            if (result)
                            {
                            DataGridUsuarios.ItemsSource = new UsuarioDAO().Listar(ref sError, globalUser);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo registrar al Usuario.", "Alert",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(sError, "Alert",  MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("La clave debe contener 8 caracteres", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
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
            Usuario user = DataGridUsuarios.SelectedItem as Usuario;

            bool result = new UsuarioDAO().Desactivar(user, ref sError);
            if (sError == null)
            {
                if (result)
                {
                    MessageBox.Show($"Se elimino a {user.NombreUsuario}", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataGridUsuarios.ItemsSource = new UsuarioDAO().Listar(ref sError, globalUser);
                }
            }
            else
            {
                MessageBox.Show(sError, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            
            DataGridUsuarios.CommitEdit();
            Usuario user = DataGridUsuarios.SelectedItem as Usuario; 

            if (!string.IsNullOrEmpty(user.NombreUsuario) && !String.IsNullOrEmpty(user.Respuesta))
            {
                bool result = new UsuarioDAO().Modificar(user, ref sError);

                if (!string.IsNullOrWhiteSpace(sError))
                    {

                        MessageBox.Show(sError, "Alert",  MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Debe de llenar todos los cuadros de texto", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DataGridUsuarios.ItemsSource = new UsuarioDAO().Listar(ref sError, globalUser);
        }
    }
}
