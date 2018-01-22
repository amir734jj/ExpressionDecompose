using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionDecompose
{
    public class ExpressionDecompose<T>
    {
        private List<AtomicExpression> _listAtomicExpressions;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExpressionDecompose()
        {
            _listAtomicExpressions = new List<AtomicExpression>();
        }

        /// <summary>
        /// Decomposes an expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public void Decompose(Expression<Func<T, bool>> expr)
        {
            Dissect(expr);
        }

        /// <summary>
        /// Tries to decompose a lambda expression recursively
        /// </summary>
        /// <param name="inputExpression"></param>
        /// <param name="setOperation"></param>
        private void Dissect(Expression inputExpression, ExpressionType setOperation = ExpressionType.Add)
        {
            inputExpression = inputExpression is LambdaExpression lambdaExpression ? lambdaExpression.Body : inputExpression;

            BinaryExpression operation = (BinaryExpression) inputExpression;
            ExpressionType binaryOperator = operation.NodeType;
            var processed = false;

            if (operation.Left is BinaryExpression leftExpression && leftExpression.Left is MemberExpression && leftExpression.Right is ConstantExpression)
            {
                _listAtomicExpressions.Add(new AtomicExpression(leftExpression.Left, leftExpression.NodeType, leftExpression.Right, setOperation));
                processed = true;
            }
            else if (!(operation.Left is MemberExpression))
            {
                processed = true;
                Dissect(operation.Left, binaryOperator);
            }
            
            if (operation.Right is BinaryExpression rightExpression && rightExpression.Left is MemberExpression && rightExpression.Right is ConstantExpression)
            {
                processed = true;
                _listAtomicExpressions.Add(new AtomicExpression(rightExpression.Left, rightExpression.NodeType, rightExpression.Right, setOperation));
            }
            else if (!(operation.Right is ConstantExpression))
            {
                processed = true;
                Dissect(operation.Right, binaryOperator);
            }
            
            if (inputExpression is BinaryExpression inputExpressionAsBinaryExpression && !processed)
            {
                _listAtomicExpressions.Add(new AtomicExpression(inputExpressionAsBinaryExpression.Left, inputExpressionAsBinaryExpression.NodeType, inputExpressionAsBinaryExpression.Right, setOperation));
            }
        }

        // returns decomposed results
        public List<AtomicExpression> AtomicExpressions => _listAtomicExpressions;

        // resets lists of results
        public void Reset() => _listAtomicExpressions = new List<AtomicExpression>();
    }
}