using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace MemoryMappedFileTest
{
    [Serializable]
    public partial class Form2 : Form, ISerializable
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(SerializationInfo info, StreamingContext context)
        {
            //this.Name = info.GetString("Name ");
            //this.Size = (Size)info.GetValue("Size ", typeof(Size));
            //this.Location = (Point)info.GetValue("Location ", typeof(Point));

            //int i = (int)info.GetValue("a", typeof(int));
            string ss = (string)info.GetValue("s", typeof(string));
            //MyClass.SS = ss;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("s ", "abcd");
        }
    }
}
