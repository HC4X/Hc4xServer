namespace DesignTest {
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
  /// <summary>Table: All, Field: atRecState</summary>
  public enum hc4x_RecState {
    None = 0,
    Read = 1,
    Update = 2,
    Delete = 4,
    Hidden = 8
    }
  }
