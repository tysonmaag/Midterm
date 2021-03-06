// NX 10.0.0.24
// Journal created by tyguy147 on Sun Apr 01 19:30:46 2018 Mountain Daylight Time
//
using System;
using NXOpen;

public class NXJournal
{
  public static void Main(string[] args)
  {
    NXOpen.Session theSession = NXOpen.Session.GetSession();
    // ----------------------------------------------
    //   Menu: File->New...
    // ----------------------------------------------
	
	//Variable Declaration
	double a = double.Parse(args[0]);
	double G = 1.618;
	double b = G*a;
	double L = 10;
	double t = double.Parse(args[1]);
	
	
    NXOpen.FileNew fileNew1;
    fileNew1 = theSession.Parts.FileNew();
    
    fileNew1.TemplateFileName = "model-plain-1-mm-template.prt";
    
    fileNew1.UseBlankTemplate = false;
    
    fileNew1.ApplicationName = "ModelTemplate";
    
    fileNew1.Units = NXOpen.Part.Units.Millimeters;
    
    fileNew1.RelationType = "";
    
    fileNew1.UsesMasterModel = "No";
    
    fileNew1.TemplateType = NXOpen.FileNewTemplateType.Item;
    
    fileNew1.TemplatePresentationName = "Model";
    
    fileNew1.ItemType = "";
    
    fileNew1.Specialization = "";
    
    fileNew1.SetCanCreateAltrep(false);
    
    fileNew1.NewFileName = "J:\\ME 578 Systems\\Midterm\\MidtermProject\\GoldBeam.prt";
    
    fileNew1.MasterFileName = "";
    
    fileNew1.MakeDisplayedPart = true;
    
    NXOpen.NXObject nXObject1;
    nXObject1 = fileNew1.Commit();
    
    NXOpen.Part workPart = theSession.Parts.Work;
    NXOpen.Part displayPart = theSession.Parts.Display;

    fileNew1.Destroy();
    
    theSession.ApplicationSwitchImmediate("UG_APP_MODELING");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch...
    // ----------------------------------------------
    
    NXOpen.Sketch nullNXOpen_Sketch = null;
    NXOpen.SketchInPlaceBuilder sketchInPlaceBuilder1;
    sketchInPlaceBuilder1 = workPart.Sketches.CreateNewSketchInPlaceBuilder(nullNXOpen_Sketch);
    
    NXOpen.Unit unit1 = (NXOpen.Unit)workPart.UnitCollection.FindObject("MilliMeter");
    NXOpen.Expression expression1;
    expression1 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression2;
    expression2 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.SketchAlongPathBuilder sketchAlongPathBuilder1;
    sketchAlongPathBuilder1 = workPart.Sketches.CreateSketchAlongPathBuilder(nullNXOpen_Sketch);
    
    sketchAlongPathBuilder1.PlaneLocation.Expression.RightHandSide = "0";

    NXOpen.DatumPlane datumPlane1 = (NXOpen.DatumPlane)workPart.Datums.FindObject("DATUM_CSYS(0) YZ plane");
    NXOpen.Point3d point1 = new NXOpen.Point3d(0,0,0);
    sketchInPlaceBuilder1.PlaneOrFace.SetValue(datumPlane1, workPart.ModelingViews.WorkView, point1);
    
    NXOpen.Features.DatumCsys datumCsys1 = (NXOpen.Features.DatumCsys)workPart.Features.FindObject("DATUM_CSYS(0)");
    NXOpen.Point point2 = (NXOpen.Point)datumCsys1.FindObject("POINT 1");
    sketchInPlaceBuilder1.SketchOrigin = point2;
    
    sketchInPlaceBuilder1.PlaneOrFace.Value = null;
    
    sketchInPlaceBuilder1.PlaneOrFace.Value = datumPlane1;
    
    NXOpen.DatumAxis datumAxis1 = (NXOpen.DatumAxis)workPart.Datums.FindObject("DATUM_CSYS(0) X axis");
    sketchInPlaceBuilder1.Axis.Value = datumAxis1;
    
    theSession.Preferences.Sketch.CreateInferredConstraints = true;
    
    theSession.Preferences.Sketch.ContinuousAutoDimensioning = true;
    
    theSession.Preferences.Sketch.DimensionLabel = NXOpen.Preferences.SketchPreferences.DimensionLabelType.Expression;
    
    theSession.Preferences.Sketch.TextSizeFixed = true;
    
    theSession.Preferences.Sketch.FixedTextSize = 3.0;
    
    theSession.Preferences.Sketch.ConstraintSymbolSize = 3.0;
    
    theSession.Preferences.Sketch.DisplayObjectColor = false;
    
    theSession.Preferences.Sketch.DisplayObjectName = true;
    
    NXOpen.NXObject nXObject2;
    nXObject2 = sketchInPlaceBuilder1.Commit();
    
    NXOpen.Sketch sketch1 = (NXOpen.Sketch)nXObject2;
    NXOpen.Features.Feature feature1;
    feature1 = sketch1.Feature;
    
    sketch1.Activate(NXOpen.Sketch.ViewReorient.True);
      
    sketchInPlaceBuilder1.Destroy();
    
    sketchAlongPathBuilder1.Destroy();
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression2);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression1);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Line...
    // ----------------------------------------------
    NXOpen.Point3d startPoint1 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Point3d endPoint1 = new NXOpen.Point3d(0, b, 0.0);
    NXOpen.Line line1;
    line1 = workPart.Curves.CreateLine(startPoint1, endPoint1);
    
    theSession.ActiveSketch.AddGeometry(line1, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    //
	
    NXOpen.Point3d startPoint2 = new NXOpen.Point3d(0.0, b, 0.0);
    NXOpen.Point3d endPoint2 = new NXOpen.Point3d(L, a, 0.0);
    NXOpen.Line line2;
    line2 = workPart.Curves.CreateLine(startPoint2, endPoint2);
    
    theSession.ActiveSketch.AddGeometry(line2, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
   
    NXOpen.Point3d startPoint3 = new NXOpen.Point3d(L, a, 0.0);
    NXOpen.Point3d endPoint3 = new NXOpen.Point3d(L, 0.0, 0.0);
    NXOpen.Line line3;
    line3 = workPart.Curves.CreateLine(startPoint3, endPoint3);
    
    theSession.ActiveSketch.AddGeometry(line3, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
      
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Point3d startPoint4 = new NXOpen.Point3d(L, 0.0, 0.0);
    NXOpen.Point3d endPoint4 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Line line4;
    line4 = workPart.Curves.CreateLine(startPoint4, endPoint4);
    
    theSession.ActiveSketch.AddGeometry(line4, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: File->Finish Sketch
    // ----------------------------------------------
    theSession.ActiveSketch.Deactivate(NXOpen.Sketch.ViewReorient.False, NXOpen.Sketch.UpdateLevel.Model);
    
    // ----------------------------------------------
    //   Menu: Insert->Design Feature->Extrude...
    // ----------------------------------------------
    
    NXOpen.Features.Feature nullNXOpen_Features_Feature = null;
    
    if ( !workPart.Preferences.Modeling.GetHistoryMode() )
    {
        throw new Exception("Create or edit of a Feature was recorded in History Mode but playback is in History-Free Mode.");
    }
    
    NXOpen.Features.ExtrudeBuilder extrudeBuilder1;
    extrudeBuilder1 = workPart.Features.CreateExtrudeBuilder(nullNXOpen_Features_Feature);
    
    NXOpen.Section section1;
    section1 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5);
    
    extrudeBuilder1.Section = section1;
    
    extrudeBuilder1.AllowSelfIntersectingSection(true);
    
    NXOpen.Unit unit2;
    unit2 = extrudeBuilder1.Draft.FrontDraftAngle.Units;
    
    NXOpen.Expression expression7;
    expression7 = workPart.Expressions.CreateSystemExpressionWithUnits("2.00", unit2);
    
    extrudeBuilder1.DistanceTolerance = 0.01;
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies1 = new NXOpen.Body[1];
    NXOpen.Body nullNXOpen_Body = null;
    targetBodies1[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies1);
    
    extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0";
    
    extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = t.ToString();
    
    extrudeBuilder1.Offset.StartOffset.RightHandSide = "0";
    
    extrudeBuilder1.Offset.EndOffset.RightHandSide = "5";
    
    
    NXOpen.GeometricUtilities.SmartVolumeProfileBuilder smartVolumeProfileBuilder1;
    smartVolumeProfileBuilder1 = extrudeBuilder1.SmartVolumeProfile;
    
    smartVolumeProfileBuilder1.OpenProfileSmartVolumeOption = false;
    
    smartVolumeProfileBuilder1.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci;
    
    section1.DistanceTolerance = 0.01;
    
    section1.ChainingTolerance = 0.0095;
    
    section1.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves);

    NXOpen.Features.Feature[] features1 = new NXOpen.Features.Feature[1];
    NXOpen.Features.SketchFeature sketchFeature1 = (NXOpen.Features.SketchFeature)feature1;
    features1[0] = sketchFeature1;
    NXOpen.CurveFeatureRule curveFeatureRule1;
    curveFeatureRule1 = workPart.ScRuleFactory.CreateRuleCurveFeature(features1);
    
    section1.AllowSelfIntersection(true);
    
    NXOpen.SelectionIntentRule[] rules1 = new NXOpen.SelectionIntentRule[1];
    rules1[0] = curveFeatureRule1;
    NXOpen.Point3d helpPoint1 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    section1.AddToSection(rules1, null, null, null, helpPoint1, NXOpen.Section.Mode.Create, false);
     
    NXOpen.Direction direction1;
    direction1 = workPart.Directions.CreateDirection(sketch1, NXOpen.Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    extrudeBuilder1.Direction = direction1;
    
    extrudeBuilder1.ParentFeatureInternal = false;
    
    NXOpen.Features.Feature feature2;
    feature2 = extrudeBuilder1.CommitFeature();
    
    NXOpen.Expression expression8 = extrudeBuilder1.Limits.StartExtend.Value;
    NXOpen.Expression expression9 = extrudeBuilder1.Limits.EndExtend.Value;
    extrudeBuilder1.Destroy();
    
    workPart.Expressions.Delete(expression7);
    
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------
	    // ----------------------------------------------
    //   Menu: File->Export->IGES...
    // ----------------------------------------------
    
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
    
    igesCreator1.InputFile = "J:\\ME 578 Systems\\Midterm\\MidtermProject\\GoldBeam.igs";
    
    igesCreator1.OutputFile = "J:\\ME 578 Systems\\Midterm\\MidtermProject\\GoldBeam.igs";
    
    igesCreator1.FileSaveFlag = false;
    
    igesCreator1.LayerMask = "1-256";
    
    igesCreator1.DrawingList = "";
    
    NXOpen.TaggedObject[] objects1 = new NXOpen.TaggedObject[0];
    igesCreator1.SetDrawingArray(objects1);
    
    NXOpen.NXObject nXObject20;
    nXObject20 = igesCreator1.Commit();
    
    igesCreator1.Destroy();
    
	while(!System.IO.File.Exists("J:\\ME 578 Systems\\Midterm\\MidtermProject\\GoldBeam.igs"))
	{
		System.Threading.Thread.Sleep(10);
	}
	
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
