using Avalonia.Controls;
using HyperCubeRemoteControlDesktop.SocketLib;
using SocketClient;
using System;

namespace HyperCubeRemoteControlDesktop.Views;

public partial class MainWindow : Window
{
    //Abrir evaluetor
    //Trazer o SocketStatus para esta janela
    //Flag para saber se o Client está conectado => Mensagem do servidor recebida = "Hello Client"
    //Flag falso se o botão sair for clicado
    //Mapear o xml
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
                    _mainView.PreviousPage.IsEnabled = true;
                    _mainView.NextPage.IsEnabled = true;
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
            _homePage.mensagem.Text += parMessage + "\n";
        }
        catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(ClientMessage)); }
        return (retValue);
    }
    private void IniciarTudo()
    {
        InitializeComponent();
        _mainView = new MainView();
        _homePage = new HomePage();
        _mainView.SendHello.Click += Cmd_Central;
        _mainView.PreviousPage.Click += Cmd_Central;
        _mainView.NextPage.Click += Cmd_Central;
    }
    public void Cmd_Central(object sender, EventArgs e)
    {
        if(ndEvaluator == null)
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
