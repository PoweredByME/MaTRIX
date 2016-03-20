using System;
using System.Collections.Generic;
using MatrixSpace;

namespace EquationSolvingSpace
{
	public class DMAS_Solver
	{
		private List<Expression> theExprssionList = new List<Expression>();
		private string expression = "";
		private Expression solution = new Expression();
		public DMAS_Solver(){}
		public DMAS_Solver(List<Expression> tel, string ex)
		{
			theExprssionList = tel;
			expression = ex;
			SimpleSolver sol = new SimpleSolver (theExprssionList, expression);

		}
			
		public Expression getSolution()
		{
			//Matrix s  = new Matrix("my",new double[,] {{2,2},{2,2}},2,2);
			//solution.setMatrix(s);
			solution.setErrorMessage("No");
			return solution;
		}
			
	}   //end class DMAS_SOLVER

	class SimpleSolver
	{
		private List<Expression> theExprssionList = new List<Expression>();
		private string expression = "";
		private List<string> eBatch = new List<string>();
		public SimpleSolver(){}
		public SimpleSolver(List<Expression> tel, string ex)
		{
			theExprssionList = tel;
			expression = ex;
		}

		private Expression Solve()
		{
			Expression sol = new Expression ();
			eBatchMaker ();

			eBatch.RemoveRange (0, eBatch.Count);
			return sol;
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
				dumy+=x.ToString();
			}
			if(!string.IsNullOrWhiteSpace(dumy))
			{
				dumy.Trim();
				eBatch.Add(dumy);
			}
		}


	}


}

