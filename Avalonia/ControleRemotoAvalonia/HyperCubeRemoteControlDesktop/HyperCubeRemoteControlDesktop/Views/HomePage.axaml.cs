using Avalonia.Controls;
using IpPortEventArgs;
using System;
namespace HyperCubeRemoteControlDesktop;

public partial class HomePage : UserControl
{
  public HomePage()
  {
    InitializeComponent();
  }
  public event EventHandler<NavigationEventArgs> TrocarDeTelaEpassarAsInforma��esParaAproximaTela;
  private void TrocarDeTela(object sender, Avalonia.Interactivity.RoutedEventArgs e)
  {
    if (!(string.IsNullOrEmpty(ip.Text) || string.IsNullOrEmpty(port.Text)))
    {
      var navigarionArgs = new NavigationEventArgs(ip.Text, port.Text);
      TrocarDeTelaEpassarAsInforma��esParaAproximaTela?.Invoke(this, navigarionArgs);

    }
    else
    {
      mensagem.Text = "Preencha Ip e Porta!";
    }
  }
}