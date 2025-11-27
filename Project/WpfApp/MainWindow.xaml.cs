using JDKManage.Views;
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
using Wpf.Ui.Controls;
using Button = System.Windows.Controls.Button;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxResult = System.Windows.MessageBoxResult;

namespace WpfApp
{
    public enum PageType
    {
        PageInfo,
        PageManage
    }
    public class PagesManage
    {
        public string Label_ { get; set; }
        public SymbolRegular Icon {  get; set; }

        public PageType Page { get; set; }
    }
    public class AppInfo
    {
        public string Title { get; set; }
        public string Color_Text_0 { get; set; }
        public string Color_0 { get; set; }
        public string Color_1 { get; set; }
        public string Color_2 { get; set; }
        public List<PagesManage> PageManages { get; set; }
        // 构造函数初始化数据
        public AppInfo()
        {
            Title = "JDK管理";
            Color_Text_0 = "#161823";
            Color_0 = "#f0a1afc9";
            Color_1 = "#4b5cc4";
            Color_2 = "#3b2e7e";
            PageManages = new List<PagesManage>() {
                new PagesManage()
                {
                    Label_="环境信息",
                    Icon=SymbolRegular.Info16,
                    Page=PageType.PageInfo,
                },
                new PagesManage()
                {
                    Label_="包管理",
                    Icon=SymbolRegular.Box16,
                    Page=PageType.PageManage,
                }
            };
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var app = new AppInfo();
            Title = app.Title;
            DataContext = app;
            InitializeComponent();
            SetPage(PageType.PageInfo);
        }


        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("User32.dll")]
        private static extern bool ReleaseCapture();
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new System.Windows.Interop.WindowInteropHelper(this).Handle,
            WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        private void SymbolIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var info = System.Windows.MessageBox.Show(
            "确定退出程序？",
            "提示",
            MessageBoxButton.OKCancel,
            MessageBoxImage.Question
            );
            if (info == MessageBoxResult.OK)
            {
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is PageType type)
            {
                SetPage(type);
            }
        }
        private void SetPage(PageType type)
        {
            frame.Content = null;
            switch (type)
            {
                case PageType.PageInfo:
                    frame.Content = new PageInfo();
                    break;
                case PageType.PageManage: 
                    frame.Content = new PageManage();
                    break;
            }
        }
    }
}
