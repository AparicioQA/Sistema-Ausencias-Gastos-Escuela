using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CentroEducativoPalmarSur.Model;
namespace CentroEducativoPalmarSur.Pages
{
    /// <summary>
    /// Interaction logic for Restablecer.xaml
    /// </summary>
    public partial class Restablecer : Page
    {
        public Restablecer()
        {
            InitializeComponent();
        }

        private void BtnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            if (!string.IsNullOrEmpty(TxtNombre.Text) && !string.IsNullOrEmpty(TxtClave.Password) && !string.IsNullOrEmpty(TxtRespuesta.Text)
                 && !string.IsNullOrWhiteSpace(TxtClaveRepetida.Password))
            {
                if (TxtClave.Password.Length == 8)
                {
                    if (TxtClaveRepetida.Password.Equals(TxtClave.Password))
                    {
                        UsuarioDAO us = new UsuarioDAO();
                        bool result = us.Restablecer(TxtNombre.Text, TxtClave.Password, TxtRespuesta.Text, ref sError);
                        if (string.IsNullOrWhiteSpace(sError))
                        {
                            if (result)
                            {
                                NavigationService.Navigate(new Login());
                            }
                            else
                            {
                                MessageBox.Show("Error el nombre de usuario o respuesta no son validos", "Alert",
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
                    MessageBox.Show("La contraseña debe de ser solo de 8 caracteres", "Alert",
                                         MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Por favor llene todos los cuadros de texto", "Alert",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }
    }
}
