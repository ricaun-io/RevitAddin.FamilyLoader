using System;
using System.Windows;

namespace RevitAddin.FamilyLoader.Views
{
    public partial class FamilyLoaderView : Window
    {
        public FamilyLoaderView()
        {
            InitializeComponent();
            InitializeWindow();
            this.KeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Escape)
                {
                    this.Close();
                }
            };
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion
    }
}