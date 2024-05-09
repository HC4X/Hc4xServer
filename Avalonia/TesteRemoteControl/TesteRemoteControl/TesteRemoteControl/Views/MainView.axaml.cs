using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using RemoteControl.XmlMapper;
using SocketClient;
using System;
using System.IO;
using System.Reactive;
using TesteRemoteControl.SocketLib;
namespace TesteRemoteControl.Views
{
    public partial class MainView : UserControl
    {
        #region Node
        private Evaluator? ndEvaluator { get; set; } = null;
        private RemoteControlPage _mainView;
        private HomePage _homePage;
        private RemoteDoc scRemoteDoc { get; set; }
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
            _mainView = new RemoteControlPage();
            _homePage = new HomePage();
            #endregion
            #region Buttons
            _mainView.SendHello.Click += Cmd_Central;
            _homePage.buttonQr.Click += OnCameraButtonClick;
            #region SectorNumeric
            _mainView.Key1.Click += Cmd_Central;
            _mainView.Key2.Click += Cmd_Central;
            _mainView.Key3.Click += Cmd_Central;
            _mainView.Key4.Click += Cmd_Central;
            _mainView.Key5.Click += Cmd_Central;
            _mainView.Key6.Click += Cmd_Central;
            _mainView.Key7.Click += Cmd_Central;
            _mainView.Key8.Click += Cmd_Central;
            _mainView.Key9.Click += Cmd_Central;
            #endregion
            #region SectorDirection
            _mainView.KeyUp.Click += Cmd_Central;
            _mainView.KeyDown.Click += Cmd_Central;
            _mainView.KeyLeft.Click += Cmd_Central;
            _mainView.KeyRight.Click += Cmd_Central;
            _mainView.KeyCentral.Click += Cmd_Central;
            #endregion
            #region SectorZoom
            _mainView.KeyXLeft.Click += Cmd_Central;
            _mainView.KeyXRight.Click += Cmd_Central;
            _mainView.KeyYUp.Click += Cmd_Central;
            _mainView.KeyYDown.Click += Cmd_Central;
            _mainView.KeyZIn.Click += Cmd_Central;
            _mainView.KeyZOut.Click += Cmd_Central;
            #endregion
            #region OpenCameraButton

            #endregion
            #endregion
            #region Reading XML file
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string resourcePath = "TesteRemoteControl.hypercube.remote.xml";
            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream != null)
                    {
                        string tempFilePath = Path.Combine(Path.GetTempPath(), "remote.xml");

                        using (var fileStream = File.Create(tempFilePath))
                        {

                            stream.CopyTo(fileStream);
                        }
                        scRemoteDoc = new RemoteDoc(tempFilePath);
                    }
                    else
                    {
                        throw new Exception($"Resource {resourcePath} not found.");
                    }
                }
            }
            catch (Exception Err)
            {
                AxisMundi.ShowException(Err, Name, nameof(IniciarTudo));
            }
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
                        var command = scRemoteDoc.GetCommandById(objElement.Name);
                        ndEvaluator.SendStr(command);
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
        public MainView()
        {
            IniciarTudo();

            (GlobalServices.CameraService).QrCodeTextChanged += CameraService_QrCodeTextChanged;

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
        public void OnCameraButtonClick(object sender, EventArgs e)
        {
            GlobalServices.CameraService?.OpenCamera();
        }
        private void CameraService_QrCodeTextChanged(object sender, string e)
        {
            string[] arSend;
            arSend = e.Split(':');
            Dispatcher.UIThread.InvokeAsync(() => _homePage.ip.Text = arSend[0]);
            Dispatcher.UIThread.InvokeAsync(() => _homePage.port.Text = arSend[1]);
        }
    }
}