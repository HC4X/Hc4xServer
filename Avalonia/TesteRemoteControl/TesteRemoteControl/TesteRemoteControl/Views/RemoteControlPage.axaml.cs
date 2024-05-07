using Avalonia.Controls;
using System;

namespace TesteRemoteControl;

public partial class RemoteControlPage : UserControl
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
    public RemoteControlPage()
    {
        InitializeComponent();
        Desligar.Click += Desconectar;
    }
    #endregion
}

