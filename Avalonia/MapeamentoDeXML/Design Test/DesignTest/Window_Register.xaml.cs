using System;
using System.IO;
using System.Windows;

namespace DesignTest {
  /// <summary>
  /// Interaction logic for Window_1.xaml
  /// </summary>
  public partial class Window_Register : Window {
    #region Axis
    private hc4x_SectorData ndSectorData => AxisMundi.ndSectorData;
    private HC4x_SectorUser ndSectorUser => ndSectorData.ndSectorUser;
    #endregion
    public Window_Register() {
      InitializeComponent();
      AxisMundi.Init(@"C:\Users\Dell\Documents\hc4x_stone\Data\Database.xml");
      }
    private void Action_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
      string strAction;
      strAction = (sender as FrameworkElement).Name;
      switch (strAction) {
        case "ActRegister":
          Act_Register();
          break;
        }
      }
    private void Act_Register() {
      HC4x_NodeUser objUser;
      try {
        objUser = ndSectorUser.FindUserOrGroup(txtUserName.Text);
        if (objUser == null)
          Message.Content = "Usuário não existe. Pode Cadastrar";
        else
          Message.Content = "Usuário já existente. Esqueceu sua senha?";
        }
      catch (Exception Err) { throw; }
      }
    }
  }
