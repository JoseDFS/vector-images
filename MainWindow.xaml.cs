using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
using System.Xml.Linq;
using VectorImages.Controlador;

namespace VectorImages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            Conexion con = new Conexion();
            if (con.AbrirConexion())
            {
                MessageBox.Show("Conectado");
                ImagenesDG.ItemsSource = ImgsCRUD.VerImgs().DefaultView;
            }
            else
            {
                MessageBox.Show("ERROR");
            }

            
        }

        private void Search_T_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = Search_T.Text;
            ImagenesDG.ItemsSource = ImgsCRUD.FilterImg(filter).DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NewName.Text != "" && NewDataPath.Text != "")
            {
                /*string data_txt = NewDataPath.Text.Replace("\"/><path d=\""," ");
                data_txt = data_txt.Trim('"');*/
                var str = XElement.Parse(NewDataPath.Text);
                var data_txt = str.Elements("path");
                //ImgsCRUD.InsertIMG(NewName.Text, data_txt);
                Console.WriteLine(data_txt);
                ImagenesDG.ItemsSource = ImgsCRUD.FilterImg("").DefaultView;
            }
        }
    }
}
