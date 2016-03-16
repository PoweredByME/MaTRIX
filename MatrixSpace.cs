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
		Matrix OutMat;
		public StringEnumerator(string theExpression = " ") 
		{
			expression = theExpression;
			eBatch = new List<string>();
			theMatrixList = new List<Matrix> ();
			OutMat = new Matrix (1, 1);
			OutMat.Zero ();
		}
        
		 public void PrintExpression()
		{
			Process ();
			ExpressionPrinter e = new ExpressionPrinter (theExpressionList [count]);
			e.Printer ();
			count++;
		}

		public void setString(string newString){expression = newString;} 

		public Matrix getMatrix()
		{
			Process ();
			return OutMat;
		}

		public void Process()
		{
			if (string.IsNullOrWhiteSpace (expression)) { 
				Console.WriteLine ("The string is empty");
			} else if (ifContain (expression, "=") && ifContain (expression, "[") && ifContain (expression, "]") && !(ifContain(expression,"+")
				&&ifContain(expression,"-"))) {
				expression = expression.Trim ();
				int lastCharacter = expression.IndexOf ("]");
				int explenght = expression.Length;
				if (explenght == lastCharacter + 1) {
					OutMat = matrixWithTagProcess (expression);
					theMatrixList.Add (OutMat);
				} else
					Console.WriteLine ("Incompatiable format... please write a compatible formate");
			} else {
				Console.WriteLine ("Unable to understand the given statement. Sorry. ");
			}
				
		}

		private bool ifContain(string myString, string toFind)
		{
			bool decision = false ;
			decision = myString.Contains (toFind);
			return decision;
		}

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
			List<string> bat = new List<string> ();  //a list for every chunk of numbers
			while (endChar != ']') { // while loop to extract the elements for the matrix 
				count++;
				endChar = matChar [count];
				if (endChar != ']' && endChar != ' ' && endChar != ';') {  //neglecting uncessary things.
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
			Matrix theMatrix = new Matrix (rows+1, greatest); //creation of the matrix.
			theMatrix.Zero ();   //make the matrix zero
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
			Expression mat = new Expression();
			mat.setMatrix (theMatrix);
			theExpressionList.Add(mat);
			countExp++;
			return theMatrix;		// returns the matrix after making it.
		}     //end function to convert the string to the matrix.
			

	}          //end class StringEnumerator

    class Expression{
		protected Matrix theMatrix;
		protected int expressionType = 0;
	    
		public int getExpressionType(){return expressionType;}
		public Matrix getMatrix(){return theMatrix;}
		public void setMatrix(Matrix incomingExp)
		{
			expressionType = 1;
			theMatrix = incomingExp;
		}
	}

	class ExpressionPrinter : Expression
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
		}

	}

}

