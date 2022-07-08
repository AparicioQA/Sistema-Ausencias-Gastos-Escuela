using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CentroEducativoPalmarSur.Model;
namespace CentroEducativoPalmarSur.Pages
{
    /// <summary>
    /// Interaction logic for Registro.xaml
    /// </summary>
    public partial class Registro : Page
    {
        public Registro()
        {
            InitializeComponent();
        }
        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }
        private void BtnRegistro_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            if(!string.IsNullOrWhiteSpace(TxtNombre.Text) && !string.IsNullOrWhiteSpace(TxtClave.Password) && !string.IsNullOrWhiteSpace(TxtRespuesta.Text)
               && !string.IsNullOrWhiteSpace(TxtClaveRepetida.Password))
            {
                if (TxtClave.Password.Length == 8)
                {
                    if (TxtClaveRepetida.Password.Equals(TxtClave.Password))
                    {
                        UsuarioDAO us = new UsuarioDAO();
                        bool result = us.Registrar(TxtNombre.Text, TxtClave.Password, TxtRespuesta.Text, ref sError);
                        if (string.IsNullOrWhiteSpace(sError))
                        {
                            if (result)
                            {
                                NavigationService.Navigate(new Login());
                            }
                            else
                            {
                                MessageBox.Show("Error el nombre de usuario y/o respuesta no es valida\nLa contraseña debe de ser solo de 8 caracteres", "Alert",
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
                        MessageBox.Show("Las contraseñas deben de coincidir", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                else
                {
                    MessageBox.Show("La contraseña debe de ser de 8 caracteres", "Alert",
                                         MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
            else
            {
                MessageBox.Show("Por favor llene todos los cuadros de texto", "Alert",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
