using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI
{
    public class BaseClassLib
    {
        static void Main()
        {
            // Use the custom text writer
            CustomTextWriter customWriter = new CustomTextWriter();

            // Use the Write method of the custom writer
            customWriter.Write('A');  // Outputs: CustomWrite: A
        }
    }
}
