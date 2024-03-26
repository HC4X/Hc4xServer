using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using DesignTest.HC4xCore;

namespace DesignTest {
  /// <summary>
  /// Interaction logic for Window_1.xaml
  /// </summary>
  public partial class Window_Email : Window {
    #region Axis
    private hc4x_SectorData ndSectorData => AxisMundi.ndSectorData;
    private HC4x_SectorUser ndSectorUser => ndSectorData.ndSectorUser;
    private HC4x_MailSender ndMailSender => ndSectorData.ndMailSender;
    #endregion
    public Window_Email() {
      InitializeComponent();
      RegExpTest.EvalExpression();
      }
    private void EnviarEmail() {
      Assembly.LoadFrom(@"R:\root\Nuget\Emgu.CV.runtime\Emgu.CV.4.7.0.5276\lib\netstandard2.0\Emgu.CV.dll");
      AxisMundi.Init(Path.Combine(Environment.CurrentDirectory, @"Data\Database.xml"));
      txtBodyPlain.Text = "Validação de Email\n"
                        + "Este email está sendo enviado por que o seu email foi cadastrado no site Hypercube4x.com\n"
                        + "Copie a url abaixo e cole no seu browser para validar o seu email:\n"
                        + "https://hypercube4x.com/validmail/B1C2D3E4F5\n";
      txtBodyHtml.Text = "<h1>Validação de Email</h1>\n"
                   + "<p>Este email está sendo enviado por que o seu email foi cadastrado no site Hypercube4x.com</p>\n"
                   + "<a href=\"https://hypercube4x.com/validmail/B1C2D3E4F5\">clique aqui</a> para ser direcionado e validar o email\n"
                   + "<p>Opcionalmente copie a url abaixo e cole no seu browser:</p>\n"
                   + "https://hypercube4x.com/validmail/B1C2D3E4F5\n";
      txtSubject.Text = "Subject de Teste";
      txtTO.Text = "alessandro@hypercube4x.com";
      }
    private void Action_MouseDown(object sender, MouseButtonEventArgs e) {
      string strAction;
      strAction = (sender as FrameworkElement).Name;
      switch (strAction) {
        case "ActInvoke":
          InvokeDll();
          break;
        case "ActEMail":
          Act_EMail();
          break;
        }
      }
    private void InvokeDll() {
      RawPage objRawPage;
      string strAssmPath;
      try {
        strAssmPath = GearReflection.AssemblyPath("DllTest.dll");
        objRawPage = GearReflection.LoadAssembly(strAssmPath, "HCStone.Logic.CustomerPage");
        objRawPage.Init();
        objRawPage.Execute();
        objRawPage.Render();
        }
      catch (Exception) { throw; }
      }
    private void Act_EMail() {
      HC4x_NodeUser objUser;
      RawMailMessage objMailMessage;
      HC4x_MailSender objMailSender;
      try {
        objMailMessage = new RawMailMessage() { atSubject = txtSubject.Text };
        objMailMessage.AddBodyPlain(txtBodyPlain.Text);
        objMailMessage.AddBodyHtml(txtBodyHtml.Text);
        objMailMessage.AddFrom(ndMailSender.atFrom, "");
        objMailMessage.AddTo(txtTO.Text, "");
        if (ndMailSender.SendMail(objMailMessage))
          Message.Content = "Email enviado com sucesso";
        else
          Message.Content = "Falha no envido do Email";
        }
      catch (Exception Err) { throw; }
      }
    }
  }
