using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MemoryMappedFileTest
{
    [Serializable]
    class MyClass //: ISerializable
    {
        //public static string SS;
        public int a;
        public int b;
        public string s;
        public List<string> StrList;

        public MyClass()
        {
            //StrList = new List<string>();
        }
        //public MyClass(SerializationInfo info, StreamingContext context)
        //{
        //    //this.Name = info.GetString("Name ");
        //    //this.Size = (Size)info.GetValue("Size ", typeof(Size));
        //    //this.Location = (Point)info.GetValue("Location ", typeof(Point));
        //    //int i = (int)info.GetValue("a", typeof(int));
        //    string ss = (string)info.GetValue("s", typeof(string));
        //    //MyClass.SS = ss;
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("s ", "abcd");
        //}
    }
}
