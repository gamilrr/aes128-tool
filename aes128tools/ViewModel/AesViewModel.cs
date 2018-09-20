using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using aes128tools.Annotations;
using aes128tools.Model;


namespace aes128tools.ViewModel
{
    class AesViewModel
    {
        public AesViewModel()
        {
            AesModel = new AesModel()
            {   Key = new byte[0],
                IV = new byte[16] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                EncrypData = new byte[0],
                PlainData = new byte[0]
            };
        }
       public static AesModel AesModel { set; get; }
    }
}
