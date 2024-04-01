using System;
using System.Windows;
using System.Windows.Input;
using DesignTest.Atom;
using LibInterface;

namespace DesignTest {
  /// <summary>
  /// Interaction logic for Window_Atom.xaml
  /// </summary>
  public partial class Window_Atom : Window {
    #region Node
    private AtomEadDoc scAtomEadDoc { get; set; }
    private RemoteDoc scRemoteDoc { get; set; }
    #endregion
    #region Event
    private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
      string[] arFileName;
      try {
        arFileName = GearFileDialog.OpenFile("Atom EAD Xml", Environment.CurrentDirectory, "", "XMl (*.xml)|*.xml", true, false);
        if (arFileName != null)
          scRemoteDoc = new RemoteDoc(arFileName[0]);
          //scAtomEadDoc = new AtomEadDoc(arFileName[0]);

        }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(Label_MouseDown)); }
      }
    #endregion
    #region Constructor
    public Window_Atom() {
      InitializeComponent();
      }
    #endregion
    }
  }
