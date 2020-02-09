using System;

namespace OrderService
{
    public class LogicalRule
    {
        public OperationTypes OperationType { get; set; }
        
        public int ParameterToCompareWith { get; set; }

        public bool IsTrue(int parameter1)
        {
            switch (OperationType)
            {
                case OperationTypes.GreaterThen:
                    return parameter1 > ParameterToCompareWith;
                
                case OperationTypes.Equal:
                    return parameter1 == ParameterToCompareWith;
                
                default:
                    return false;
            }            
        }
    }
}