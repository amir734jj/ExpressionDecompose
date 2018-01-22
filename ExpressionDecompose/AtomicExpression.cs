using System.Linq.Expressions;

namespace ExpressionDecompose
{
    public class AtomicExpression
    {
        private readonly Expression _leftOperand;
        private readonly Expression _rightOperand;
        private readonly ExpressionType _operation;
        private readonly ExpressionType _setOperation;

        private readonly string _operandField;
        private readonly string _operandValue;

        public AtomicExpression(Expression leftOperand, ExpressionType operation, Expression rightOperand, ExpressionType setOperation = ExpressionType.Add)
        {
            _leftOperand = leftOperand;
            _operation = operation;
            _rightOperand = rightOperand;
            _setOperation = setOperation;

            _operandField = GetPropertyName(_leftOperand as MemberExpression);
            _operandValue = _rightOperand.ToString();
        }

        /// <summary>
        /// Gets propertyName given MemberExpression
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static string GetPropertyName(MemberExpression exp)
        {
            return exp.Member.Name;
        }
        
        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $@"'{_operandField}' {_operation} {_operandValue} ({_setOperation})";
        }
    }
}