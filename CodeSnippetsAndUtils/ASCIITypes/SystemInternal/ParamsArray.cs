namespace CodeSnippetsAndUtils.ASCIITypes.SystemInternal
{
    public readonly struct ParamsArray
    {
        private static readonly object?[] oneArgArray = new object?[1];
        private static readonly object?[] twoArgArray = new object?[2];
        private static readonly object?[] threeArgArray = new object?[3];

        private readonly object? arg0;
        private readonly object? arg1;
        private readonly object? arg2;

        private readonly object?[] args;

        public int Length => args.Length;

        public object? this[int index] => index == 0 ? arg0 : GetAtSlow(index);

        public ParamsArray(object? arg0)
        {
            this.arg0 = arg0;
            this.arg1 = null;
            this.arg2 = null;
            this.args = oneArgArray;
        }
        
        public ParamsArray(object? arg0, object? arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = null;
            this.args = twoArgArray;
        }
        
        public ParamsArray(object? arg0, object? arg1, object? arg2)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.args = threeArgArray;
        }
        
        public ParamsArray(object?[] args)
        {
            int len = args.Length;
            this.arg0 = len > 0 ? args[0] : null;
            this.arg1 = len > 1 ? args[1] : null;
            this.arg2 = len > 2 ? args[2] : null;
            this.args = args;
        }

        private object? GetAtSlow(int index)
        {
            if (index == 1) return arg1;
            if (index == 2) return arg2;
            return args[index];
        }
    }
}
