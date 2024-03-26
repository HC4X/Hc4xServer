using Avalonia.Controls;
using System;

namespace HyperCubeRemoteControlDesktop.Views;

public partial class MainView : UserControl
{
    #region Node
    #endregion
    #region Action
    #endregion
    #region Event


    public event EventHandler DesconectarSolicitado;

    private void Desconectar(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        DesconectarSolicitado?.Invoke(this, EventArgs.Empty);
    }
    #endregion
    #region Constructor
    public MainView()
    {
        InitializeComponent();
        Desligar.Click += Desconectar;
    }
    #endregion
}

