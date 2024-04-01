using System;
using System.IO;
using System.Reflection;

namespace DesignTest.HC4xCore {
  public class RawPage {
    public virtual bool Init() { return true; }
    public virtual bool Execute() { return true; }
    public virtual bool Render() { return true; }
    }
  public static class GearReflection {
    public static string AssemblyPath(string parDllName) {
      string retValue;
      retValue = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      retValue = Path.Combine(retValue, parDllName);
      return (retValue);
      }
    public static RawPage LoadAssembly(string parDllPath, string parClassName) {
      RawPage retValue;
      //# string strFileName;
      Assembly objAssembly;
      AssemblyName objAssName;
      try {
        //# strFileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //# strFileName = Path.Combine(strFileName, "DllTest.dll");
        objAssName = AssemblyName.GetAssemblyName(parDllPath);
        objAssembly = Assembly.Load(objAssName);
        retValue = (RawPage)objAssembly.CreateInstance(parClassName);
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    }
  }