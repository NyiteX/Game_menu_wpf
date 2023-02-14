using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

namespace wpf_pract_13._02
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string str = @"Data Source = WIN-U669V8L9R5E; Initial Catalog = GameDB; Trusted_Connection=True";
        public MainWindow()
        {
            InitializeComponent();


            for (int i = 0; i < 2; i++)
            {
                ini();
            }
            
            //ini2();
            
        }

        void ini()
        {
            //for (int i = 0; i < 10; i++) //Add 36 columns
            //{
            //    RowDefinition row = new RowDefinition();
            //    row.Height = new GridLength(40, GridUnitType.Pixel);
            //    grid.RowDefinitions.Add(row);
            //}
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    
                    SqlCommand command = new SqlCommand(
                        "SELECT Character_my.Names,Character_my.Lore,Pic_table.Pic FROM Character_my,Pic_table WHERE Pic_table.ID = Character_my.PicID",
                        connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        byte[] result = (byte[])reader.GetValue(2);

                        Stream StreamObj = new MemoryStream(result);
                        BitmapImage BitObj = new BitmapImage();

                        BitObj.BeginInit();
                        BitObj.StreamSource = StreamObj;
                        BitObj.EndInit();

                        Button t1 = new Button()
                        {
                            Content = reader.GetValue(0),
                            Background = new ImageBrush(BitObj),
                            FontSize = 30,
                            FontFamily = new FontFamily("consolas"),
                            FontWeight = FontWeights.SemiBold,
                            BorderBrush = Brushes.LightGray,
                            BorderThickness = new Thickness(2),
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            VerticalContentAlignment = VerticalAlignment.Top,
                            Style = Resources["ButtonStyle1"] as Style,

                            Height = 152,
                            Width = 125,
                        };
                        l_box.Items.Add(t1);        
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        async void ini2()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("SELECT Pic FROM Pic_table WHERE ID = 1", connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        byte[] result = (byte[])reader.GetValue(0);

                        Stream StreamObj = new MemoryStream(result);
                        BitmapImage BitObj = new BitmapImage();

                        BitObj.BeginInit();
                        BitObj.StreamSource = StreamObj;
                        BitObj.EndInit();

                        //btn1.Background = new ImageBrush(BitObj);          
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
