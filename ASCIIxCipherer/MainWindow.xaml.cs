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

namespace ASCIIxCipherer
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

        private void Cipher_Button_Click(object sender, RoutedEventArgs e)
        {
            var iterations = int.Parse(IterationsCount_TextBox.Text);

            var hex_prev = "";
            var hex_curr = "";

            foreach(var ch in Plaintext_TextBox.Text)
            {
                int value = Convert.ToInt32(ch);
                hex_curr += String.Format("{0:X}", value);
            }

            //ASCIed_TextBox.Text = ascii;

            for (int i = 1; i < iterations; i++)
            {
                hex_prev = hex_curr;

                hex_curr = "";

                foreach (var ch in hex_prev)
                {
                    int value = Convert.ToInt32(ch);
                    hex_curr += String.Format("{0:X}", value);
                }
            }

            ASCIed_TextBox.Text = hex_curr;
        }

        private void Decipher_Button_Click(object sender, RoutedEventArgs e)
        {
            var deciphered = HexString2Ascii(ASCIed_TextBox.Text);

            while(true)
            {
                //if(deciphered.Length % 2 == 0)
                if (DoesContainsNotHex(deciphered))
                    break;

                deciphered = HexString2Ascii(deciphered);
            }

            Plaintext_TextBox.Text = deciphered;
        }


        static bool DoesContainsNotHex(string hex)
        {
            foreach(var ch in hex)
            {
                if(!(ch >= '0' && ch <= '9' || 
                   ch >= 'a' && ch <= 'f' ||
                   ch >= 'A' && ch <= 'F'))
                    return true;
            }

            return false;
        }


        private string HexString2Ascii(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hexString.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }
    }
}
