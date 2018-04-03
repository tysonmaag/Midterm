// NX 10.0.0.24
// Journal created by tyguy147 on Wed Mar 21 09:22:44 2018 Mountain Daylight Time
//Master Journal File

using System;
using System.Linq;
using NXOpen;

public class NXJournal
{
    private Session theSession;
    private Part workPart;

    //public static void Main(string[] args)
    //{
    //    NXJournal journal = new NXJournal();
    //    journal.CreateBracket(0);
    //}

    public NXJournal()
    {
        theSession = NXOpen.Session.GetSession();
        workPart = theSession.Parts.Work;
    }


  public void CreateBracket(double thickness, double length, double height, double holeradius, double holedistance)
  {
    NXOpen.Point3d point1 = new NXOpen.Point3d(0, 0, 0);
    NXOpen.Point3d point2 = new NXOpen.Point3d(0, 0, height);
    NXOpen.Point3d point3 = new NXOpen.Point3d(0, thickness, height);
    NXOpen.Point3d point4 = new NXOpen.Point3d(0, thickness, thickness);
    NXOpen.Point3d point5 = new NXOpen.Point3d(0, length, thickness);
    NXOpen.Point3d point6 = new NXOpen.Point3d(0, length, 0);

    IBaseCurve[] curves1 = new IBaseCurve[6]
    {
        workPart.Curves.CreateLine(point1,point2),
        workPart.Curves.CreateLine(point2, point3),
        workPart.Curves.CreateLine(point3, point4),
        workPart.Curves.CreateLine(point4, point5),
        workPart.Curves.CreateLine(point5, point6),
        workPart.Curves.CreateLine(point6, point1)
    };

    CreateExtrude(curves1, true, 52.0, new Vector3d(1.0, 0, 0));

      //Left Line
    NXOpen.Point3d point7 = new NXOpen.Point3d(-21.0, 0, height-8.0);
    NXOpen.Point3d point8 = new NXOpen.Point3d(-21, 0, height-16.0);
      //Right LIne
    NXOpen.Point3d point9 = new NXOpen.Point3d(-15, 0, height - 16.0);
    NXOpen.Point3d point10 = new NXOpen.Point3d(-15, 0, height - 8.0);
      //Top Arc
    NXOpen.Point3d Center1 = new NXOpen.Point3d(-18, 0, height - 8.0);
    NXOpen.Vector3d dir1 = new NXOpen.Vector3d(-1.0, 0 , 0);
    NXOpen.Vector3d dir2 = new NXOpen.Vector3d(0, 0, -1.0);
      //Bottom Arc
    NXOpen.Point3d Center2 = new NXOpen.Point3d(-18, 0, height - 16.0);
    
   IBaseCurve[] curves2 = new IBaseCurve[4]
    {
        workPart.Curves.CreateLine(point7,point8),
        workPart.Curves.CreateArc(Center2,dir1,dir2,3.0,0,Math.PI),
        workPart.Curves.CreateLine(point9, point10),
        workPart.Curves.CreateArc(Center1,dir1,dir2,3.0,Math.PI,2*Math.PI)
    };

      //Right Side
   //Left Line
   NXOpen.Point3d point11 = new NXOpen.Point3d(21.0, 0, height - 8.0);
   NXOpen.Point3d point12 = new NXOpen.Point3d(21, 0, height - 16.0);
   //Right LIne
   NXOpen.Point3d point13 = new NXOpen.Point3d(15, 0, height - 16.0);
   NXOpen.Point3d point14 = new NXOpen.Point3d(15, 0, height - 8.0);
   //Top Arc
   NXOpen.Point3d Center3 = new NXOpen.Point3d(18, 0, height - 8.0);
   //Bottom Arc
   NXOpen.Point3d Center4 = new NXOpen.Point3d(18, 0, height - 16.0);

   IBaseCurve[] curves3 = new IBaseCurve[4]
    {
        workPart.Curves.CreateLine(point11,point12),
        workPart.Curves.CreateArc(Center4,dir1,dir2,3.0,0,Math.PI),
        workPart.Curves.CreateLine(point13, point14),
        workPart.Curves.CreateArc(Center3,dir1,dir2,3.0,Math.PI,2*Math.PI)
    };

   CreateExtrude(curves2, false, 2.0 * thickness, new Vector3d(0, 1, 0));
   CreateExtrude(curves3, false, 2.0 * thickness, new Vector3d(0, 1, 0));

      //EdgeBlend Start
      NXOpen.Features.EdgeBlendBuilder edgeBlendBuilder1 = workPart.Features.CreateEdgeBlendBuilder(null);

      NXOpen.ScCollector scCollector1 = workPart.ScCollectors.CreateCollector();

      NXOpen.Edge[] seedEdges1 = new NXOpen.Edge[2];
      NXOpen.Features.Extrude extrude1 = (NXOpen.Features.Extrude)workPart.Features.ToArray().OfType<NXOpen.Features.Extrude>().First();
      Edge[] edges = extrude1.GetBodies()[0].GetEdges();
      NXOpen.Point3d edgeendpoint1 = new NXOpen.Point3d(26, 0, height);
      NXOpen.Point3d edgeendpoint2 = new NXOpen.Point3d(26, thickness, height);
      NXOpen.Point3d edgeendpoint3 = new NXOpen.Point3d(-26, 0, height);
      NXOpen.Point3d edgeendpoint4 = new NXOpen.Point3d(-26, thickness, height);
      Edge edge1 = null;
      Edge edge2 = null;
      for (int i=0; i < edges.Length; i++)
      {
        NXOpen.Point3d endpoint;
        NXOpen.Point3d endpoint2;
        edges[i].GetVertices(out endpoint, out endpoint2);

        if (Math.Abs(edgeendpoint1.X - endpoint.X) < .01 && Math.Abs(edgeendpoint1.Y - endpoint.Y) < .01 && Math.Abs(edgeendpoint1.Z - endpoint.Z) < .01 && Math.Abs(edgeendpoint2.X - endpoint2.X) < .01 && Math.Abs(edgeendpoint2.Y - endpoint2.Y) < .01 && Math.Abs(edgeendpoint2.Z - endpoint2.Z) < .01)
        {
            edge1 = edges[i];
        }
        else if (Math.Abs(edgeendpoint3.X - endpoint.X) < .01 && Math.Abs(edgeendpoint3.Y - endpoint.Y) < .01 && Math.Abs(edgeendpoint3.Z - endpoint.Z) < .01 && Math.Abs(edgeendpoint4.X - endpoint2.X) < .01 && Math.Abs(edgeendpoint4.Y - endpoint2.Y) < .01 && Math.Abs(edgeendpoint4.Z - endpoint2.Z) < .01)
        {
            edge2 = edges[i];
        }

          if(edge1 != null && edge2 != null)
          {
              break;
          }

      }

      seedEdges1[0] = edge1;
      seedEdges1[1] = edge2;

      NXOpen.EdgeMultipleSeedTangentRule edgeMultipleSeedTangentRule1;
      edgeMultipleSeedTangentRule1 = workPart.ScRuleFactory.CreateRuleEdgeMultipleSeedTangent(seedEdges1, 0.5, true);

      NXOpen.SelectionIntentRule[] rules1 = new NXOpen.SelectionIntentRule[1];
      rules1[0] = edgeMultipleSeedTangentRule1;
      scCollector1.ReplaceRules(rules1, false);

      int csIndex1;
      csIndex1 = edgeBlendBuilder1.AddChainset(scCollector1, "8");

      edgeBlendBuilder1.CommitFeature();
      edgeBlendBuilder1.Destroy();

      //EdgeBlend End

      //Circle's Extrude
      NXOpen.Point3d holecenter1 = new NXOpen.Point3d(holedistance/2, 20, 10);
      NXOpen.Point3d holecenter2 = new NXOpen.Point3d(-holedistance/2, 20, 10);
      NXOpen.Vector3d dir3 = new NXOpen.Vector3d(0, -1.0, 0);
      NXOpen.Vector3d dir4 = new NXOpen.Vector3d(1, 0, 0);

      IBaseCurve[] hole1 = new IBaseCurve[1]
    {
        workPart.Curves.CreateArc(holecenter1,dir3,dir4,holeradius,0,2*Math.PI),
    };

      IBaseCurve[] hole2 = new IBaseCurve[1]
    {
        workPart.Curves.CreateArc(holecenter2,dir3,dir4,holeradius,0,2*Math.PI),
    };

      CreateExtrude(hole1,false,2*thickness,new Vector3d(0,0,1));
      CreateExtrude(hole2, false, 2*thickness, new Vector3d(0, 0, 1));

      //Big Slot

      //Top Line
      NXOpen.Point3d slotpt1 = new NXOpen.Point3d(-6, 44, 0);
      NXOpen.Point3d slotpt2 = new NXOpen.Point3d(-6, length-14.0, 0);
      //BOttom Line
      NXOpen.Point3d slotpt4 = new NXOpen.Point3d(6, 44, 0);
      NXOpen.Point3d slotpt3 = new NXOpen.Point3d(6, length - 14.0, 0);
      //Right Arc
      NXOpen.Point3d slotcenter1 = new NXOpen.Point3d(0, 44, 0);
      //Left Arc
      NXOpen.Point3d slotcenter2 = new NXOpen.Point3d(0, length - 14.0, 0);

      IBaseCurve[] slotcurve = new IBaseCurve[4]
    {
        workPart.Curves.CreateLine(slotpt1,slotpt2),
        workPart.Curves.CreateArc(slotcenter2,dir3,dir4,6.0,Math.PI/2,3*Math.PI/2),
        workPart.Curves.CreateLine(slotpt3, slotpt4),
        workPart.Curves.CreateArc(slotcenter1,dir3,dir4,6.0,-Math.PI/2,Math.PI/2)
    };

      CreateExtrude(slotcurve, false, 2 * thickness, new Vector3d(0, 0, 1));

      //Curvey Part

      //bottom Arc
      NXOpen.Point3d botarc = new NXOpen.Point3d(26, 44, 0);
      //bottom line
      NXOpen.Point3d botpt1 = new NXOpen.Point3d(14, 44, 0);
      NXOpen.Point3d botpt2 = new NXOpen.Point3d(14, length - 14.0, 0);
      //Big Arc
      NXOpen.Point3d sidearc = new NXOpen.Point3d(0, length - 14.0, 0);
      //Top Line
      NXOpen.Point3d toppt2 = new NXOpen.Point3d(-14, 44, 0);
      NXOpen.Point3d toppt1 = new NXOpen.Point3d(-14, length - 14.0, 0);
      //Right Arc
      NXOpen.Point3d toparc = new NXOpen.Point3d(-26, 44, 0);

      //OUtline
      NXOpen.Point3d out1 = new NXOpen.Point3d(-26, 32, 0);
      NXOpen.Point3d out2 = new NXOpen.Point3d(-28, 32, 0);
      NXOpen.Point3d out3 = new NXOpen.Point3d(-28, length + 10, 0);
      NXOpen.Point3d out4 = new NXOpen.Point3d(28, length +10, 0);
      NXOpen.Point3d out5 = new NXOpen.Point3d(28, 32, 0);
      NXOpen.Point3d out6 = new NXOpen.Point3d(26, 32, 0);


      IBaseCurve[] bigcutcurve = new IBaseCurve[10]
    {
        workPart.Curves.CreateArc(botarc,dir3,dir4,12,-Math.PI/2,0),
        workPart.Curves.CreateLine(botpt1, botpt2),
        workPart.Curves.CreateArc(sidearc,dir3,dir4,14,Math.PI/2,3*Math.PI/2),
        workPart.Curves.CreateLine(toppt1, toppt2),
        workPart.Curves.CreateArc(toparc,dir3,dir4,12,0,Math.PI/2),
        workPart.Curves.CreateLine(out1, out2),
        workPart.Curves.CreateLine(out2, out3),
        workPart.Curves.CreateLine(out3, out4),
        workPart.Curves.CreateLine(out4, out5),
        workPart.Curves.CreateLine(out5, out6)
    };

      CreateExtrude(bigcutcurve, false, 2 * thickness, new Vector3d(0, 0, 1));


  }

  private void CreateExtrude(IBaseCurve[] curves, bool add, double distance, Vector3d vector)
  {
      SelectionIntentRule[] rules = new SelectionIntentRule[1]
      {
          workPart.ScRuleFactory.CreateRuleBaseCurveDumb(curves)
      };

      NXOpen.Features.ExtrudeBuilder extrudeBuilder1 = workPart.Features.CreateExtrudeBuilder(null);
    NXOpen.Section section1 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5);
    section1.AddToSection(rules, (Curve)curves[0], null, null, new Point3d(0, 0, 0), Section.Mode.Create, false);
    extrudeBuilder1.Section = section1;
    
      if (add)
      {
          extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
      }
      else
      {
          extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Subtract;

          extrudeBuilder1.BooleanOperation.SetTargetBodies(new Body[1] { workPart.Bodies.ToArray()[0] });
      }
    distance = distance/2.0;
    extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "-" + distance.ToString();
    extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = distance.ToString();

    NXOpen.Point3d origin1 = new NXOpen.Point3d(0.0, 0, 0);
    extrudeBuilder1.Direction = workPart.Directions.CreateDirection(origin1, vector, NXOpen.SmartObject.UpdateOption.WithinModeling);

   extrudeBuilder1.CommitFeature();

   extrudeBuilder1.Destroy();
    
  }
}