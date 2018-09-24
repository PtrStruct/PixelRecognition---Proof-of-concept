using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PixelRecognition.Annotations;
using PixelRecognition.MVVM.Commands;

namespace PixelRecognition.MVVM
{
    public class BaseViewModel
    {
        public Screenshot Screenshot { get; } = new Screenshot();
    }
}
