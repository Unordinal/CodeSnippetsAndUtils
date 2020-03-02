using System;

namespace CodeSnippetsAndUtils.ASCIITypes.SystemInternal
{
    internal interface ISpanFormattable
    {
        bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider);
    }
}
