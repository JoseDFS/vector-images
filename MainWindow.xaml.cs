using Microsoft.Win32;
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
using System.Xml;
using System.Xml.Linq;
using VectorImages.Controlador;

namespace VectorImages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int contF = 0;
        int contFF;
        string[] files;

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
            ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(ImagenesDG);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToTop();
            }
        }

        private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.SVG)|*.SVG" +
            "|All files(*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = true;
            
            contF = 0;
            files =null;
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    files = dialog.FileNames;
                    contFF = files.Length;
                    AddSVG(files);
                      
                        
                    
                }
                catch
                {
                    throw;
                }
                
            }
        }
            
        private void AddSVG(string[]a) {
            if (contF < contFF)
            {
                dg_newV.IsOpen = true;
                var ep = a[contF];
                string newPath = "";
                XmlDocument document = new XmlDocument();
                document.Load(ep);
                XmlNodeList elemList = document.GetElementsByTagName("path");
                for (int i = 0; i < elemList.Count; i++)
                {
                    string attrVal = elemList[i].Attributes["d"].Value;
                    Console.WriteLine(attrVal);
                    newPath += attrVal;
                }
                NewDataPath.Text = newPath;
                
                contF++;
            }
            else
            {
                dg_newV.IsOpen = false;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (NewName.Text != "")
            {
                ImgsCRUD.InsertIMG(NewName.Text, NewDataPath.Text);
                ImagenesDG.ItemsSource = ImgsCRUD.FilterImg("").DefaultView;
                ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(ImagenesDG);
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToTop();
                }
                MessageBox.Show("Ingresado a DB");
                NewName.Clear();
                NewDataPath.Clear();
                AddSVG(files);
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var id = ImagenesDG.Columns[0].GetCellContent(ImagenesDG.Items[ImagenesDG.SelectedIndex]) as TextBlock;
            Console.WriteLine(id.Text);
            ImgsCRUD.DeleteImg(id.Text);
            ImagenesDG.ItemsSource = ImgsCRUD.FilterImg("").DefaultView;
            ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(ImagenesDG);
        }

        private void NewDataPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            try {
                Path.Data = Geometry.Parse(NewDataPath.Text);
            }
            catch
            {
                Path.Data = Geometry.Parse("");
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ImagenesDG.Columns[4].Visibility = Visibility.Hidden;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            dg_newV.IsOpen = false;
            ImagenesDG.Columns[4].Visibility = Visibility.Visible;
            AddSVG(files);
        }

        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            var path = ImagenesDG.Columns[2].GetCellContent(ImagenesDG.Items[ImagenesDG.SelectedIndex]) as TextBlock;
            Console.WriteLine(path.Text);
            Clipboard.SetText(path.Text);
        }
    }
}
