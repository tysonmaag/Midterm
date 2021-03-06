// NX 10.0.0.24
// Journal created by tyguy147 on Sun Apr 01 20:21:34 2018 Mountain Daylight Time
//
using System;
using NXOpen;

public class NXJournal
{
  public static void Main(string[] args)
  {
    NXOpen.Session theSession = NXOpen.Session.GetSession();
    NXOpen.Part workPart = theSession.Parts.Work;
    NXOpen.Part displayPart = theSession.Parts.Display;
    // ----------------------------------------------
    //   Menu: File->Export->IGES...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.IgesCreator igesCreator1;
    igesCreator1 = theSession.DexManager.CreateIgesCreator();
    
    igesCreator1.ExportModelData = true;
    
    igesCreator1.ExportDrawings = true;
    
    igesCreator1.MapTabCylToBSurf = true;
    
    igesCreator1.BcurveTol = 0.0508;
    
    igesCreator1.IdenticalPointResolution = 0.001;
    
    igesCreator1.MaxThreeDMdlSpace = 10000.0;
    
    igesCreator1.ObjectTypes.Curves = true;
    
    igesCreator1.ObjectTypes.Surfaces = true;
    
    igesCreator1.ObjectTypes.Annotations = true;
    
    igesCreator1.ObjectTypes.Structures = true;
    
    igesCreator1.ObjectTypes.Solids = true;
    
    igesCreator1.SettingsFile = "C:\\Program Files\\Siemens\\NX 10.0\\iges\\igesexport.def";
    
    igesCreator1.MaxLineThickness = 2.0;
    
    igesCreator1.SysDefmaxThreeDMdlSpace = true;
    
    igesCreator1.SysDefidenticalPointResolution = true;
    
    igesCreator1.InputFile = "J:\\ME 578 Systems\\Midterm\\GoldBeam.prt";
    
    igesCreator1.OutputFile = "C:\\Users\\tyguy147\\AppData\\Local\\Temp\\GoldBeam.igs";
    
    theSession.SetUndoMarkName(markId1, "Export to IGES Options Dialog");
    
    igesCreator1.OutputFile = "J:\\ME 578 Systems\\Midterm\\Tests\\GoldBeam.igs";
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Export to IGES Options");
    
    theSession.DeleteUndoMark(markId2, null);
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Export to IGES Options");
    
    igesCreator1.FileSaveFlag = false;
    
    igesCreator1.LayerMask = "1-256";
    
    igesCreator1.DrawingList = "";
    
    NXOpen.TaggedObject[] objects1 = new NXOpen.TaggedObject[0];
    igesCreator1.SetDrawingArray(objects1);
    
    igesCreator1.ViewList = "Top,Front,Right,Back,Bottom,Left,Isometric,Trimetric,User Defined";
    
    NXOpen.NXObject nXObject1;
    nXObject1 = igesCreator1.Commit();
    
    theSession.DeleteUndoMark(markId3, null);
    
    theSession.SetUndoMarkName(markId1, "Export to IGES Options");
    
    igesCreator1.Destroy();
    
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
