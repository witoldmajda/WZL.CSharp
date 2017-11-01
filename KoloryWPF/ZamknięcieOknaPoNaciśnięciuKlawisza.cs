using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace KoloryWPF
{
    public class ZamknięcieOknaPoNaciśnięciuKlawisza : Behavior<Window>
    {
        public Key Klawisz { get; set; }

        protected override void OnAttached()
        {
            Window window = this.AssociatedObject;
            if (window != null) window.PreviewKeyDown += Window_PreviewKeyDown;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Key == Klawisz) window.Close();
        }
    }
}
