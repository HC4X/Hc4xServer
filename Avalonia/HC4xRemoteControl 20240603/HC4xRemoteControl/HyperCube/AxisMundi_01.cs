using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using LibModel;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using System;
using System.IO;
using System.Reflection;

namespace HyperCube.Platform
{
  public class LandLifetime
  {
    private const string Name = nameof(LandLifetime);
    #region Framework
    public Control fwMainWindow { get; protected set; }
    public StackPanel fwStackPanel { get; private set; }
    private ScrollViewer fwViewer
    {
      get
      {
        ScrollViewer retValue = default;
        if (fwStackPanel != null) retValue = fwStackPanel.FindControl<ScrollViewer>(atViewer);
        return (retValue);
      }
    }
    private Label fwLblMessage
    {
      get
      {
        Label retValue = default;
        if (fwStackPanel != null) retValue = fwStackPanel.FindControl<Label>(atStatusLbl);
        return (retValue);
      }
    }
    protected Assembly fwLibAsset { get; private set; }
    #endregion
    #region Attribute
    public string atBasePath { get; protected set; }
    public string atMetaPath { get; protected set; }
    public string atInterfacePath { get; protected set; }
    private string atViewer { get; set; }
    private string atStatusLbl { get; set; }
    private bool atIsFS { get; set; }
    #endregion
    #region Node
    public Evaluator ndEvaluator { get; private set; }
    public HyperApplication scHyperCubeApp { get; protected set; }
    public SectorLocale scLocale => scHyperCubeApp.scLocale;
    public SectorInterface scInterface => scHyperCubeApp.scInterface;
    #endregion
    #region Virtual
    public virtual void ShowException(Exception parErr, string parClass, string parMethod) => ShowException(parErr, parClass, parMethod, "");
    public virtual void ShowException(Exception parErr, string parClass, string parMethod, string parExtraInfo) => AxisMundi.ShowException(parErr, parClass, parMethod, parExtraInfo);
    public virtual Control LoadControl(string parFileName)
    {
      Control retValue;
      NodeInterface objInterface;
      try
      {
        objInterface = InterfacePath(parFileName);
        retValue = GearXAML.LoadXamlControl(objInterface.atContent);
      }
      catch (Exception Err) { retValue = default; ShowException(Err, Name, nameof(LoadControl)); }
      return (retValue);
    }
    public virtual bool LoadContent(string parFileName) => true;
    public virtual bool OpenCamera() => throw new NotImplementedException();
    public virtual bool InitEvaluator()
    {
      ndEvaluator = new Evaluator();
      return true;
    }
    public virtual bool AndroidStckPnl() => throw new NotImplementedException();
    public virtual bool Init() => true;
    public virtual bool ErrMsgBox(IMsBox<ButtonResult> objMsgBox) => AppMessage("Ready");
    #endregion
    #region Method
    public bool AppMessage(string parMessage) { if (fwLblMessage != null) { fwLblMessage.Content = parMessage; } return (true); }
    public void SetViewerContent(UserControl parControl) => fwViewer.Content = parControl;
    internal T GetViewerContent<T>()
    {
      T retValue;
      if (fwViewer.Content is UserControl objUserCtrl)
        retValue = (T)objUserCtrl.Content;
      else
        retValue = (T)fwViewer.Content;
      return (retValue);
    }
    public NodeInterface InterfacePath(string parFileName)
    {
      NodeInterface retValue;
      RawXml objXml;
      try
      {
        if (atIsFS)
        {
          parFileName = GearPath.Combine(atInterfacePath, parFileName);
          objXml = new RawXml(parFileName, hc4x_NodeType.XmlFileOpen);
        }
        else
        {
          parFileName = atInterfacePath + parFileName;
          objXml = GearXml.ParseStream(GetResource(parFileName));
        }
        retValue = new NodeInterface(objXml);
      }
      catch (Exception Err) { retValue = default; ShowException(Err, Name, nameof(InterfacePath)); }
      return (retValue);
    }
    protected bool MainStckPnl(StackPanel parStackPanel, string parViewer, string parStatusLbl)
    {
      fwStackPanel = parStackPanel;
      fwStackPanel.PointerPressed -= Pointer_Pressed;
      fwStackPanel.PointerPressed += Pointer_Pressed;
      atViewer = parViewer;
      atStatusLbl = parStatusLbl;
      return (true);
    }
    public Stream GetResource(string parResName) => fwLibAsset.GetManifestResourceStream(parResName);
    #endregion
    #region Event
    private void Pointer_Pressed(object? sender, PointerPressedEventArgs e)
    {
      StyledElement objElement;
      objElement = GearControl.GetElement(e.Source as Control);
      if (objElement != null) ndEvaluator.EvalPressed(objElement);
    }
    #endregion
    #region Constructor
    protected bool InitFS()
    {
      bool retValue = false;
      string strHyperCube;
      NodeLocale objLocale;
      try
      {
        atBasePath = atBasePath = fwLibAsset.Location.Substring(0, fwLibAsset.Location.IndexOf("HC4xRemoteControl.Desktop")); //# @"L:\HyperCube\Community\HC4xRemoteControl\LibHC4xAsset";
        atMetaPath = GearPath.Combine(atBasePath, "meta");
        strHyperCube = GearPath.Combine(atMetaPath, "HyperCube.xml");
        scHyperCubeApp = new HyperApplication(strHyperCube);
        objLocale = scLocale[scHyperCubeApp.ValueStr("LanguageCode")];
        atInterfacePath = GearPath.Combine(atBasePath, scInterface.atAreaPath);
        atInterfacePath = GearPath.Combine(atInterfacePath, objLocale.atLanguagePath);
        retValue = atIsFS = true;
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(InitFS)); }
      return (retValue);
    }
    protected bool InitRes()
    {
      bool retValue = false;
      string strHyperCube;
      NodeLocale objLocale;
      try
      {
        atBasePath = "LibHC4xAsset.";
        atMetaPath = atBasePath + "meta.";
        strHyperCube = atMetaPath + "HyperCube.xml";
        scHyperCubeApp = new HyperApplication(GetResource(strHyperCube));
        objLocale = scLocale[scHyperCubeApp.ValueStr("LanguageCode")];
        atInterfacePath = atBasePath + scInterface.atAreaPath + ".";
        atInterfacePath = atInterfacePath + objLocale.atLanguagePath.Replace('-', '_') + ".";
        retValue = true;
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(InitRes)); }
      return (retValue);
    }
    public LandLifetime() { fwLibAsset = LibHC4xAsset.Core.Init(); }
    #endregion
  }
}
