using System;
using MatrixSpace;
namespace MLFA
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string m = "";
			StringEnumerator a = new StringEnumerator(m);
			while (m!="end"){
				m = Console.ReadLine();
				if(m!="end")
				{  
					a.setString(m);
					//a.Process ();
					ProcessWithErrorHandling (a); 
					if(a.AllReadyExistAndPrinted()==false)
						a.PrintExpression();
				}
			}
			a.printAllExpressions ();
		}
		public static void ProcessWithErrorHandling(StringEnumerator sn)
		{
			try{
				sn.StringProcess();
			}
			catch{
				Expression error = new Expression ();
				error.setErrorMessage ("I could not understand the inputs you gave me. Check if you formate was correct.");
				sn.addExpression (error);
			}
		}

	}
}


