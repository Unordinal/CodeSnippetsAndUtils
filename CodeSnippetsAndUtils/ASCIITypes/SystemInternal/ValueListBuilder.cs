﻿using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CodeSnippetsAndUtils.ASCIITypes.SystemInternal
{
    public ref partial struct ValueListBuilder<T>
    {
        private Span<T> _span;
        private T[]? _arrayFromPool;
        private int _pos;

        public int Length
        {
            get => _pos;
            set
            {
                Debug.Assert(value >= 0);
                Debug.Assert(value <= _span.Length);
                _pos = value;
            }
        }

        public ref T this[int index]
        {
            get
            {
                Debug.Assert(index < _pos);
                return ref _span[index];
            }
        }

        public ValueListBuilder(Span<T> initialSpan)
        {
            _span = initialSpan;
            _arrayFromPool = null;
            _pos = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(T item)
        {
            int pos = _pos;
            if (pos >= _span.Length)
                Grow();

            _span[pos] = item;
            _pos = pos + 1;
        }

        public ReadOnlySpan<T> AsSpan()
        {
            return _span.Slice(0, _pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            if (_arrayFromPool != null)
            {
                ArrayPool<T>.Shared.Return(_arrayFromPool);
                _arrayFromPool = null;
            }
        }

        private void Grow()
        {
            T[] array = ArrayPool<T>.Shared.Rent(_span.Length * 2);

            bool success = _span.TryCopyTo(array);
            Debug.Assert(success);

            T[]? toReturn = _arrayFromPool;
            _span = _arrayFromPool = array;
            if (toReturn != null)
            {
                ArrayPool<T>.Shared.Return(toReturn);
            }
        }
    }
}
