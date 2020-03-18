using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ThreadSafe.CrossThreading
{
    public static class CrossThreadText
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);
        /// <summary>
        /// Set text property of various controls
        ///  /// For updating the GUI Thread on Windows.
        /// Code sourced from: https://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe
        /// Code wrote by: Thunder (StackOverflow)
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                try
                {
                    form.Invoke(d, new object[] { form, ctrl, text });                    
                }
                catch (ObjectDisposedException IO)
                {
                    MessageBox.Show(IO.Message);
                }
            }
            else
            {
                ctrl.Text = text;
            }
        }


    }
}
