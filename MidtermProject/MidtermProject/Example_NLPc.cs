using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

/***********************************************************************/
/*
//     This is an example call of MIDACO 6.0
//     -------------------------------------
//
//     MIDACO solves Multi-Objective Mixed-Integer Non-Linear Problems:
//
//
//      Minimize     F_1(X),... F_O(X)  where X(1,...N-NI)   is CONTINUOUS
//                                      and   X(N-NI+1,...N) is DISCRETE
//
//      subject to   G_j(X)  =  0   (j=1,...ME)      equality constraints
//                   G_j(X) >=  0   (j=ME+1,...M)  inequality constraints
//
//      and bounds   XL <= X <= XU
//
//
//     The problem statement of this example is given below. You can use 
//     this example as template to run your own problem. To do so: Replace 
//     the objective functions 'F' (and in case the constraints 'G') given 
//     here with your own problem and follow the below instruction steps.
*/
/***********************************************************************/
/*********************   OPTIMIZATION PROBLEM   ************************/
/***********************************************************************/  
class Optimizationproblem {
    
    public static void blackbox( double[] f, double[] g, double[] x ) 
    {
        /* Objective functions F(X) */
        System.IO.StreamReader maxstress = new System.IO.StreamReader(@"J:\ME 578 Systems\Midterm\MidtermProject\maxstress.txt");
        string stress = maxstress.ReadLine();
        double stressnum = double.Parse(stress);

        double a = x[0];
        double thick = x[1];

        double G = 1.618;
        double b = G*a;

        /* Objective functions F(X) */
        double volume = 0;

        volume = a*thick*10+10*.5*(b-a);
        f[0] = -volume;

        /* Equality constraints G(X) = 0 MUST COME FIRST in g[0:me-1] */

        /* Inequality constraints G(X) >= 0 MUST COME SECOND in g[me:m-1] */
        g[0] = 2000000000 - stressnum;
    }
} 
/***********************************************************************/
/************************   MAIN PROGRAM   *****************************/
/***********************************************************************/
class Example {

  public static void Main() {

      long i,n; double[] x,xl,xu;
      Dictionary<string, long> problem = new Dictionary<string, long>();
      Dictionary<string, long> option  = new Dictionary<string, long>();
      Dictionary<string, double> parameter  = new Dictionary<string, double>();
      Dictionary<string, double[]> solution   = new Dictionary<string, double[]>();
      string key = "MIDACO_LIMITED_VERSION___[CREATIVE_COMMONS_BY-NC-ND_LICENSE]";

      /*****************************************************************/
      /***  Step 1: Problem definition  ********************************/
      /*****************************************************************/

      /* STEP 1.A: Problem dimensions
      ******************************/
      problem["o"]  = 1; /* Number of objectives                          */
      problem["n"]  = 2; /* Number of variables (in total)                */
      problem["ni"] = 0; /* Number of integer variables (0 <= ni <= n)    */
      problem["m"]  = 1; /* Number of constraints (in total)              */
      problem["me"] = 0; /* Number of equality constraints (0 <= me <= m) */

      /* STEP 1.B: Lower and upper bounds 'xl' & 'xu'  
      **********************************************/ 
      n = problem["n"]; 
      xl = new double[n]; 
      xu = new double[n];
      
      xl[0] = 0.1;
      xu[0] = 5.0;

      xl[1] = 0.1;
      xu[1] = 5.0;


      /* STEP 1.C: Starting point 'x'  
      ******************************/          
      x  = new double[n]; 
      x[0] = 1.0; /* Here for example: starting point = lower bounds */
      x[1] = 1.0;

      ProcessStartInfo info = new ProcessStartInfo
      {
          Arguments = "\"J:\\ME 578 Systems\\Midterm\\MidtermProject\\NXJournalPortion\\bin\\Debug\\NXJournalPortion.exe\" " + x[0].ToString() + " " + x[1].ToString(),
          CreateNoWindow = true,
          ErrorDialog = true,
          FileName = "\"C:\\Program Files\\Siemens\\NX 10.0\\UGII\\run_managed.exe\"",
          RedirectStandardError = true,
          RedirectStandardOutput = true,
          UseShellExecute = false
      };

      //Unsure how to execute a batch file from Windows. John helped me on the last lab with the code above, but now I have no clue what it is doing :(

      Console.WriteLine("C:\\Program Files\\ANSYS Inc\\V181\\ansys\\bin\\winx64\\ansys181 -b -i J:\\ME 578 Systems\\Midterm\\MidtermProject\\Solution.txt -o outputfile.txt");



      /*****************************************************************/
      /***  Step 2: Choose stopping criteria and printing options   ****/
      /*****************************************************************/
                  
      /* STEP 2.A: Stopping criteria 
      *****************************/
      option["maxeval"] = 10000;    /* Maximum number of function evaluation (e.g. 1000000)  */
      option["maxtime"] = 60*60*24; /* Maximum time limit in Seconds (e.g. 1 Day = 60*60*24) */

      /* STEP 2.B: Printing options  
      ****************************/
      option["printeval"] = 1000; /* Print-Frequency for current best solution (e.g. 1000) */
      option["save2file"] = 1;    /* Save SCREEN and SOLUTION to TXT-files [ 0=NO/ 1=YES]  */          

      /*****************************************************************/
      /***  Step 3: Choose MIDACO parameters (FOR ADVANCED USERS)    ***/
      /*****************************************************************/

      parameter["param1"]  = 0.0; /* ACCURACY  */
      parameter["param2"]  = 0.0; /* SEED      */
      parameter["param3"]  = 0.0; /* FSTOP     */
      parameter["param4"]  = 0.0; /* ALGOSTOP  */
      parameter["param5"]  = 0.0; /* EVALSTOP  */
      parameter["param6"]  = 0.0; /* FOCUS     */
      parameter["param7"]  = 0.0; /* ANTS      */
      parameter["param8"]  = 0.0; /* KERNEL    */
      parameter["param9"]  = 0.0; /* ORACLE    */
      parameter["param10"] = 0.0; /* PARETOMAX */
      parameter["param11"] = 0.0; /* EPSILON   */
      parameter["param12"] = 0.0; /* BALANCE   */
      parameter["param13"] = 0.0; /* CHARACTER */   
 
      /*****************************************************************/
      /***  Step 4: Choose Parallelization Factor    *******************/
      /*****************************************************************/  

      option ["parallel"] = 0; /* Serial: 0 or 1, Parallel: 2,3,4,... */

      /*****************************************************************/
      /***********************  Run MIDACO  ****************************/
      /*****************************************************************/

      solution = Midaco.run ( problem, x,xl,xu, option, parameter, key );

      /* Print solution return arguments from MIDACO to console */
      
      double[] f,g;
      Console.WriteLine(" ");
      f = solution["f"]; Console.WriteLine("solution f[0]: " + f[0]);
      g = solution["g"]; Console.WriteLine("solution g[0]: " + g[0]);
      x = solution["x"]; Console.WriteLine("solution x[0]: " + x[0] + "Solution to x[1]: " + x[1]);
      Console.ReadKey(); /* pause */

      /*****************************************************************/
      /***********************  END OF MAIN  ***************************/
      /*****************************************************************/

      ProcessStartInfo info2 = new ProcessStartInfo
      {
          Arguments = "\"J:\\ME 578 Systems\\Midterm\\MidtermProject\\NXJournalPortion\\bin\\Debug\\NXJournalPortion.exe\" " + x[0].ToString() + " " + x[1].ToString(),
          CreateNoWindow = true,
          ErrorDialog = true,
          FileName = "\"C:\\Program Files\\Siemens\\NX 10.0\\UGII\\run_managed.exe\"",
          RedirectStandardError = true,
          RedirectStandardOutput = true,
          UseShellExecute = false
      };
  }
}

