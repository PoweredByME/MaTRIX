using System;
using System.Collections.Generic;
using MatrixSpace;

namespace EquationSolvingSpace
{
	public class DMAS_Solver
	{
		private List<Expression> theExpressionList = new List<Expression>();
		private string expression = "";
		private Expression solution = new Expression();
		public DMAS_Solver(){}
		public DMAS_Solver(List<Expression> tel, string ex)
		{
			foreach (Expression x in tel) {
				Expression x1 = new Expression (x);
				theExpressionList.Add (x1);
			}
			expression = ex;
			SimpleSolver sol = new SimpleSolver (theExpressionList, expression);
			solution  = sol.Solve ();

		}
	    
		public Expression getSolution()
		{
			return solution;
		}

		private Expression getExpression(string tag)
		{
			Expression dumy = new Expression ();
			foreach (Expression x in theExpressionList) {

				if (x.getTag () == tag)
				{
					dumy = x;
					break;
				}
			}
			return dumy;
		}
			
	}   //end class DMAS_SOLVER

	class SimpleSolver
	{
		private List<Expression> theExpressionList = new List<Expression>();
		private string expression = "";
		private List<string> eBatch = new List<string>();
		public SimpleSolver(){}
		public List<string> geteBatch (){
			return eBatch;}

		public SimpleSolver(List<Expression> tel, string ex)
		{
			foreach (Expression i in tel) {
				Expression n = new Expression (i);
				theExpressionList.Add (n);
			}
			expression = ex;
		}

	     public Expression Solve()
		{
			Expression sol = new Expression ();
			if (eBatchMaker ()) {
				if (allPresent ()) {
				
					if (DMAS_Division ()) {
						if (DMAS_Multpilication ()) {
							if (DMAS_Addition ()) {
								sol = getExpression (eBatch [0].TrimStart (new char[]{ '-' }));
							} else {
								Expression x = new Expression ();
								x.setErrorMessage ("Invalid operation.");
								sol = x;
							}
						} else {
							Expression x = new Expression ();
							x.setErrorMessage ("Invalid operation.");
							sol = x;
						}


					} else {
						Expression x = new Expression ();
						x.setErrorMessage ("Invalid Operation.");
						sol = x;
					}
				} else {
					sol.setErrorMessage ("The variables in the expression are undefined or the operations you are trying top perfor are in valid");
				}
			} else {
				sol.setErrorMessage ("Invalid operation.");
				Console.WriteLine ("The matrix was not defined in apropriate manner. Please ammend this to proceed.");
			}
			eBatch.RemoveRange (0, eBatch.Count);
			return sol;
		}

		private bool allPresent()
		{
			bool found = true;
			foreach (string x in eBatch) {
				string a= x.Trim ();
				if (isNumeric (a.TrimStart(new char[] {'-'}))==false) {
					if ((a == "+" || a == "-" || a == "*" || a == "/") == false) {
						if (SearchExpressionWithTag (a.TrimStart (new char[]{ '-' })) == false) {
							found = false;
							break;
						}
					}
				}
			}
			return found;
		}

		private bool SearchExpressionWithTag(string tag)
		{
			bool found = false;
			foreach (Expression x in theExpressionList) {
				if (x.getTag () == tag) {
					found = true;
					break;
				}
			}
			return found;
		}

		private void functionForDebugging()
		{
			foreach (Expression x in theExpressionList) {
				Console.WriteLine ("ex = " + x.getTag ());
			}
		}

		private Expression getExpression(string tag)
		{
			Expression dumy = new Expression ();
			foreach (Expression x in theExpressionList) {
				
				if (x.getTag () == tag)
				{
					dumy = x;
					break;
				}
			}
			return dumy;
		}


		private bool eBatchMaker()
		{
			bool proceed = true;
			string dumy = "";
			foreach (char x in expression) {
				if (x == '+' || x == '-' || x == '/' || x == '*') {
					if (!string.IsNullOrWhiteSpace (dumy)) {
						dumy = dumy.Trim ();
						eBatch.Add (dumy);
						dumy = "";
					}
					eBatch.Add (x.ToString ());
				} else
					dumy += x.ToString ();
			}
			if (!string.IsNullOrWhiteSpace (dumy)) {
				dumy.Trim ();
				eBatch.Add (dumy);
			}
				if (eBatchMatrixManager ()) {
				eBatchUnnamedNumberManager ();
				bool h = eBatchManager ();
				bool h1 = eBatchMinusManager ();
				if (h || h1) {
					PrintAbles.PrintError ("Attention!!! There were some operator anomalies. I have sorted them out according to my understanding.");
				}
			} else
				 proceed = false;
			return proceed;
		}

		private bool eBatchManager()
		{
			bool occured = false;
			if (eBatch [0] == "-") {
				eBatch [1] = "-" + eBatch [1];
				eBatch.RemoveAt (0);
			}
			List<int> p = new List<int> ();
			for(int c=0; c < eBatch.Count ; c++){
				string x = eBatch [c];
				if (x == "-") {
					if (eBatch [c - 1] == "(" || eBatch [c- 1] == "+" || eBatch [c - 1] == "*"
					   || eBatch [c - 1] == "/") {
						eBatch [c+1] = "-" + eBatch [c + 1];
							p.Add(c);
						occured = true;
					}
				}
			}
			for (int c = p.Count-1; c >= 0; c--) {
				int e = p [c];
				eBatch.RemoveAt (e);
			}
			return occured;
		}

		private void eBatchUnnamedNumberManager()
		{
			for(int c = 0; c < eBatch.Count ; c++) {
				string x = eBatch[c];
				if (isNumeric (x)) {
					Number n = new Number ();
					n.setTag ("Nnew_restricted_ab_oxfff" + SAcounter);
					n.setNumber (double.Parse (x));
					Expression n1 = new Expression ();
					n1.setNumberExpression (n);
					theExpressionList.Add (n1);
					eBatch [c] = "Nnew_restricted_ab_oxfff" + SAcounter;
					SAcounter++;
				}
			}
		}

		private bool eBatchMinusManager()
		{
		bool occured = false;
			List<int> d = new List<int> ();
			for (int c = 0; c < eBatch.Count; c++) {
				string x = eBatch [c];
				if (x == "-") {
					string a = eBatch [c - 1];
					if (a == "-") {
						eBatch [c - 1] = "+";
						d.Add (c);
						occured = true;
					}
				}
			}
			for (int c = d.Count-1; c >= 0; c--) {
				int e = d [c];
				eBatch.RemoveAt (e);
			}
			for (int c = 0; c < eBatch.Count; c++) {
				string x = eBatch [c];
				if (x == "-") {
					eBatch[c] ="+";
					eBatch [c + 1] = "-" + eBatch [c + 1];
				}
			}
			return occured;
		}
			
		private int countOccurance(string exp, char c)
		{
			int count = 0;
			foreach (char x in exp) {
				if (x == c)
					count++;
			}
			return count;
		}

		private bool eBatchMatrixManager()
		{
			bool proceed = true;
			if (expression.Contains ("[")) {
				bool done = false;
				for (int c = 0; c < eBatch.Count; c++) {
					string x = new string (eBatch [c].ToCharArray ());
					if ((x.Contains ("[")  && x.Contains ("]"))|| x.Contains (";")) {
						if (countOccurance (x, '[') == 1 && countOccurance (x, ']') == 1) {
							int greatest = 0;
							int cols = 0;
							int rows = 0;
							string dumy = "";
							string myCount = "-0123456789.";
							List<string> temp = new List<string> ();
							foreach (char a in x) {
								if (ifContain (myCount, a.ToString ())) {
									dumy += a.ToString ();
								} 
								else if (a == ' '){
									if (!string.IsNullOrWhiteSpace (dumy)) {
										temp.Add (dumy);
										dumy = "";
									}
								}
								else if (a == ';') {
									if (!string.IsNullOrWhiteSpace (dumy)) {
										temp.Add (dumy);
										dumy = "";
										temp.Add (";");
									} else {
										dumy = "";
										temp.Add (";");
									}
								}
							}
							if (!string.IsNullOrWhiteSpace (dumy)) {
								temp.Add (dumy);
								dumy = "";
								
							}
							foreach (string b in temp) {
								if (b == ";") {
									if (cols > greatest)
										greatest = cols;
									cols = 0;
									rows++;
								} else
									cols++;
							}
							if (cols > greatest)
								greatest = cols;
							else if (greatest == 0)
								greatest = cols;
							if (greatest != cols && !done) {
								Console.WriteLine ("You left some undefined positions in the matrix. So, I have set them to zero.");
								done = true;
							}
							Matrix u = new Matrix (rows + 1, greatest);
							u.Zero ();
							cols = 0;
							rows = 0;
							foreach (string s in temp) {
								if (s == ";") {
									rows++;
									cols = 0;
								} else {
									u.setElement (double.Parse (s), rows + 1, cols + 1);
									cols++;
								}
							}
							u.setTag ("self_ASSigned_matoxdddd" + SAcounter);
							eBatch [c] = "self_ASSigned_matoxdddd" + SAcounter;
							SAcounter++;
							Expression nmat = new Expression ();
							nmat.setMatrix (u);
							theExpressionList.Add (nmat);
						} else {
							proceed = false;
							break;
						}
					}
				}
			}
			return proceed;
		}

		private bool ifContain(string myString, string toFind)
		{
			bool decision = false ;
			decision = myString.Contains (toFind);
			return decision;
		}
		private bool isNumeric(string exp)
		{
			int count = 0;
			string myCounting  = "0123456789.";
			foreach (char x in exp) {
				if (ifContain (myCounting, x.ToString ()))
					count++;
				else
					break;
			}
			if (count == exp.Length)
				return true;
			else
				return false;
		}

		private bool DMAS_Addition()   //function for addition.
		{
			
			bool okay = true;
			int c = 0;
			int size = eBatch.Count;
			while ((size>1)) {
				if (c == eBatch.Count)
					break;
				string x = eBatch [c].Trim();
				if (eBatch.Count <= 1)
					break;
				if (x == "+") {
					string left = eBatch [c - 1];
					string right = eBatch [c + 1];
					Expression lhs = getExpression (eBatch [c - 1].TrimStart(new char[]{'-'}));
					Expression rhs = getExpression (eBatch [c + 1].TrimStart(new char[]{'-'}));
					int lhsType = lhs.getExpressionType ();
					int rhsType = rhs.getExpressionType ();
					bool yeal = false;bool year = false;
					if (lhsType == rhsType) {
						if (lhsType == 1 && rhsType==1) {
							if (lhs.getMatrix ().getColumns () == rhs.getMatrix ().getColumns () && lhs.getMatrix ().getRows () == rhs.getMatrix ().getRows ()) {
								Matrix LHS = lhs.getMatrix ();
								Matrix RHS = rhs.getMatrix ();
								if (left.Contains ("-")) {
									LHS = LHS * (-1);
									eBatch [c - 1] = eBatch [c - 1].TrimStart (new char[]{'-'});
									yeal = true;
								}
								if (right.Contains ("-")) {
									RHS = RHS * (-1);
									eBatch [c + 1] = eBatch [c + 1].TrimStart (new char[]{'-'});
									year = true;
								}
								Matrix sum = LHS + RHS;
								if(yeal)
								LHS = LHS * (-1);
								if(year)
								RHS = RHS * (-1);
								sum.setTag (lhs.getTag ());
								theExpressionList [theExpressionList.IndexOf ((lhs))].setMatrix (sum);
								eBatch.RemoveAt (c+1);
								eBatch.RemoveAt (c);
							} else {
								PrintAbles.PrintError ("Invalid Operation");
								okay = false;
								break;
							}
						} else if (rhsType == 3 ||isNumeric(left.TrimStart (new char[]{'-'})) ||isNumeric(right.TrimStart (new char[]{'-'}))) {
							double LHS = 0, RHS = 0;
							if (isNumeric (left.TrimStart (new char[]{'-'}))) {
								 LHS = double.Parse (left.TrimStart (new char[]{ '-' }));
							}
							else 
							      LHS = lhs.getNumberExpression ().getNumber ();
							if (isNumeric (right.TrimStart (new char[]{'-'}))) {
								RHS = double.Parse (right.TrimStart (new char[]{ '-' }));
							}
							else
							     RHS = rhs.getNumberExpression ().getNumber ();
							if (left.Contains ("-")) {
								LHS = LHS * (-1);
								eBatch [c - 1] = eBatch [c - 1].TrimStart (new char[]{'-'});
							}
							if (right.Contains ("-")) {
								RHS = RHS * (-1);
								eBatch [c + 1] = eBatch [c + 1].TrimStart (new char[]{'-'});
							}
							double num = LHS + RHS;
							bool go =true;
							Number sum = new Number ();
							if (isNumeric (left.TrimStart (new char[]{ '-' })) && isNumeric (right.TrimStart (new char[]{ '-' }))) {
								sum.setTag (num.ToString ());
								eBatch [c - 1] = num.ToString ();
								go = false;
							} else if (isNumeric (left.TrimStart (new char[]{ '-' })) && go) {
								sum.setTag (num.ToString ());
								eBatch [c - 1] = num.ToString ();
							} else {
								sum.setTag (eBatch [c - 1]);
							}
							sum.setNumber (num);
							theExpressionList [theExpressionList.IndexOf ((lhs))].setNumberExpression (sum);
 							eBatch.RemoveAt (c+1);
							eBatch.RemoveAt (c);
						}  
					}

						c = 0;

					if(lhsType != rhsType) {
						okay = false;
						break;
					}
				} 

				else{
					size = eBatch.Count;
					c += 1;
				}


			}
			return okay;

		}     //end funciton of addition.

	    int SAcounter = 0;
	
		private bool DMAS_Multpilication()     //function for multiplication 
		{
				bool okay = true;
				int c = 0;
				int size = eBatch.Count;
				while ((size>1)) {
					if (c == eBatch.Count)
						break;
					string x = eBatch [c].Trim();
					if (eBatch.Count <= 1)
						break;
					if (x == "*") {
						string left = eBatch [c - 1];
						string right = eBatch [c + 1];
						Expression lhs = getExpression (eBatch [c - 1].TrimStart(new char[]{'-'}));
						Expression rhs = getExpression (eBatch [c + 1].TrimStart(new char[]{'-'}));
						int lhsType = lhs.getExpressionType ();
						int rhsType = rhs.getExpressionType ();
						bool yeal = false;bool year = false;
						if (lhsType == rhsType || lhsType!=rhsType) {
							if (lhsType == 1 && rhsType==1) {
								if (lhs.getMatrix ().getColumns () == rhs.getMatrix ().getRows ()) {
									Matrix LHS = lhs.getMatrix ();
									Matrix RHS = rhs.getMatrix ();
									if (left.Contains ("-")) {
										LHS = LHS * (-1);
										eBatch [c - 1] = eBatch [c - 1].TrimStart (new char[]{'-'});
										yeal = true;
									}
									if (right.Contains ("-")) {
										RHS = RHS * (-1);
										eBatch [c + 1] = eBatch [c + 1].TrimStart (new char[]{'-'});
										year = true;
									}
									Matrix sum = LHS * RHS;
									if(yeal)
										LHS = LHS * (-1);
									if(year)
										RHS = RHS * (-1);
									sum.setTag (lhs.getTag ());
									theExpressionList [theExpressionList.IndexOf ((lhs))].setMatrix (sum);
									eBatch.RemoveAt (c+1);
									eBatch.RemoveAt (c);
								} else {
									PrintAbles.PrintError ("Invalid Operation");
									okay = false;
									break;
								}
							} 
						else if(lhsType == 3 && rhsType== 1){
							double LHS = lhs.getNumberExpression ().getNumber ();
							Matrix RHS = rhs.getMatrix ();
							bool yes = false; 
							if (left.Contains ("-")) {
								LHS = LHS * (-1);
							}
							if (right.Contains ("-")) {
								RHS = RHS * (-1);
								yes = true;
							}
							Matrix ans = LHS * RHS;
							if (yes)
								RHS = RHS * (-1);
							ans.setTag (left.TrimStart(new char []{'-'}));
							theExpressionList [theExpressionList.IndexOf (lhs)].setMatrix (ans);
							eBatch.RemoveAt (c+1);
							eBatch.RemoveAt (c);
						}
						else if(lhsType == 1 && rhsType== 3){
							double RHS = rhs.getNumberExpression ().getNumber ();
							Matrix LHS = lhs.getMatrix ();
							bool yes = false; 
							if (right.Contains ("-")) {
								RHS = RHS * (-1);
							}
							if (left.Contains ("-")) {
								LHS = LHS * (-1);
								yes = true;
							}
							Matrix ans = LHS * RHS;
							if (yes)
								LHS = LHS * (-1);
							ans.setTag (left.TrimStart(new char []{'-'}));
							theExpressionList [theExpressionList.IndexOf (lhs)].setMatrix (ans);
							eBatch.RemoveAt (c+1);
							eBatch.RemoveAt (c);
						}
						else if (rhsType == 3 ||isNumeric(left.TrimStart (new char[]{'-'})) ||isNumeric(right.TrimStart (new char[]{'-'}))) {
								double LHS = 0, RHS = 0;
								if (isNumeric (left.TrimStart (new char[]{'-'}))) {
									LHS = double.Parse (left.TrimStart (new char[]{ '-' }));
								}
								else 
									LHS = lhs.getNumberExpression ().getNumber ();
								if (isNumeric (right.TrimStart (new char[]{'-'}))) {
									RHS = double.Parse (right.TrimStart (new char[]{ '-' }));
								}
								else
									RHS = rhs.getNumberExpression ().getNumber ();
								if (left.Contains ("-")) {
									LHS = LHS * (-1);
									eBatch [c - 1] = eBatch [c - 1].TrimStart (new char[]{'-'});
								}
								if (right.Contains ("-")) {
									RHS = RHS * (-1);
									eBatch [c + 1] = eBatch [c + 1].TrimStart (new char[]{'-'});
								}
								double num = LHS * RHS;
								bool go =true;
								Number sum = new Number ();
								if (isNumeric (left.TrimStart (new char[]{ '-' })) && isNumeric (right.TrimStart (new char[]{ '-' }))) {
									sum.setTag (num.ToString ());
									eBatch [c - 1] = num.ToString ();
									go = false;
								} else if (isNumeric (left.TrimStart (new char[]{ '-' })) && go) {
									sum.setTag (num.ToString ());
									eBatch [c - 1] = num.ToString ();
								} else {
									sum.setTag (eBatch [c - 1]);
								}
								sum.setNumber (num);
								theExpressionList [theExpressionList.IndexOf ((lhs))].setNumberExpression (sum);
								eBatch.RemoveAt (c+1);
								eBatch.RemoveAt (c);
							}  
						}

					c = 0;
					} 

					else{
						size = eBatch.Count;
						c += 1;
					}


				}
				return okay;
		}   //function for multiplication ends

		public bool DMAS_Division()   // function for division
		{
				bool okay = true;
				int c = 0;
				int size = eBatch.Count;
				while ((size>1)) {
					if (c == eBatch.Count)
						break;
					string x = eBatch [c].Trim();
					if (eBatch.Count <= 1)
						break;
					if (x == "/") {
						string left = eBatch [c - 1];
						string right = eBatch [c + 1];
						Expression lhs = getExpression (eBatch [c - 1].TrimStart(new char[]{'-'}));
						Expression rhs = getExpression (eBatch [c + 1].TrimStart(new char[]{'-'}));
						int lhsType = lhs.getExpressionType ();
						int rhsType = rhs.getExpressionType ();
						if (lhsType == rhsType || lhsType!=rhsType) {
							if (lhsType == 1 && rhsType==1) {
							PrintAbles.PrintError ("Invalid Operation");
							okay = false;
							break;
						    }
							else if(lhsType == 3 && rhsType== 1){
							PrintAbles.PrintError ("Invalid Operation");
							okay = false;
							break;
							}
							else if(lhsType == 1 && rhsType== 3){
								double RHS = rhs.getNumberExpression ().getNumber ();
								Matrix LHS = lhs.getMatrix ();
								bool yes = false; 
								if (right.Contains ("-")) {
									RHS = RHS * (-1);
								}
								if (left.Contains ("-")) {
									LHS = LHS * (-1);
									yes = true;
								}
								Matrix ans = LHS / RHS;
								if (yes)
									LHS = LHS * (-1);
								ans.setTag (left.TrimStart(new char []{'-'}));
								theExpressionList [theExpressionList.IndexOf (lhs)].setMatrix (ans);
								eBatch.RemoveAt (c+1);
								eBatch.RemoveAt (c);
							}
							else if (rhsType == 3 ||isNumeric(left.TrimStart (new char[]{'-'})) ||isNumeric(right.TrimStart (new char[]{'-'}))) {
								double LHS = 0, RHS = 0;
								if (isNumeric (left.TrimStart (new char[]{'-'}))) {
									LHS = double.Parse (left.TrimStart (new char[]{ '-' }));
								}
								else 
									LHS = lhs.getNumberExpression ().getNumber ();
								if (isNumeric (right.TrimStart (new char[]{'-'}))) {
									RHS = double.Parse (right.TrimStart (new char[]{ '-' }));
								}
								else
									RHS = rhs.getNumberExpression ().getNumber ();
								if (left.Contains ("-")) {
									LHS = LHS * (-1);
									eBatch [c - 1] = eBatch [c - 1].TrimStart (new char[]{'-'});
								}
								if (right.Contains ("-")) {
									RHS = RHS * (-1);
									eBatch [c + 1] = eBatch [c + 1].TrimStart (new char[]{'-'});
								}
								double num = LHS / RHS;
								bool go =true;
								Number sum = new Number ();
								if (isNumeric (left.TrimStart (new char[]{ '-' })) && isNumeric (right.TrimStart (new char[]{ '-' }))) {
									sum.setTag (num.ToString ());
									eBatch [c - 1] = num.ToString ();
									go = false;
								} else if (isNumeric (left.TrimStart (new char[]{ '-' })) && go) {
									sum.setTag (num.ToString ());
									eBatch [c - 1] = num.ToString ();
								} else {
									sum.setTag (eBatch [c - 1]);
								}
								sum.setNumber (num);
								theExpressionList [theExpressionList.IndexOf ((lhs))].setNumberExpression (sum);
								eBatch.RemoveAt (c+1);
								eBatch.RemoveAt (c);
							}  
						}

						c = 0;
					} 

					else{
						size = eBatch.Count;
						c += 1;
					}


				}
				return okay;
		}


	} // end simple solver class.

	static class PrintAbles
	{
		static public void PrintError()
		{
			Console.WriteLine ("Error");
		}

		static public void PrintError(string Error)
		{
			Console.WriteLine (Error);
		}
	}


}   //end EquationSolvingSpace.

