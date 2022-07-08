using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CentroEducativoPalmarSur.Model;
using CentroEducativoPalmarSur.Pages;
using System.Diagnostics;

namespace CentroEducativoPalmarSur
{
    /// <summary>
    /// Interaction logic for ProgramWindow.xaml
    /// </summary>
    public partial class ProgramWindow : Window
    {
        public Usuario user;
      
        private ColaboradoresPage ColaboradoresLink;
        private UsuariosPage UsuariosLink;
        private AusenciaPage AusenciasLink;
        private PresupuestoPage PresupuestosLink;
        private GastosPage GastosLink;

        public ProgramWindow(Usuario user)
        {
            InitializeComponent(); 
            this.user = user;
            TitleWindow.Content = "Colaboradores";
            UsuariosLink = new UsuariosPage(user);
            ColaboradoresLink = new ColaboradoresPage(user);
            AusenciasLink = new AusenciaPage(user);
            PresupuestosLink = new PresupuestoPage(user);
            GastosLink = new GastosPage(user);
            PagesContainer.NavigationService.Navigate(ColaboradoresLink);
            if (user.EsAdmin)
            {
                ListBoxItem usuario = new ListBoxItem();
                usuario.Style = (Style)FindResource("ProgramItems");
                usuario.Content = "Usuarios";
                NameTitle.Items.Add(usuario);
            }
        }

        //private void BtnLogout_Click(object sender, RoutedEventArgs e)
        //{
        //    Window closeProgram = Application.Current.MainWindow;
           
        //    Application.Current.MainWindow = new MainWindow();
        //    closeProgram.Close();
        //    Application.Current.MainWindow.Show();
           
        //}

        private void NameTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string page = (NameTitle.SelectedItem as ListBoxItem).Content.ToString();
          
            if(page == "Colaboradores" && TitleWindow.Content.ToString() != "Colaboradores")
            {
                TitleWindow.Content = page;
                ColaboradoresLink.Actualizar();
                PagesContainer.NavigationService.Navigate(ColaboradoresLink);
            }
            else if (page == "Ausencias" && TitleWindow.Content.ToString() != "Ausencias")
            {
                TitleWindow.Content = page;
                AusenciasLink.Actualizar();
                PagesContainer.NavigationService.Navigate(AusenciasLink);
            }
            else if (page == "Usuarios" && TitleWindow.Content.ToString() != "Usuarios")
            {
                TitleWindow.Content = page;
                UsuariosLink.Actualizar();
                PagesContainer.NavigationService.Navigate(UsuariosLink);
            }
            else if (page == "Presupuestos" && TitleWindow.Content.ToString() != "Presupuestos")
            {
                TitleWindow.Content = page;
                PresupuestosLink.Actualizar();
                PagesContainer.NavigationService.Navigate(PresupuestosLink);
            }
            else if (page == "Gastos" && TitleWindow.Content.ToString() != "Gastos")
            {
               TitleWindow.Content = page;
               GastosLink.Actualizar();
               PagesContainer.NavigationService.Navigate(GastosLink);
            }else if (page == "Manual de Usuario")
            {
                string pdfPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,
                    "Guia\\Manual de Usuario.pdf");

                Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });

            }else if (page == "Cerrar Sesion")
            {
                Window closeProgram = Application.Current.MainWindow;

                Application.Current.MainWindow = new MainWindow();
                closeProgram.Close();
                Application.Current.MainWindow.Show();
            }


        }
    }
}
