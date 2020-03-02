using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnippetsAndUtils
{
    public struct DirtyType<T>
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                if (!Equals(this.value, value))
                {
                    this.value = value;
                    IsDirty = true;
                }
            }
        }
        public bool IsDirty { get; set; }
    }
}
