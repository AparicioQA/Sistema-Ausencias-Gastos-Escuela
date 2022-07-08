using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CentroEducativoPalmarSur.Model;
namespace CentroEducativoPalmarSur.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 

    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnRegistro(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registro());
        }

        private void BtnRestablecimiento(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Restablecer());
        }
    
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string sError = null;
            if (!string.IsNullOrEmpty(ClaveBox.Password) && !string.IsNullOrEmpty(UserNameBox.Text))
            {
               
                UsuarioDAO us = new UsuarioDAO();
                Usuario usuario = us.Ingresar(UserNameBox.Text, ClaveBox.Password, ref sError);
                if (string.IsNullOrEmpty(sError))
                {
                    if (usuario != null)
                    {
                        Window closeMain = Application.Current.MainWindow;
                        Application.Current.MainWindow = new ProgramWindow(usuario);
                        closeMain.Close();

                        Application.Current.MainWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error el Usuario no existe o sus datos estan incorrectos\nPor favor verifique sus datos y vuelvalo a intentar", "Alert",
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
                MessageBox.Show("Para poder iniciar sesión debe ingresar sus datos", "Alert",
              MessageBoxButton.OK, MessageBoxImage.Error);
            }
          

        }
    }
}
