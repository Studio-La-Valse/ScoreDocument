using System.Windows;

namespace StudioLaValse.ScoreDocument.Example.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(UIElement canvas, UIElement selectionBorder)
        {
            InitializeComponent();

            this.grid.Children.Add(canvas);
            this.grid.Children.Add(selectionBorder);
        }
    }
}
