using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WpfApp
{
    public class AppInfo
    {
        public string Title { get; set; }
        public string Color_Text_0 { get; set; }
        public string Color_0 { get; set; }
        public string Color_1 { get; set; }
        public string Color_2 { get; set; }

        // 构造函数初始化数据
        public AppInfo()
        {
            Title = "JDK管理";
            Color_Text_0 = "#E2852E";
            Color_0 = "#E2852E";
            Color_1 = "#F5C857";
            Color_2 = "#FFEE91";
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Title_ { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            var app = new AppInfo();
            DataContext = app;
            Title = app.Title;
        }


        // 导入 Win32 API：发送消息给窗口
        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        // 导入 Win32 API：释放鼠标捕获
        [DllImport("User32.dll")]
        private static extern bool ReleaseCapture();
        // Win32 消息常量：非客户区鼠标左键按下
        private const int WM_NCLBUTTONDOWN = 0xA1;
        // 非客户区区域常量：标题栏
        private const int HT_CAPTION = 0x2;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            // 向窗口发送非客户区左键按下消息，指定为标题栏区域
            SendMessage(new System.Windows.Interop.WindowInteropHelper(this).Handle,
                        WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        private void SymbolIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
           var info =  MessageBox.Show(
                "确定退出程序？",
                "提示",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question
           );
            if (info == MessageBoxResult.OK) {
                Close();
            }
        }
    }
}
