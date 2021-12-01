using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserPanel.ViewModels
{
    public class CustomMessageBoxViewModel
    {
        public static CustomMessageBoxViewModel mbox { get; set; }

        public string Message { get; set; }


        public static DialogResult Result { get; set; } = System.Windows.Forms.DialogResult.No;

        public enum CmessageboxButton
        {
            Ok,No,Yes,Cancel,Confirm
        }
        public enum CmessageboxTitle
        {

            Error,Info,Warning,Confirm
        }
        //public static DialogResult Show(string message,CmessageboxTitle title,CmessageboxButton btnOkey, CmessageboxButton btnNo)
        //{
        //    mbox = new CustomMessageBoxViewModel();
        //
        //    //Message = message;
        //
        //}
    }
}
