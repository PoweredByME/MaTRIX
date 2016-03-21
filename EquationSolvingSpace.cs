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
			eBatchMaker ();
			if (allPresent ()) {
				if (DMAS_Addition ()) {
					/*if (DMAS_Subtraction ()) {
						sol = (getExpression (eBatch [0]));
					} else {
						Expression x = new Expression ();
						x.setErrorMessage ("Invalid operation.");
						sol = x;
					}*/
					sol = getExpression (eBatch [0]);
				} else {
					Expression x = new Expression ();
					x.setErrorMessage ("Invalid operation.");
					sol = x;
				}
			} else {
				sol.setErrorMessage ("The variables in the expression are undefined or the operations you are trying top perfor are in valid");
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


		private void eBatchMaker()
		{
			string dumy = "";
			foreach (char x in expression) {
				if(x == '+' || x == '-'|| x == '*'|| x == '+')
				{
					if(!string.IsNullOrWhiteSpace(dumy))
					{
						dumy = dumy.Trim();
						eBatch.Add(dumy);
						dumy="";
					}
					eBatch.Add(x.ToString());
				}
				else
				dumy+=x.ToString();
			}
			if(!string.IsNullOrWhiteSpace(dumy))
			{
				dumy.Trim();
				eBatch.Add(dumy);
			}
			eBatchManager ();
		}

		private void eBatchManager()
		{
			if (eBatch [0] == "-") {
				eBatch [1] = "-" + eBatch [1];
				eBatch.RemoveAt (0);
			}
			List<int> p = new List<int> ();
			for(int c=0; c < eBatch.Count ; c++){
				string x = eBatch [c];
				if (x == "-") {
					if (eBatch [eBatch.IndexOf (x) - 1] == "(" || eBatch [eBatch.IndexOf (x) - 1] == "+" || eBatch [eBatch.IndexOf (x) - 1] == "-" || eBatch [eBatch.IndexOf (x) - 1] == "*"
					   || eBatch [eBatch.IndexOf (x) - 1] == "/") {
						eBatch [eBatch.IndexOf (x) + 1] = "-" + eBatch [eBatch.IndexOf (x) + 1];
							p.Add(eBatch.IndexOf (x));
					}
				}
			}
			foreach (int x1 in p) {
				eBatch.RemoveAt (x1);
			}
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
				string x = eBatch [c].Trim();
				if (eBatch.Count <= 1)
					break;
				if (x == "+") {
					string left = eBatch [c - 1];
					string right = eBatch [c + 1];
					if (isNumeric (left.TrimStart (new char[]{ '-' })) && !theExpressionList.Contains(getExpression(left.TrimStart(new char [] {'-'})))) {
						Number a1 = new Number ();
						a1.setTag (left.TrimStart(new char[]{'-'}));
						a1.setNumber (double.Parse(left.TrimStart(new char[]{'-'})));
						Expression x22 = new Expression ();
						x22.setNumberExpression (a1);
						theExpressionList.Add (x22);
					}
					if (isNumeric (right.TrimStart (new char[]{ '-' }))&& !theExpressionList.Contains(getExpression(right.TrimStart(new char [] {'-'})))) {
						Number a1 = new Number ();
						a1.setTag (right.TrimStart(new char[]{'-'}));
						a1.setNumber (double.Parse(right.TrimStart(new char[]{'-'})));
						Expression x22 = new Expression ();
						x22.setNumberExpression (a1);
						theExpressionList.Add (x22);
					}
					Expression lhs = getExpression (eBatch [c - 1].TrimStart(new char[]{'-'}));
					Expression rhs = getExpression (eBatch [c + 1].TrimStart(new char[]{'-'}));
					int lhsType = lhs.getExpressionType ();
					int rhsType = rhs.getExpressionType ();
					bool yeal = false;bool year = false;
					if (lhsType == rhsType || isNumeric(left.TrimStart (new char[]{'-'})) || isNumeric(right.TrimStart (new char[]{'-'}))) {
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


		/*private bool DMAS_subtraction()
		{
			bool okay = true;
			int count = 0;
			int size = eBatch.Count;
			while (size > 1) {
				if (eBatch.Count <= 1) {
					break;
				}
				string x = eBatch [count];
				if (x == "-") {
					string 
				}
				count++;
			}
			return okay;
		}*/






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

