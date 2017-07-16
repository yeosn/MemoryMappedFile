using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.MemoryMappedFiles;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

//using MemoryMappedFileTest;

namespace MemoryMappedFileTest
{
    public partial class Form1 : Form
    {
        private MemoryMappedFile _mmf { get; set; }
        private MemoryMappedViewAccessor _viewAccessor { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialReader();            
        }

        //初始化
        private void InitialReader()
        {
            long capacity = 10 * 1024 * 1024;   //10MB

            //创建或者打开共享内存
            //这个共享内存的名称是testMmf，共享内存的大小为10MB
            this._mmf = MemoryMappedFile.CreateOrOpen("testMmf", capacity);
            //this._mmf = MemoryMappedFile.OpenExisting("testMmf");

            //通过MemoryMappedFile的CreateViewAccssor方法获得共享内存的访问器  
            this._viewAccessor = this._mmf.CreateViewAccessor(0, capacity);
        }

        //读取数据
        private string ReadData()
        {
            //读取字符长度  
            int strLength = this._viewAccessor.ReadInt32(0);
            char[] charsInMmf = new char[strLength];

            //读取字符
            this._viewAccessor.ReadArray<char>(4, charsInMmf, 0, strLength);
            string str = new string(charsInMmf);
            return str;
        }

        private object ReadData_Class()
        {
            //读取数据流长度  
             int strLength = this._viewAccessor.ReadInt32(0);
            byte[] charsInMmf = new byte[strLength];

            //读取数据流
            this._viewAccessor.ReadArray<byte>(4, charsInMmf, 0, strLength);
            
            //把数据流反序列化为对象
            MemoryStream stream = new MemoryStream(charsInMmf);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Binder = new UBinder();
            return formatter.Deserialize(stream);
        }
           
        private void Form1_Activated(object sender, EventArgs e)
        {
            //while (true)
            //{
            //    this.textBox1.Text = ReadData();
            //    Application.DoEvents();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.textBox1.Text = ReadData();
            //Application.DoEvents();

            //while (true)
            //{
            //    this.textBox1.Text = ReadData();
            //    //Thread.Sleep(200);

            //    Application.DoEvents();
            //}

            while (true)
            {
                object obj = ReadData_Class();

                MyClass myCls = (MyClass)obj;
                //Form form = (Form)obj;

                //Thread.Sleep(200);

                Application.DoEvents();
            }
        }

        public class UBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Assembly ass = Assembly.GetExecutingAssembly();
                return ass.GetType(typeName);
            }
        }
    }
}
