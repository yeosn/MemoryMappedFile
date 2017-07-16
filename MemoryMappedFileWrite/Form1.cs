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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            //初始化
            InitialWriter();
        }

        //初始化
        private void InitialWriter()
        {
            long capacity = 10 * 1024 * 1024;   //10MB

            //创建或者打开共享内存
            //这个共享内存的名称是testMmf，共享内存的大小为10MB
            this._mmf = MemoryMappedFile.CreateOrOpen("testMmf", capacity);

            //通过MemoryMappedFile的CreateViewAccssor方法获得共享内存的访问器  
            this._viewAccessor = this._mmf.CreateViewAccessor(0, capacity);
        }

        //写入数据
        private void WriteData(string data)
        {
            //向共享内存开始位置写入字符串的长度  
            this._viewAccessor.Write(0, data.Length);

            //向共享内存4位置写入字符  
             this._viewAccessor.WriteArray<char>(4, data.ToArray(), 0, data.Length);
        }

        //写入数据(类/结构)
        private void WriteData_Class(object data)
        {
            //把对象序列化
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);

            //向共享内存开始位置写入字符串的长度  
            this._viewAccessor.Write(0, stream.Length);

            //向共享内存4位置写入字符  
            this._viewAccessor.WriteArray<byte>(4, stream.ToArray(), 0, (int)stream.Length);
        }
        private void btnWrite_Click(object sender, EventArgs e)
        {
            //string inputData = this.textBox1.Text.Trim();
            //if(inputData != null)
            //{
            //    WriteData(inputData);
            //}

            //传自定义的类

            MyClass myCls = new MyClass();
            myCls.a = 100;
            myCls.b = 234;
            myCls.s = "内存映射文件测试！！！";
            myCls.StrList = new List<string>();
            myCls.StrList.Add("a");
            myCls.StrList.Add("b");
            myCls.StrList.Add("c");
            myCls.StrList.Add("d");
            object inputData = myCls;

            //传窗体Form2
            //Form2 form2 = new Form2();
            //object inputData = form2;

            if (inputData != null)
            {
                WriteData_Class(inputData);
            }
        }
    }
}
