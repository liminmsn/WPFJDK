using Library;
using System;
using System.Windows;
using System.Windows.Controls;

namespace JDKManage.Views
{
    /// <summary>
    /// PageInfo.xaml 的交互逻辑
    /// </summary>
    public partial class PageInfo : Page
    {
        public PageInfo()
        { 
            InitializeComponent();
            exec.Text = Init("java --version");
            exec_.Text = Init("where java");
        }
        string Init(string comm)
        {
            var command = new Command(CommandType.CMD);
            var a = command.Send(comm);
            var val = a.ExitCode == 0 ? a.Out : a.Err;
            return val;
            //exec.Text = $"{val}";
            //MessageBox.Show(val);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            exec.Text = Init("javaa --version");
            exec_.Text = Init("where javaa");
        }
    }
}
