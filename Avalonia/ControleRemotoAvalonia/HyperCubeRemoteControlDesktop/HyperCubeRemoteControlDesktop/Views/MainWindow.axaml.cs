using Avalonia.Controls;
using HyperCubeRemoteControlDesktop.SocketLib;
using SocketClient;
using System;

namespace HyperCubeRemoteControlDesktop.Views;

public partial class MainWindow : Window
{
  #region Node
  private Evaluator? ndEvaluator { get; set; } = null;
  private MainView _mainView;
  private HomePage _homePage;
  private bool isConnected = false;
  #endregion
  #region Action
  private string ClientMessage(string parMessage, hc4x_SocketStatus parStatus)
  {
    string retValue = "";
    try
    {
      switch (ndEvaluator.EvalMessage(parMessage))
      {
        case EnumSocketStatus.HelloReceived:
          _mainView.Command_Left.IsEnabled = true;
          _mainView.Command_Right.IsEnabled = true;
          break;
        default:
          break;
      }
      if (parMessage == "Hello Client")
      {
        ShowView(_mainView);
        isConnected = true;
        _mainView.TxtClientLog.Text += parMessage + "\n";
      }
      if (isConnected && parMessage != "Hello Client") _mainView.TxtClientLog.Text += parMessage + "\n";
      _homePage.mensagem.Text += parMessage + "\n";
    }
    catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(ClientMessage)); }
    return (retValue);
  }
  private void IniciarTudo()
  {
    InitializeComponent();
    #region Views
    _mainView = new MainView();
    _homePage = new HomePage();
    #endregion
    #region Buttons
    _mainView.SendHello.Click += Cmd_Central;
    #region SectorNumeric
    _mainView.Command_1.Click += Cmd_Central;
    _mainView.Command_2.Click += Cmd_Central;
    _mainView.Command_3.Click += Cmd_Central;
    _mainView.Command_4.Click += Cmd_Central;
    _mainView.Command_5.Click += Cmd_Central;
    _mainView.Command_6.Click += Cmd_Central;
    _mainView.Command_7.Click += Cmd_Central;
    _mainView.Command_8.Click += Cmd_Central;
    _mainView.Command_9.Click += Cmd_Central;
    #endregion
    #region SectorDirection
    _mainView.Command_Left.Click += Cmd_Central;
    _mainView.Command_Up.Click += Cmd_Central;
    _mainView.Command_Right.Click += Cmd_Central;
    _mainView.Command_Down.Click += Cmd_Central;
    _mainView.Command_Central.Click += Cmd_Central;
    #endregion
    #region SectorZoom
    _mainView.Command_XLeft.Click += Cmd_Central;
    _mainView.Command_XRight.Click += Cmd_Central;
    _mainView.Command_YUp.Click += Cmd_Central;
    _mainView.Command_YDown.Click += Cmd_Central;
    _mainView.Command_ZIn.Click += Cmd_Central;
    _mainView.Command_ZOut.Click += Cmd_Central;
    #endregion
    #endregion
  }
  public void Cmd_Central(object sender, EventArgs e)
  {
    if (ndEvaluator == null)
    {
      ndEvaluator = new(ClientMessage);
      isConnected = true;
    }
    Button objElement;
    try
    {
      objElement = sender as Button;
      switch (objElement.Name)
      {
        case "SendHello":
          ndEvaluator.SendHello(_mainView.txtSendTo.Text, _mainView.txtMessage.Text);
          break;
        case "Desligar":
          ndEvaluator.SendStr("Desconectando...");
          _homePage.mensagem.Text = "";
          _mainView.TxtClientLog.Text = "";
          ShowView(_homePage);
          ndEvaluator.Close();
          ndEvaluator = null;
          isConnected = false;
          break;
        default:
          ndEvaluator.SendStr(objElement.Name);
          break;
      }
    }
    catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(Cmd_Central)); }
  }
  public void ShowView(UserControl view)
  {
    ContentArea.Content = view;
  }

  #endregion
  public MainWindow()
  {
    IniciarTudo();
    _homePage.TrocarDeTelaEpassarAsInformaçõesParaAproximaTela += (sender, e) =>
    {
      _mainView.txtSendTo.Text = e.IP + ":" + e.Porta;
      Cmd_Central(_mainView.SendHello, EventArgs.Empty);
    };
    _mainView.DesconectarSolicitado += (sender, e) =>
    {
      Cmd_Central(_mainView.Desligar, EventArgs.Empty);
    };
    ShowView(_homePage);
  }
}
