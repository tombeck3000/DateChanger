using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.IO;
using System.Drawing;

using ExifLibrary;

namespace DateChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //Configuration.Default.AddImageFormat(new JpegFormat());

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JPG (*.jpg)|*.jpg";
            openFileDialog.InitialDirectory = @"t:\Hochzeitsalbum\";

            var date = dtPicker.SelectedDate;
            var hour = txtHour.Text;

            if (!string.IsNullOrWhiteSpace(hour) && date != null)
            {
                var dateTime = date.Value.AddHours(int.Parse(hour));
                if (openFileDialog.ShowDialog() == true)
                {
                    foreach (string filename in openFileDialog.FileNames.OrderBy(fn => fn).ToList())
                    {
                        //var xxx = System.IO.Path.GetFileName(filename);
                        //lbfiles.Items.Add(xxx);
                   
                        var file = ImageFile.FromFile(filename);
                        file.Properties.Set(ExifTag.DateTimeDigitized, dateTime);
                        file.Properties.Set(ExifTag.DateTimeOriginal, dateTime);
                        file.Save(filename);

                        dateTime = dateTime.AddSeconds(60);
                    }
                }
                    
            }
        }        
    }
}
