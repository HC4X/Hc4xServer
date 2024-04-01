using System;
using System.IO;
using System.Windows;

namespace DesignTest {
  /// <summary>
  /// Interaction logic for Window_1.xaml
  /// </summary>
  public partial class Window_Login : Window {
    #region Axis
    private hc4x_SectorData ndSectorData => AxisMundi.ndSectorData;
    private HC4x_SectorUser ndSectorUser => ndSectorData.ndSectorUser;
    #endregion
    public Window_Login() {
      InitializeComponent();
      AxisMundi.Init(@"C:\Users\Dell\Documents\hc4x_stone\Data\Database.xml");
      }
    private void Action_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
      string strAction;
      strAction = (sender as FrameworkElement).Name;
      switch (strAction) {
        case "ActLogin":
          Act_Login();
          break;
        }
      }
    private void Act_Login() {
      HC4x_NodeUser objUser;
      try {
        objUser = ndSectorUser.FindUser(txtUserName.Text);
        if (objUser != null) {
          if (objUser.ValidPassword(txtUserPass.Text))
            Message.Content = "Login autorizado";
          else
            Message.Content = "senha inválida. Esqueceu a senha?";
          }
        else
          Message.Content = "Usuário não existente. Tem que registrar";
        }
      catch (Exception Err) { throw; }
      }
    }
  }
