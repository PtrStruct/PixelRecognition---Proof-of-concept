using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PixelRecognition.MVVM.Commands
{
    public class PrintscreenCommand : ICommand
    {
        
        private Action _screenshot;

        public PrintscreenCommand(Action print)
        {
            _screenshot = print;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _screenshot.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
