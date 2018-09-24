using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PixelRecognition.MVVM.Commands
{
    public class BrowseImageCommand : ICommand
    {
        private Action _action;

        public BrowseImageCommand(Action Action)
        {
            _action = Action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
