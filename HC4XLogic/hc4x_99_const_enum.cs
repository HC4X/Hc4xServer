namespace HC4x_Server.Logic {
  /// <summary>Table: hc4xAppUser, Field: AccessType</summary>
  public enum hc4x_AppAccessType {
    None = 0,
    Free = 1,
    Freemium = 2,
    Premium = 3
    }
  /// <summary>Table: hc4xUser, Field: typeUser</summary>
  public enum hc4x_UserType {
    None = 0,
    User = 1,
    Group = 2,
    System = 3
    }
  /// <summary>Table: hc4xUser, Field: selfUser</summary>
  public enum hc4x_UserStatus {
    None = 0,
    Admin = 1,
    Anonymous = 2,
    Waiting = 3,
    User = 4,
    Suspended = 5,
    Canceled = 6
    }
  /// <summary>Table: All, Field: atRecState</summary>
  public enum hc4x_RecState {
    None = 0,
    Read = 1,
    Update = 2,
    Delete = 4,
    Hidden = 8
    }
  public enum hc4x_PasswordLevel {
    None = 0,
    Weak = 1,
    Normal = 2,
    Strong = 3
    }

  public enum hc4x_CustomerCategory {
    None = 0,
    Produtor = 4,
    Beneficiador = 5,
    Distribuidor = 6,
    Construcao_civil = 7,
    Consumidor_final = 8
  }
  public enum hc4x_TypeAlert {
    Success,
    Info,
    Warning,
    Danger,
    Primary,
    Secundary
  }
}
