using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using log4net;
using WpfApp1.Utility;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            //读取XML配置信息
            XMLHelper.ReadXml();
            //日志清除
            Task.Factory.StartNew(() =>
            {
                DirectoryInfo di = new DirectoryInfo(Parameter.LogFilePath);
                if (!di.Exists)
                    di.Create();
                FileInfo[] fi = di.GetFiles("Demo_*.log");
                DateTime dateTime = DateTime.Now;
                foreach (FileInfo info in fi)
                {
                    TimeSpan ts = dateTime.Subtract(info.LastWriteTime);
                    if (ts.TotalDays > Parameter.LogFileExistDay)
                    {
                        info.Delete();
                        LogHelper.Debug(string.Format("已删除日志。{0}", info.Name));
                    }
                }
                LogHelper.Debug("日志清理完毕。");
            });
        }

    }
}
