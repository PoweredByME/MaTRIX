using System;
using System.Collections.Generic;

namespace MatrixSpace
{
	public class Matrix  //matrix class starts
	{
		protected double[,] theMatrix;      //the basic matrix;
		protected int mRows, mCols;
		private OperationsClass theOperations =  new OperationsClass();
		private static OperationsClass staticOperations =  new OperationsClass();
		private string tag;

		public Matrix(Matrix theOther)
		{
			mRows = theOther.mRows;
			mCols = theOther.mCols;
			theMatrix = theOther.theMatrix;
			tag = theOther.tag;
		}

		public Matrix(string thetag, double[,] matrix,int row = 1, int column = 1 )
		{
			mRows = row;mCols = column;
			theMatrix = matrix;
			tag = thetag;
		}

		public Matrix(int rows = 01, int columns = 01)
		{
			mRows = rows; mCols = columns;
			theMatrix = new double[mRows, mCols];
			tag = " ";
		}

		public Matrix(double[,] mat, int rows , int cols)
		{
			mRows = rows; mCols = cols;
			theMatrix = mat;
			tag = " ";
		}

		public void consoleStepInput()
		{
			consoleMatrixInput input = new consoleMatrixInput (mRows, mCols);
			input.getInput ();
			theMatrix = input.getMatrix ();
		}

		public void consoleOutput()
		{
			Console.WriteLine ("{0} = ", tag);
			consoleMatrixOutput output = new consoleMatrixOutput (theMatrix, mRows, mCols);
			output.getOutput ();
		}

		public void Zero()
		{
			for (int c = 0; c < mRows; c++) {
				for (int c1 = 0; c1 < mCols; c1++) {
					theMatrix [c, c1] = 0;
				}
			}
		}

		public void Identity()
		{
			for (int c = 0; c < mRows; c++) {
				for (int c1 = 0; c1 < mCols; c1++) {
					if (c == c1)
						theMatrix [c, c1] = 1;
					else
						theMatrix [c, c1] = 0;
				}
			}
		}

		public void setMatrixArray(double [,] mat){theMatrix = mat;}
		public string getTag(){return tag;}
		public Matrix getAdjoint(){return theOperations.getAdjoint (this);}
		public Matrix getTranspose(){return theOperations.getTranspose (this);}
		public double determinant(){return theOperations.getdetreminant (this);}
		public Matrix addTo(Matrix otherMat){return theOperations.addMatrix (this,otherMat);}
		public Matrix subtractFrom(Matrix otherMat){return theOperations.subtractMatrix (otherMat, this);}
		public int getRows(){return mRows;}
		public int getColumns(){return mCols;}
		public double  getElement(int row, int col){return theMatrix [row-1, col-1];}
		public double[,] getMatrixArray(){return theMatrix;}
		public void setTag(string theMatTag) {tag = theMatTag;}
		public void setElement(double number, int rows, int col){theMatrix [rows-1, col-1] = number;}
		static public Matrix operator+(Matrix lhs, Matrix rhs){return staticOperations.addMatrix (lhs, rhs);}
		static public Matrix operator-(Matrix lhs, Matrix rhs){return staticOperations.subtractMatrix (lhs, rhs);}
		static public Matrix operator*(Matrix lhs, Matrix rhs){return staticOperations.multiplyMatrix (lhs, rhs);}
	}      //the basic matrix class ends

	public class Number{      // the class for numeric numbers
		private double number;
		private string tag;

		public double getNumber(){
			return number;
		}

		public string getTag(){
			return tag;
		}

		public void setTag(string theTag){
			tag = theTag;
		}

		public void setNumber(double theNumber){
			number = theNumber;
		}

		public Number(){
			number = 0;
			tag = "";
		}

		public Number(Number obj){
			number = obj.number;
			tag = obj.tag;
		}

		public void consolePrintNumber()
		{
			Console.WriteLine ("{0} = {1}", tag, number);
		}
	}     //end class for numbers;


	class OperationsClass     //start operations class
	{
		public Matrix addMatrix(Matrix mat, Matrix mat1)    //Matrix Addition
		{ 
			if (mat.getRows () == mat1.getRows () && mat.getColumns () == mat1.getColumns ()) {
				Matrix result = new Matrix (mat.getRows (), mat.getColumns ());
				for (int c = 1; c <= mat.getRows (); c++) {
					for (int c1 = 1; c1 <= mat1.getColumns (); c1++) {
						result.setElement (mat.getElement (c, c1) + mat1.getElement (c, c1), c, c1);
					}
				}
				return result;
			} else {
				Matrix result = new Matrix (1, 1);result.setElement (0, 1, 1);
				Console.WriteLine ("The Matrices are not similar... cannot be added.Sorry.");
				return result;
			}
		}      //Matrix Addition Ends

		public Matrix subtractMatrix(Matrix mat, Matrix mat1)   //Matrix Subtraction
		{
			if (mat.getRows () == mat1.getRows () && mat.getColumns () == mat1.getColumns ()) {
				Matrix result = new Matrix (mat.getRows (), mat.getColumns ());
				for (int c = 1; c <= mat.getRows (); c++) {
					for (int c1 = 01; c1 <= mat1.getColumns (); c1++) {
						result.setElement (mat.getElement (c, c1) - mat1.getElement (c, c1), c, c1);
					}
				}
				return result;
			} else {
				Matrix result = new Matrix (1, 1);
				result.setElement (0, 1, 01);
				Console.WriteLine ("The Matrices are not similar... cannot be subtracted.Sorry.");
				return result;
			}
		}         //End Matrix Subtration

		public Matrix multiplyMatrix(Matrix lhs, Matrix rhs)
		{
			if (lhs.getColumns() == rhs.getRows ()) {
				Matrix result = new Matrix (lhs.getRows (), rhs.getColumns ());
				for (int c = 1; c <= lhs.getRows (); c++) {
					for (int c1 = 1; c1 <= rhs.getColumns (); c1++) {
						for (int c2 = 1; c2 <= lhs.getColumns (); c2++) {
							if (c2 == 0)
								result.setElement (lhs.getElement (c, c2) * rhs.getElement (c2, c1), c, c1);
							else
								result.setElement (result.getElement (c, c1) + (lhs.getElement (c, c2) * rhs.getElement (c2, c1)), c, c1);
						}
					}
				}
				return result;
			} else {
				Matrix result = new Matrix (1, 1);
				result.setElement (0, 1, 1);
				Console.WriteLine ("The matrices cannot be multiplied.Sorry.");
				return result;
			}
		}

		public double getdetreminant(Matrix theMatrix)    //getdeterminant interface
		{
			double[,] matrix = theMatrix.getMatrixArray ();
			int mRows = theMatrix.getRows ();
			int mCols = theMatrix.getColumns ();
			return determinant (matrix, mRows, mCols);
		}     //getdetermiant ends here.

		private double determinant(double [,] matrix, int rows, int cols)   //determinant implementation 
		{
			if (rows == cols) {
				if (rows == 1 && cols == 1) {
					return matrix [0, 0];
				} else if (rows == 2 && cols == 2) {
					return matrix [0, 0] * matrix [1, 1] - matrix [0, 1] * matrix [1, 0];
				} else {
					double det = 0;
					double[] store = new double[cols];
					int newRows = rows - 1;
					int newCols = cols - 1;
					double[,] newMatrix = new double[newRows, newCols];

					for (int ncolumn = 0; ncolumn < cols; ncolumn++) {
						int newrow = 0;
						int newcolumn = 0;
						for (int row = 0 ; row < rows; row++) {
							for (int column = 0; column < cols; column++) {
								if (row != 0) {
									if (column != ncolumn) {
										newMatrix [newrow, newcolumn] = matrix [row, column];
										newcolumn++;
										if (newcolumn == newCols) {
											newcolumn = 0;
											newrow++;
										}//end
									}//end
								}//end
							}//end for
						}//end for
						int sign = (int)Math.Pow (-1, ncolumn);
						store [ncolumn] = sign * determinant (newMatrix, newRows, newCols);
					}//end for
					det = 0;
					for (int c = 0; c < cols; c++) {
						det += (matrix [0, c] * store [c]);
					}
					return det;
				}//end else

			} else {
				Console.WriteLine ("The determinant of the matrix doesnot exist.Sorry.");
				return 0;
			}
		}     //end determinant measurement function ends here

		public Matrix getTranspose(Matrix mat)
		{
			double[,] matrix = mat.getMatrixArray ();
			int rows = mat.getRows (); int cols = mat.getColumns ();
			return Transpose (matrix, rows, cols);
		}

		private Matrix Transpose(double[,] matrix, int rows, int cols)
		{
			Matrix t = new Matrix (cols, rows);
			for (int c = 01; c <= rows; c++) {
				for (int c1 = 01; c1 <= cols; c1++) {
					t.setElement (matrix [c-1, c1-1], c1, c);
				}
			}
			return t;
		}

		public Matrix getAdjoint(Matrix matrix)
		{
			double[,] mat = matrix.getMatrixArray ();
			int rows = matrix.getRows (); int cols = matrix.getColumns ();
			return Adjoint (mat, rows, cols);
		}

		private Matrix Adjoint(double[,] matrix, int rows, int cols)   //function for adjoint
		{
			if (rows == cols) {
				if (rows == 1 && cols == 1) {
					Matrix result = new Matrix (1, 1);
					result.setElement (matrix [0, 0], 1, 1);
					return result;
				} else {
					double[,] newMatrix = new double[rows, cols];
					double[,] tobeAdjoint = new double[rows, cols];
					int newCol = cols - 1;
					int newRow = rows - 1;
					for (int nrow = 0; nrow < rows; nrow++) {
						for (int ncol = 0; ncol < cols; ncol++) {
							int newrow = 0;int newcol = 0;
							for (int c = 0; c < rows; c++) {
								for (int c1 = 0; c1 < cols; c1++) {
									if (nrow != c) {
										if (ncol != c1) {
											newMatrix [newrow, newcol] = matrix [c, c1];
											newcol++;
											if (newcol == newCol) {
												newcol = 0;
												newrow++;
											}
										}
									}
								}
							}
							int sign = 0;
							int sum = ncol + nrow;
							sign = (int)Math.Pow (-1, sum);
							tobeAdjoint [nrow, ncol] = sign * determinant (newMatrix, newRow, newCol);
						}
					}
					return Transpose (tobeAdjoint, rows, cols);
				}
			} else {
				Console.WriteLine ("The Adjoint of this matrix doesnot exist. Sorry.");
				Matrix result = new Matrix (1, 1);
				result.Zero ();
				return result;
			}
		}     //end function for adjoint.

	}       //end operations class.


	class consoleMatrixInput    //class for console input
	{
		private double[,] number;
		private int Mrows, Mcolumns;

		public consoleMatrixInput(int rows = 1, int cols = 0)
		{
			Mrows = rows;
			Mcolumns = cols;
			number = new double[Mrows, Mcolumns];
		}

		public void getInput()
		{
			for (int c = 0; c < Mrows; c++) {
				for (int c1 = 0; c1 < Mcolumns; c1++) {
					Console.Write ("Enter the element [{0}][{1}] = ", c + 1, c1 + 1);

					number[c,c1] = double.Parse(Console.ReadLine());
				}
			}
		}

		public double [,] getMatrix(){return number;}

	}      //end class input matrix

	class consoleMatrixOutput      //console output
	{
		private double[,] theMatrix;
		private int mRows, mCols;
		public consoleMatrixOutput(int rows=1, int columns=1)
		{
			mRows = rows;
			mCols = columns;
		}
		public consoleMatrixOutput(double [,] mat, int rows, int columns)
		{
			mRows = rows ;
			mCols = columns;
			theMatrix = mat;
		}
		public void setMatrix(double [,] mat)
		{
			theMatrix = mat;
		}
		public void getOutput()
		{
			for (int c = 0; c < mRows; c++) {
				for (int c1 = 0; c1 < mCols; c1++) {
					Console.Write ("\t {0}", theMatrix [c, c1]);
				}
				Console.WriteLine ();
			}
		}
	}     //end class output

	public class StringEnumerator{     //class for understanding the string 

		private string expression;
		private List<string> eBatch;
		private List<Matrix> theMatrixList;
		static private List<Expression> theExpressionList = new List<Expression>();
		static int count = 0;
		static int countExp = 0;
		string errorMessage = "";
		string Alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		string Counting  = "0123456789";
		bool allReadyExistAndPrinted = false;
		Matrix OutMat;
		public StringEnumerator(string theExpression = " ") 
		{
			expression = theExpression;
			eBatch = new List<string>();
			theMatrixList = new List<Matrix> ();
			OutMat = new Matrix (1, 1);
			OutMat.Zero ();
		}

		public bool AllReadyExistAndPrinted(){
			bool x  = allReadyExistAndPrinted;
			allReadyExistAndPrinted =false;
			return x;}


		public void PrintExpression()
		{
			if(allReadyExistAndPrinted == false)
			{
				//Process ();
				ExpressionPrinter e = new ExpressionPrinter (theExpressionList [count]);
				if(!(string.IsNullOrWhiteSpace(errorMessage)))
				{
					Console.WriteLine(errorMessage);
					errorMessage = "";
				}
				e.Printer ();
				count++;
			}
		}

		public void setString(string newString){expression = newString;} 

		public Matrix getMatrix()
		{
			Process ();
			return OutMat;
		}

		public void printAllExpressions()
		{
			int count = 0;
			foreach(Expression x in theExpressionList)
			{
				Console.WriteLine("Counter = " + count);
				count++;
				ExpressionPrinter a = new ExpressionPrinter(x);
				a. Printer();
			}
		}

		private bool LookInto(Expression x, string tag)
		{
			if(x.getExpressionType() == 1)
			{
				if((x.getMatrix()).getTag() == tag)
					return true;
				else 
					return false;
			}
			else return false;
		}

		private bool lookIntoNumber(Expression x, string theTag)
		{
			if (x.getExpressionType () == 3) {
				if ((x.getNumberExpression ()).getTag () == theTag)
					return true;
				else
					return false;
			} else
				return false;
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

		private bool CheckAlphatbetsInStart(string exp)
		{
			int count = 0;
			foreach (char x in exp) {
				if (ifContain (Alphabets, x.ToString()))
					count++;
				if (x == '[')
					break;
			}
			if (count == 0)
				return false;
			else
				return true;
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

		private struct theTwoResultStrings
		{
			public string resultLHS;
			public string resultRHS;
		}


		public void addExpression(Expression x)
		{
			theExpressionList.Add (x);
		}

		public void StringProcess()
		{
			expression = expression.Trim ();
			theTwoResultStrings a = SplitString (expression, '=');
			a.resultLHS = a.resultLHS.Trim ();
			a.resultRHS = a.resultRHS.Trim ();
			if (!string.IsNullOrWhiteSpace(a.resultLHS) && !string.IsNullOrWhiteSpace(a.resultRHS)) {
				if (ifContain(expression, "=") && (ifContain(expression, "+")||ifContain(expression, "-")||ifContain(expression, "*"))) {
					ProcessEquation (a.resultLHS, a.resultRHS);
				} else
					Process ();
			} else
				Process ();
		}

		private void ProcessEquation(string lhs, string rhs)
		{
			EquationSolvingSpace.DMAS_Solver sol = new EquationSolvingSpace.DMAS_Solver (theExpressionList, rhs);
			string dumy = "";
			if (sol.getSolution ().getExpressionType () != 2) {
				foreach (char x in lhs) {
					if (x == '+' || x == '-' || x == '(' || x == '/' || x == '*' || x == ')') {
						dumy = dumy.Trim ();
						if (!string.IsNullOrWhiteSpace (dumy)) {
							eBatch.Add (dumy);
							dumy = "";
						}
						eBatch.Add (x.ToString ());
					} else {
						dumy += x.ToString ();
					}
				}
				if (!string.IsNullOrWhiteSpace (dumy)) {
					dumy = dumy.Trim ();
					eBatch.Add (dumy);
				}
            
				if (eBatch.Count != 1) {
					Expression error = new Expression ();
					if (eBatch.Count > 1)
						error.setErrorMessage ("Invalid form of equation entered. You must enter only one variable on the LEFT HAND SIDE.");
					else if (eBatch.Count < 1)
						error.setErrorMessage ("Invalid form of equation entered. You must enter only one variable name on the RIGHT HAND SIDE.");
					theExpressionList.Add (error);
				} else {
					if (theExpressionList.Contains (getExpression (lhs))) {
						Expression n = new Expression (sol.getSolution ());
						n.setTag (lhs);
						if (n.getExpressionType () == 1)
							n.getMatrix ().setTag (lhs);
						else if (n.getExpressionType () == 3)
							n.getNumberExpression ().setTag (lhs);
						theExpressionList [theExpressionList.IndexOf (getExpression (lhs))].setExpression (n);
						Expression Alert = new Expression ();
						Alert.setAlertMessage ("The variable on the left hand side has been updates.");
						theExpressionList.Add (Alert);
						Console.WriteLine ("SOLVED!!!");
					} else {
						Expression n = new Expression (sol.getSolution ());
						n.setTag (lhs);
						if (n.getExpressionType () == 1)
							n.getMatrix ().setTag (lhs);
						else if (n.getExpressionType () == 3)
							n.getNumberExpression ().setTag (lhs);
						theExpressionList.Add (n);
						Console.WriteLine ("SOLVED!!!");
					}
				}
				eBatch.RemoveRange (0, eBatch.Count);
			} else {
				Expression error = new Expression ();
				error.setErrorMessage ("The variables entered on the RIGHT HAND SIDE may be undefined.");
				theExpressionList.Add (error);
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

		private void Process()    //method for processing the string
		{
			expression = expression.Trim();     //removes the junk from the string edges
			theTwoResultStrings a = SplitString (expression, '=');   //spliting the string
			a.resultLHS = a.resultLHS.Trim ();     //trims the splited things
			char[] rlhs = a.resultLHS.ToCharArray ();   // converting to char array
			a.resultRHS = a.resultRHS.Trim ();   //triming
			if (string.IsNullOrWhiteSpace (expression)) {     //if the string is empty
				Expression err = new Expression ();
				err.setErrorMessage ("You have not given any input for me to process");
				theExpressionList.Add (err);
			} else if (ifContain (expression, "=") && ifContain (expression, "[") && ifContain (expression, "]") && !(ifContain (expression, "+")
				&& ifContain (expression, "-"))) { // if teh string full fills the attributes of teh matrix. like having and assignment thing and a [] bracket and doesnot contain any unessarry operator like "+-/* etc"
				if (ifContain ("0123456789", rlhs [0].ToString ()) == false) { //if the right hand side does not contain a numerical on the start of the string
					int lastCharacter = expression.IndexOf ("]");   //gets the index of the closing brackett
					int explenght = expression.Length;   //gets the lenght of the expression 
					if (explenght == lastCharacter + 1 && countOccurance (expression, ']') == 1 && countOccurance (expression, '[') == 1) { // if the  things are all right the makes the matrix 
						OutMat = matrixWithTagProcess (expression);   //process and making of the matrix
					} else {     //else if something went wrong like [ or ] occered twice or things like that
						Expression err = new Expression ();
						err.setErrorMessage ("I cannot understand the expression you entered.");  // error message
						theExpressionList.Add (err);
					}  //end else
				} else {   //else if the right hnad side contain a numerical on teh right side of the expression.
					Expression err = new Expression ();
					err.setErrorMessage ("You have entered an invalid variable name. Its first character should be an Alphabet.");  //error message
					theExpressionList.Add (err);
				} // end else
			} // end the making of the matrix with discreet assignment and tag name
			else if (ifContain (expression, "[") && ifContain (expression, "]") && !(ifContain (expression, "+") && ifContain (expression, "-"))) { // if teh expression doesnot contain an assignment operator as well as a variable name. 
				string theNewExpression = "";   
				int lastCharacter = 0;
				int expLenght = 0;
				char[] echar = expression.ToCharArray ();   // converting expression to a char array.
				if (ifContain ("0123456789", echar [0].ToString ()) == false) {   // if there is not an invalid variable name.
					if (CheckAlphatbetsInStart (expression)) {    // if there is a valid variable name at the start.
						theNewExpression = expression.Insert (expression.IndexOf ("["), "=");  //inserting  an assignment operator in between the vaiable name and the []
						theNewExpression = theNewExpression.Trim ();  //trim the new expression.
						lastCharacter = theNewExpression.IndexOf ("]");   
						expLenght = theNewExpression.Length;
					} else {   //else if there is no assignment and a vaiable name in the expression 
						theNewExpression = "=";    
						theNewExpression += expression;
						theNewExpression = theNewExpression.Trim ();
						lastCharacter = theNewExpression.IndexOf ("]");
						expLenght = theNewExpression.Length;
					}
					if (expLenght == lastCharacter + 1 && countOccurance (theNewExpression, '[') == 1 && countOccurance (theNewExpression, ']') == 1) {// if there is everything alright.
						OutMat = matrixWithTagProcess (theNewExpression);
						theMatrixList.Add (OutMat);
					} else {     //else errror
						Expression err = new Expression ();
						err.setErrorMessage ("I cannot understand the expression you entered.");
						theExpressionList.Add (err);	
					}
				} else {    // else if an invalid variable name was entered
					Expression err = new Expression ();
					err.setErrorMessage ("You have entered an invalid variable name. Its first character should be an Alphabet.");
					theExpressionList.Add (err);
				}   //end else  
			}     //if for unknown variable or assignment

			else if (ifContain (expression, "=") && isNumeric (a.resultRHS) && ifContain (Alphabets, rlhs [0].ToString ())) {  // looks if the string is a normal number.
				Number num = new Number ();
				num.setTag (a.resultLHS);
				num.setNumber (double.Parse (a.resultRHS));
				if (SearchForSameTagCaseNumber (num, a.resultLHS) == false) {
					Expression number = new Expression ();
					number.setNumberExpression (num);
					theExpressionList.Add (number);
				}
			}

			else if (ifContain (expression, "=") && isNumeric (a.resultRHS) && a.resultLHS == null) {  //looks if the string is a number and user has given no name to it.
				Number num = new Number ();
				num.setTag ("n" + SNACount);
				SNACount++;
				num.setNumber (double.Parse (a.resultRHS));
				Expression number = new Expression ();
				number.setNumberExpression (num);
				theExpressionList.Add (number);
			}

			else if (isNumeric (expression)) { // this checks if the expression is a number and assigns an approprate class to it.
				Number num = new Number();
				num.setTag ("n" + SNACount);
				SNACount++;
				num.setNumber (double.Parse (expression));
				Expression number = new Expression ();
				number.setNumberExpression (num);
				theExpressionList.Add (number);
			}

			else    //else if the string format doesnot matches that of a matrix
			{
				string theGivenString = "";  //a dumy string for the trimed expression;
				bool found = false;
				theGivenString = expression.Trim();
				foreach(Expression x in theExpressionList)  //checks the entire list for a variable tag... if it exists.
				{
					if(LookInto(x, theGivenString)|| lookIntoNumber(x,theGivenString))  // if the tag matches the given string.
					{
						found = true;   // the match is found
						allReadyExistAndPrinted = true;  // a lock is trued 
						ExpressionPrinter x1 = new ExpressionPrinter(x);   // this prints that expression. 
						x1.Printer();
					}
				}
				if(found == false)    // if the variable was not found. it looks if the string is command or something like that.
				{
					string dumyExp = expression;   // another dumy expression for the expression.
					dumyExp = dumyExp.ToUpperInvariant();   // the dumy gets the upper invariant of the expression string.
					char[] dumyExpArray = dumyExp.ToCharArray();  // dumy expression to a char array.
					char endC = dumyExpArray[0];  
					int dumyExpCount = 0;
					Counting += "*";   //add another option in the counting class global variable. 
					string CommandString = "";
					string ParameterString = "";
					while(dumyExpCount < dumyExp.Length)   //seperation of the command string and parameter string.
					{
						endC = dumyExpArray[dumyExpCount];   //assigning  a char one by one and chackning it.
						if(ifContain(Alphabets, endC.ToString()))
						{
							CommandString += endC;      
						}
						else if(ifContain(Counting+="()", endC.ToString()))
						{
							ParameterString+=endC;
						}
						dumyExpCount++;
					}
					if(CommandString == "IDENTITYMATRIX"|| CommandString=="IDENTITY")   //if a valid command string is given.
					{ 
						if(string.IsNullOrWhiteSpace(ParameterString)){   //but if the parameters are not entered
							Expression error = new Expression();
							error.setErrorMessage("No parameters given. Incomplete information.");
							theExpressionList.Add(error);
						}
						else{
							ParameterString = ParameterString.TrimEnd (new Char[] {')'});   //triming unwanted characters
							ParameterString = ParameterString.TrimStart (new Char[] {'('});

							char[] pChar = ParameterString.ToCharArray();    // char array if the parameter string.
							string[] rc = new string[2];     //strings to take the parametes
							int rows= 1, columns = 1;    
							dumyExpCount=0;
							int selector = 0;
							endC = pChar[dumyExpCount];
							bool makeMatrix = true;
							dumyExpCount = 0;
							int cx0 = ParameterString.IndexOf ("*");
							int cx1 = ((ParameterString.Length) - 1);
							if (ifContain (ParameterString, "*") == true && cx0 != cx1 && ParameterString.IndexOf("*")!=0)
							{    //if everything is right and matrix parameter extraction is good to goo i.e the string has a single amount.
								while (dumyExpCount < ParameterString.Length) {
									endC = pChar [dumyExpCount];
									rc [selector] += endC;
									dumyExpCount++;

									if (endC == '*' && selector == 0) {
										selector = 1;
									} else if (endC == '*' && selector == 1) {
										Expression error = new Expression ();
										error.setErrorMessage ("I cannot understand the parameters you entered.");
										theExpressionList.Add (error);
										makeMatrix = false;
										break;
									}

								}
							} else {   // there is no * operator
								if (isNumeric (ParameterString) == true) {
									rc [0] = rc[1] = ParameterString;
								} else {
									makeMatrix = false;
									Expression error = new Expression ();
									error.setErrorMessage ("Invalid Parameters entered.");
									theExpressionList.Add (error);
								}
							}
							if (makeMatrix == true){  // if matrix making is good to gooooo
								rc[0] = rc[0].TrimEnd(new Char[]{'*'});
								rows = int.Parse(rc[0]);
								columns = int.Parse(rc[1]);
								if(rows == columns)   // of rows and columns are equal to eachother.
								{
									string id = "I"+IdentityCount.ToString();
									IdentityCount++;
									Matrix Identity = new Matrix(rows, columns);
									Identity.Identity();
									Identity.setTag(id);
									Expression m = new Expression();
									m.setMatrix(Identity);
									theExpressionList.Add(m);
								}
								else  // of this is not the case then do this.
								{
									Expression error = new Expression();
									error.setErrorMessage("The number of ROWS and COLUMNS you entered are not EQUAL. Cannot make an Identity matrix");
									theExpressionList.Add(error);
								}// end else for row==column
							}//end if matrix make true
						}  //end else if some parameters are given.
					}    //end the commang string checking 
					else{      //when every thing is not understandable and no command and no matrix making is possible then.  logic for assignment operator.
						theTwoResultStrings x = SplitString (expression, '=');  //spliting the string on the basis of assignment operators.
						x.resultLHS = x.resultLHS.Trim ();
						x.resultRHS = x.resultRHS.Trim ();
						char[] first = x.resultLHS.ToCharArray ();
						if (x.resultLHS != x.resultRHS &&ifContain (expression, "=") && countOccurance (expression, '=') == 1 && !isNumeric(x.resultRHS) && ifContain(Alphabets,first[0].ToString())) // if there is a need of copying th variables
						{
							bool iFoundRHS = false;
							bool iFoundLHS = false;
							int indexLHS = 0; int indexRHS = 0;
							foreach (Expression exp in theExpressionList) {   // finding if both the variables occur in the database that can be manipulaed
								if (LookInto (exp, x.resultRHS) == true || lookIntoNumber(exp,x.resultRHS) == true) {
									iFoundRHS = true;
									indexRHS = theExpressionList.IndexOf(exp);
								} else if (LookInto (exp, x.resultLHS) == true || lookIntoNumber(exp,x.resultLHS)== true) {
									iFoundLHS = true; indexLHS = theExpressionList.IndexOf (exp);
								}
							}
							if (iFoundRHS) {   //if RHS variable is found 
								if (iFoundLHS) {    //if LHS variable is also found 
									if (theExpressionList [indexRHS].getExpressionType () == 1) {          // if the expression if RHS is a matrix.
										Matrix RHS = new Matrix (theExpressionList [indexRHS].getMatrix ());
										RHS.setTag (x.resultLHS);
										int t = theExpressionList [indexLHS].getExpressionType ();
										theExpressionList [indexLHS].setMatrix (RHS);
										Expression Alert = new Expression ();
										if(t == 1)
											Alert.setAlertMessage ("Information: The variable (matrix) \"" + x.resultLHS + "\" already existed and so I have overwritten it.");
										else if(t == 3)
											Alert.setAlertMessage ("Information: The variable (number) \"" + x.resultLHS + "\" already existed and so I have overwritten it.");
										theExpressionList.Add (Alert);
									}
									else if (theExpressionList [indexRHS].getExpressionType () == 3)  // if the rhs expression is number
									{
										Number num = new Number (theExpressionList[indexRHS].getNumberExpression());
										num.setTag (x.resultLHS);
										int t = theExpressionList [indexLHS].getExpressionType ();
										theExpressionList [indexLHS].setNumberExpression (num);
										Expression Alert = new Expression ();
										if(t == 1)
											Alert.setAlertMessage ("Information: The variable (matrix) \"" + x.resultLHS + "\" already existed and so I have overwritten it.");
										else if(t == 3)
											Alert.setAlertMessage ("Information: The variable (number) \"" + x.resultLHS + "\" already existed and so I have overwritten it.");
										theExpressionList.Add (Alert);
									}
								} else {   //if Lhs variable is not found 
									if (theExpressionList [indexRHS].getExpressionType () == 1) {  //if the rhs is a matrix.
										Matrix LHS = new Matrix (theExpressionList [indexRHS].getMatrix ());
										LHS.setTag (x.resultLHS);
										Expression theNewMat = new Expression ();
										theNewMat.setMatrix (LHS);
										theExpressionList.Add (theNewMat);
									} else if(theExpressionList [indexRHS].getExpressionType () == 3){   //if the rhs is a number.
										Number LHS = new Number(theExpressionList[indexRHS].getNumberExpression());
										LHS.setTag (x.resultLHS);
										Expression theNewNumber = new Expression ();
										theNewNumber.setNumberExpression (LHS);
										theExpressionList.Add (theNewNumber);
									}
								}     // end if variable on lhs is not found
							} else {     //else if rhs if not found
								Expression err = new Expression ();
								err.setErrorMessage ("The variable \"" + x.resultRHS + "\" was not found. Invalid expression.");
								theExpressionList.Add (err);
							}
						} else {       // when all it is totaly ambigous to know what the hell is being given.
							if (x.resultLHS == x.resultRHS) {   // there is a self-assignment.
								allReadyExistAndPrinted = true;
								bool f = false;
								foreach(Expression x1 in theExpressionList){
									if(LookInto(x1,x.resultLHS) == true || lookIntoNumber(x1,x.resultRHS) == true)
									{
										f = true;
										break;
									}
								}
								if (!f) {      // if the variable doesnot exist. 
									Console.WriteLine ("The variable does not exist, currently.");
								}
							}
							else {
								Expression err = new Expression ();
								err.setErrorMessage ("I cannot understand the expression you entered.");
								theExpressionList.Add (err);
							}
						}  //end else 
					} //end else for every thign is ambigous.
				}// end else found==false.
			}  //end else after discreet matrix making

		}     //method for processing the string.


		private theTwoResultStrings SplitString(string exp, char atChar)
		{
			theTwoResultStrings x1;
			x1.resultLHS = "";
			x1.resultRHS = "";
			if(ifContain(exp,atChar.ToString()))
			{
				int transition = 0;
				foreach (char x in exp) {
					if(transition == 0 && x !='=')
						x1.resultLHS += x.ToString ();
					if (transition == 1)
						x1.resultRHS += x.ToString ();
					if (x == '=') {
						transition = 1;
					}
				}
			}
			return x1;
		}
		private bool ifContain(string myString, string toFind)
		{
			bool decision = false ;
			decision = myString.Contains (toFind);
			return decision;
		}

		static int SACount = 0;   //counter for self assigned variables matrices.
		static int IdentityCount = 0;   //counter for naming self made identity matrices
		static int SNACount = 0; // counter for self assigned numbers.

		/*This function interprets a given string in to a matrix with a tag*/
		private Matrix matrixWithTagProcess(string exp)
		{
			int equalOccur = exp.IndexOf ("=");    //sees the position of the occurance of the "="
			char[] characterArray = exp.ToCharArray ();   // converting exp into char array
			char ender = characterArray [0];
			int counter = 0;   //the counter and ender
			string theMatrixTag = "";   // the dumy string to store a matrix tag.
			while (ender != '=') {      // while loop to extract the tag of the matrix
				theMatrixTag = theMatrixTag + ender.ToString ();  
				counter++;
				ender = characterArray [counter];
			}       //end while loop to extract the tag.
			theMatrixTag = theMatrixTag.Trim ();    //removes the unneeded spaces and things like that
			string matrixString = exp.Substring (equalOccur + 1);  //extractes the matrix string form the given string.
			matrixString = matrixString.Trim ();  // removes the exesscive spaces and things like this 
			char[] matChar = matrixString.ToCharArray ();    
			char endChar = '[';  
			int count = 0;
			string dumy = ""; 
			string myCounting = "0123456789.-";
			List<string> bat = new List<string> ();  //a list for every chunk of numbers
			while (endChar != ']') { // while loop to extract the elements for the matrix 
				count++;
				endChar = matChar [count];
				if (ifContain(myCounting, endChar.ToString())) {  //neglecting uncessary things.
					dumy += endChar;
				} else if (endChar == ';') {  //transition for rows 
					if (string.IsNullOrWhiteSpace (dumy) || string.IsNullOrEmpty (dumy)) {
						dumy = "";
						dumy += endChar;
						bat.Add (dumy);
						dumy = "";
					} else {
						bat.Add (dumy);
						dumy = "";
						dumy += endChar;
						bat.Add (dumy);
						dumy = "";
					}
				}      //end transition for rows.
				else {
					if (string.IsNullOrWhiteSpace (dumy) || string.IsNullOrEmpty (dumy))
						dumy = "";
					else {
						bat.Add (dumy);
						dumy = "";
					}
				}
			}	   //end while loop to extract matrix elements.
			int rows = 0, columns = 0; 
			int greatest = 0;
			foreach (string num in bat) {  //foreach loop to set the number of rows and columns
				if (num == ";") {
					if (greatest < columns) {
						greatest = columns;
					} 
					columns = 0;
					rows++;
				} else
					columns++;
			}// end foreach loop.
			if (columns > greatest)
				greatest = columns;
			if (greatest == 0)
				greatest = columns;  // end of logic to set the matrix number of rows and columns
			int col=0;
			foreach(string num in bat)
			{
				if(num == ";")
				{if(col != greatest)
					{
						errorMessage = "Alert!!! There were some postions in the matrix that you left undefined. So, I have set them to zero.";  
					}
					col=0;
				}
				else
					col++;
			}
			if(col != greatest)
			{
				errorMessage = "Alert!!! There were some postions in the matrix that you left undefined. So, I have set them to zero.";
			}
			Matrix theMatrix = new Matrix (rows+1, greatest); //creation of the matrix.
			theMatrix.Zero ();   //make the matrix zero
			if(string.IsNullOrWhiteSpace(theMatrixTag))    //if some variable name is not given.
			{
				theMatrixTag = "M"+SACount.ToString();
				SACount++;
			}
			theMatrix.setTag (theMatrixTag);   //sets the tag of the matrix.
			rows = 0; columns = 0;    //reinitialize the rows and columns after seting.
			foreach(string num in bat){   //sets the matrix values
				if (num == ";") {
					rows++;
					columns = 0;
				} else {
					theMatrix.setElement (double.Parse (num), rows+1, columns+1);
					columns++;
				}
			}    // ends the matrix values setter
			if (SearchForSameTagCaseMatrix(theMatrix, theMatrixTag, errorMessage) == false)
			{
				Expression mat = new Expression();
				mat.setMatrix (theMatrix);
				theExpressionList.Add(mat);
				countExp++;
			}
			return theMatrix;		// returns the matrix after making it.
		}     //end function to convert the string to the matrix.

		private bool SearchForSameTagCaseMatrix(Matrix mat , string tag, string error)
		{
			bool thereExists = false;
			foreach(Expression x in theExpressionList)
			{ 

				if(x.getExpressionType()==1)
				{  
					if(x.getMatrix().getTag() == tag)
					{
						allReadyExistAndPrinted = true;
						Console.WriteLine("\n"+ error + "\n");
						errorMessage = "";
						Console.WriteLine("Information: A matrix with a same name already exists. So, I have overwritten it with the new " +
							"matrix you entered.");
						x.setMatrix(mat);
						thereExists = true;
						break;
					}
				}
				else if(x.getExpressionType()==3)
				{  
					if(x.getNumberExpression().getTag() == tag)
					{
						allReadyExistAndPrinted = true;
						Console.WriteLine("\n"+ error + "\n");
						errorMessage = "";
						Console.WriteLine("Information: A number with a same name already exists. So, I have overwritten it with the new " +
							"matrix you entered.");
						x.setMatrix(mat);
						thereExists = true;
						break;
					}
				}

			}
			return thereExists;
		}

		private bool SearchForSameTagCaseNumber(Number num, string tag)
		{
			bool thereExists = false;
			foreach(Expression x in theExpressionList)
			{ 

				if(x.getExpressionType()==1)
				{  
					if(x.getMatrix().getTag() == tag)
					{
						allReadyExistAndPrinted = true;
						Console.WriteLine("Information: A matrix with a same name already exists. So, I have overwritten it with the new " +
							"number you entered.");
						x.setNumberExpression(num);
						thereExists = true;
						break;
					}
				}
				else if(x.getExpressionType()==3)
				{  
					if(x.getNumberExpression().getTag() == tag)
					{
						allReadyExistAndPrinted = true;
						Console.WriteLine("Information: A number with a same name already exists. So, I have overwritten it with the new " +
							"number you entered.");
						x.setNumberExpression(num);
						thereExists = true;
						break;
					}
				}

			}
			return thereExists;
		}



	}          //end class StringEnumerator

	public class Expression{
		protected Matrix theMatrix;
		protected string theErrorMessage = "";
		protected int expressionType = 0;
		private string tag = "";
		private Number numberExpression;
		public int getExpressionType(){return expressionType;}
		public Matrix getMatrix(){return theMatrix;}
		public string getErrorMessage(){return theErrorMessage;}
		public Number getNumberExpression(){return numberExpression;}
		public void setTag(string theTag){if(expressionType != 2)tag = theTag;}
		public string getTag(){return tag;}

		public Expression(Expression obj)
		{
			theMatrix = obj.theMatrix;
			numberExpression = obj.numberExpression;
			theErrorMessage = obj.theErrorMessage;
			expressionType = obj.expressionType;
			tag = obj.tag;
		}
		public Expression(){}

		public void setNumberExpression( Number incomingExp)
		{
			setTag (incomingExp.getTag ());
			expressionType = 3;
			numberExpression = incomingExp;
		}
		public void setMatrix(Matrix incomingExp)
		{
			setTag (incomingExp.getTag ());
			expressionType = 1;
			theMatrix = incomingExp;
		}
		public void setErrorMessage(string incomingMessage)
		{
			setTag ("ErrorMessage");
			expressionType = 2;
			theErrorMessage = incomingMessage;
		}

		public void setAlertMessage(string incomingExp)
		{
			setTag ("AlertMessage");
			expressionType = 2;
			theErrorMessage = incomingExp;
		}

		public void setExpression(Expression obj)
		{
			theMatrix = obj.getMatrix ();
			numberExpression = obj.getNumberExpression ();
			expressionType = obj.getExpressionType ();
			tag = obj.getTag ();
		}
	}

	class ExpressionPrinter
	{
		private Expression exp =  new Expression();
		private int expType = 0;
		public ExpressionPrinter(Expression e){exp = e;}
		private void setType()
		{
			expType = exp.getExpressionType ();
		}
		public void Printer()
		{
			setType ();
			if (expType == 1) {
				exp.getMatrix ().consoleOutput ();
			}
			else if (expType == 2) {
				Console.WriteLine (exp.getErrorMessage ());
			}
			else if (expType == 3) {
				exp.getNumberExpression ().consolePrintNumber ();
			}
		}

	}


}

