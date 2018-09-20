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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using aes128tools.Converters;
using aes128tools.Model;
using aes128tools.ViewModel;

namespace aes128tools
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void EncryptBotton_Click(object sender, RoutedEventArgs e)
        {


            if (AesViewModel.AesModel.IsValidToEn && HexStringConverter.TypeFlag == 0)
            {
                AesViewModel.AesModel.EncrypData = AesModel.Encrytp(AesViewModel.AesModel.Key,
                    AesViewModel.AesModel.IV, AesViewModel.AesModel.PlainData);
            }
           


        }

        private void DecryptBtton_Click(object sender, RoutedEventArgs e)
        {
            if (AesViewModel.AesModel.IsValidToDe)
            {
                HexStringConverter.TypeFlag = 0;
                AesViewModel.AesModel.PlainData = AesModel.Decrypt(AesViewModel.AesModel.Key,
                    AesViewModel.AesModel.IV, AesViewModel.AesModel.EncrypData);
              
            }
        }

        
    }
}
