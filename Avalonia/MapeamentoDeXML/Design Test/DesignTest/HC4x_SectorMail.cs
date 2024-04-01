using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using LibModel;

namespace DesignTest {
  public class RawMailMessage {
    private const string Name = nameof(RawMailMessage);
    #region Attribute
    public string atFrom => ndMailMsg.From.ToString();
    public string atSubject {
      get => ndMailMsg.Subject;
      set => ndMailMsg.Subject = value;
      }
    public string atBody => ndMailMsg.Body;
    #endregion
    #region Node
    public MailMessage ndMailMsg { get; internal set; }
    #endregion
    #region Method
    /// <summary>Experimental</summary>
    public string AttachImage(string parFileName) {
      string retValue;
      Attachment objAttachment;
      try {
        objAttachment = new Attachment(parFileName, MediaTypeNames.Image.Jpeg);
        ndMailMsg.Attachments.Add(objAttachment);
        retValue = objAttachment.ContentId;
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    public bool AddBodyPlain(string parPlain) {
      bool retValue = false;
      AlternateView objView;
      try {
        objView = AlternateView.CreateAlternateViewFromString(parPlain, Encoding.UTF8, MediaTypeNames.Text.Plain);
        ndMailMsg.AlternateViews.Add(objView);
        retValue = true;
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    public bool AddBodyHtml(string parHtml) {
      bool retValue = false;
      AlternateView objView;
      try {
        objView = AlternateView.CreateAlternateViewFromString(parHtml, Encoding.UTF8, MediaTypeNames.Text.Html);
        ndMailMsg.AlternateViews.Add(objView);
        retValue = true;
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    public bool AddFrom(string parAddress, string parDisplay) {
      ndMailMsg.From = ParseAddress(parAddress, parDisplay);
      return (true);
      }
    public bool AddTo(string parAddress, string parDisplay) {
      ndMailMsg.To.Add(ParseAddress(parAddress, parDisplay));
      return (true);
      }
    public bool AddCC(string parAddress, string parDisplay) {
      ndMailMsg.CC.Add(ParseAddress(parAddress, parDisplay));
      return (true);
      }
    public bool AddBcc(string parAddress, string parDisplay) {
      ndMailMsg.Bcc.Add(ParseAddress(parAddress, parDisplay));
      return (true);
      }
    private MailAddress ParseAddress(string parAddress, string parDisplay) { return (new MailAddress(parAddress, parDisplay, Encoding.UTF8)); }
    #endregion
    #region Constructor
    public RawMailMessage() {
      ndMailMsg = new MailMessage() {
        BodyEncoding = Encoding.UTF8,
        SubjectEncoding = Encoding.UTF8,
        IsBodyHtml = true,
        };
      }
    #endregion
    }
  /// <summary>Mail Sender class</summary>
  public class HC4x_MailSender : RawXml {
    private const string Name = nameof(HC4x_MailSender);
    #region Attribute
    public string atSmtp => ValueStr(c_smtp);
    public int atPort => ValueInt(c_port);
    public string atFrom => ValueStr(c_from);
    public string atPass => ValueStr(c_pass);
    #endregion
    #region Method
    public bool SendMail(RawMailMessage parMail) {
      bool retValue = false;
      NetworkCredential objCredential;
      SmtpClient objSmtp;
      try {
        objCredential = new NetworkCredential(atFrom, atPass);
        objSmtp = new SmtpClient(atSmtp, atPort) {
          Credentials = objCredential,
          EnableSsl = false,
          UseDefaultCredentials = false
          };
        objSmtp.Send(parMail.ndMailMsg);
        retValue = true;
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    #endregion
    #region Constructor
    public bool Init() { ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; return (true); }
    public HC4x_MailSender(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    private const string c_pass = "Pass";
    private const string c_from = "From";
    private const string c_port = "Port";
    private const string c_smtp = "Smtp";
    #endregion
    }
  }
