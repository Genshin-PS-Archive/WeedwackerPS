using csFastFloat;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Weedwacker.GameServer.Data
{
    public static class TsvParserSupport
    {
        public static long LongParseFast(ReadOnlySpan<char> span)
        {
            if (span.IsEmpty)
                return 0;

            bool isNegative = span[0] == '-';

            long result = 0;
            for (int i = isNegative ? 1 : 0; i < span.Length; i++)
            {
                char c = span[i];
                if (!char.IsDigit(c))
#if DEBUG
                    throw new InvalidOperationException("Number contains invalid characters.");
#else
                    continue;
#endif

                result = 10 * result + (c - 48);
            }

            if (isNegative)
                result = -result;

            return result;
        }

        public static ulong UlongParseFast(ReadOnlySpan<char> span)
        {
            ulong result = 0;
            foreach (char c in span)
            {
                if (!char.IsDigit(c))
                {
                    if (c == ' ' && result == 0) continue; //leading whitespace
                    throw new InvalidOperationException("Number contains invalid characters.");
                }
                result = 10 * result + (ulong)(c - 48);
            }

            return result;
        }

        public static float FloatParseFast(ReadOnlySpan<char> span)
        {
            if (span.IsEmpty)
                return 0;

            return FastFloatParser.ParseFloat(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf(ref char searchSpace, char value, int start, int length)
        {
            nint index = start; // Use nint for arithmetic to avoid unnecessary 64->32->64 truncations

            while (length >= 8)
            {
                length -= 8;

                if (value == Unsafe.Add(ref searchSpace, index))
                    goto Found;
                if (value == Unsafe.Add(ref searchSpace, index + 1))
                    goto Found1;
                if (value == Unsafe.Add(ref searchSpace, index + 2))
                    goto Found2;
                if (value == Unsafe.Add(ref searchSpace, index + 3))
                    goto Found3;
                if (value == Unsafe.Add(ref searchSpace, index + 4))
                    goto Found4;
                if (value == Unsafe.Add(ref searchSpace, index + 5))
                    goto Found5;
                if (value == Unsafe.Add(ref searchSpace, index + 6))
                    goto Found6;
                if (value == Unsafe.Add(ref searchSpace, index + 7))
                    goto Found7;

                index += 8;
            }

            if (length >= 4)
            {
                length -= 4;

                if (value == Unsafe.Add(ref searchSpace, index))
                    goto Found;
                if (value == Unsafe.Add(ref searchSpace, index + 1))
                    goto Found1;
                if (value == Unsafe.Add(ref searchSpace, index + 2))
                    goto Found2;
                if (value == Unsafe.Add(ref searchSpace, index + 3))
                    goto Found3;

                index += 4;
            }

            while (length > 0)
            {
                if (value == Unsafe.Add(ref searchSpace, index))
                    goto Found;

                index += 1;
                length--;
            }

        Found: // Workaround for https://github.com/dotnet/runtime/issues/8795
            return (int)index;
        Found1:
            return (int)(index + 1);
        Found2:
            return (int)(index + 2);
        Found3:
            return (int)(index + 3);
        Found4:
            return (int)(index + 4);
        Found5:
            return (int)(index + 5);
        Found6:
            return (int)(index + 6);
        Found7:
            return (int)(index + 7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfAny(ref char searchSpace, char value0, char value1, int start, int length)
            => IndexOfAny(ref Unsafe.As<char, short>(ref searchSpace), Unsafe.As<char, short>(ref value0), Unsafe.As<char, short>(ref value1), start, length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfAny(ref char searchSpace, char value0, char value1, char value2, int start, int length)
            => IndexOfAny(ref Unsafe.As<char, short>(ref searchSpace), Unsafe.As<char, short>(ref value0), Unsafe.As<char, short>(ref value1), Unsafe.As<char, short>(ref value2), start, length);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static int IndexOfAny<TValue>(ref TValue searchSpace, TValue value0, TValue value1, int start, int length)
            where TValue : struct, INumber<TValue>
        {
            Debug.Assert(length >= 0, "Expected non-negative length");
            Debug.Assert(value0 is byte or short or int or long, "Expected caller to normalize to one of these types");

            if (!Vector128.IsHardwareAccelerated || length < Vector128<TValue>.Count)
            {
                nint offset = start;
                TValue lookUp;

                if (typeof(TValue) == typeof(byte)) // this optimization is beneficial only to byte
                {
                    while (length >= 8)
                    {
                        length -= 8;

                        ref TValue current = ref Unsafe.Add(ref searchSpace, offset);
                        lookUp = current;
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset;
                        lookUp = Unsafe.Add(ref current, 1);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 1;
                        lookUp = Unsafe.Add(ref current, 2);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 2;
                        lookUp = Unsafe.Add(ref current, 3);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 3;
                        lookUp = Unsafe.Add(ref current, 4);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 4;
                        lookUp = Unsafe.Add(ref current, 5);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 5;
                        lookUp = Unsafe.Add(ref current, 6);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 6;
                        lookUp = Unsafe.Add(ref current, 7);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 7;

                        offset += 8;
                    }
                }

                while (length >= 4)
                {
                    length -= 4;

                    ref TValue current = ref Unsafe.Add(ref searchSpace, offset);
                    lookUp = current;
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset;
                    lookUp = Unsafe.Add(ref current, 1);
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 1;
                    lookUp = Unsafe.Add(ref current, 2);
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 2;
                    lookUp = Unsafe.Add(ref current, 3);
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset + 3;

                    offset += 4;
                }

                while (length > 0)
                {
                    length -= 1;

                    lookUp = Unsafe.Add(ref searchSpace, offset);
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1)) return (int)offset;

                    offset += 1;
                }

                return (int)offset;
            }

            if (Vector256.IsHardwareAccelerated && length >= Vector256<TValue>.Count)
            {
                Vector256<TValue> equals, current, values0 = Vector256.Create(value0), values1 = Vector256.Create(value1);
                ref TValue currentSearchSpace = ref Unsafe.Add(ref searchSpace, start);
                ref TValue oneVectorAwayFromEnd = ref Unsafe.Add(ref Unsafe.Add(ref searchSpace, start), length - Vector256<TValue>.Count);

                // Loop until either we've finished all elements or there's less than a vector's-worth remaining.
                do
                {
                    current = Vector256.LoadUnsafe(ref currentSearchSpace);
                    equals = DontNegate<TValue>.NegateIfNeeded(Vector256.Equals(values0, current) | Vector256.Equals(values1, current));
                    if (equals == Vector256<TValue>.Zero)
                    {
                        currentSearchSpace = ref Unsafe.Add(ref currentSearchSpace, Vector256<TValue>.Count);
                        continue;
                    }

                    return ComputeFirstIndex(ref searchSpace, ref currentSearchSpace, equals);
                }
                while (!Unsafe.IsAddressGreaterThan(ref currentSearchSpace, ref oneVectorAwayFromEnd));

                // If any elements remain, process the last vector in the search space.
                if ((uint)length % Vector256<TValue>.Count != 0)
                {
                    current = Vector256.LoadUnsafe(ref oneVectorAwayFromEnd);
                    equals = DontNegate<TValue>.NegateIfNeeded(Vector256.Equals(values0, current) | Vector256.Equals(values1, current));
                    if (equals != Vector256<TValue>.Zero)
                    {
                        return ComputeFirstIndex(ref searchSpace, ref oneVectorAwayFromEnd, equals);
                    }
                }
            }
            else
            {
                Vector128<TValue> equals, current, values0 = Vector128.Create(value0), values1 = Vector128.Create(value1);
                ref TValue currentSearchSpace = ref Unsafe.Add(ref searchSpace, start);
                ref TValue oneVectorAwayFromEnd = ref Unsafe.Add(ref Unsafe.Add(ref searchSpace, start), length - Vector128<TValue>.Count);

                // Loop until either we've finished all elements or there's less than a vector's-worth remaining.
                do
                {
                    current = Vector128.LoadUnsafe(ref currentSearchSpace);
                    equals = DontNegate<TValue>.NegateIfNeeded(Vector128.Equals(values0, current) | Vector128.Equals(values1, current));
                    if (equals == Vector128<TValue>.Zero)
                    {
                        currentSearchSpace = ref Unsafe.Add(ref currentSearchSpace, Vector128<TValue>.Count);
                        continue;
                    }

                    return ComputeFirstIndex(ref searchSpace, ref currentSearchSpace, equals);
                }
                while (!Unsafe.IsAddressGreaterThan(ref currentSearchSpace, ref oneVectorAwayFromEnd));

                // If any elements remain, process the first vector in the search space.
                if ((uint)length % Vector128<TValue>.Count != 0)
                {
                    current = Vector128.LoadUnsafe(ref oneVectorAwayFromEnd);
                    equals = DontNegate<TValue>.NegateIfNeeded(Vector128.Equals(values0, current) | Vector128.Equals(values1, current));
                    if (equals != Vector128<TValue>.Zero)
                    {
                        return ComputeFirstIndex(ref searchSpace, ref oneVectorAwayFromEnd, equals);
                    }
                }
            }

            return length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static int IndexOfAny<TValue>(ref TValue searchSpace, TValue value0, TValue value1, TValue value2, int start, int length)
            where TValue : struct, INumber<TValue>
        {
            Debug.Assert(length >= 0, "Expected non-negative length");
            Debug.Assert(value0 is byte or short or int or long, "Expected caller to normalize to one of these types");

            if (!Vector128.IsHardwareAccelerated || length < Vector128<TValue>.Count)
            {
                nint offset = start;
                TValue lookUp;

                if (typeof(TValue) == typeof(byte)) // this optimization is beneficial only to byte
                {
                    while (length >= 8)
                    {
                        length -= 8;

                        ref TValue current = ref Unsafe.Add(ref searchSpace, offset);
                        lookUp = current;
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset;
                        lookUp = Unsafe.Add(ref current, 1);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 1;
                        lookUp = Unsafe.Add(ref current, 2);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 2;
                        lookUp = Unsafe.Add(ref current, 3);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 3;
                        lookUp = Unsafe.Add(ref current, 4);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 4;
                        lookUp = Unsafe.Add(ref current, 5);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 5;
                        lookUp = Unsafe.Add(ref current, 6);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 6;
                        lookUp = Unsafe.Add(ref current, 7);
                        if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 7;

                        offset += 8;
                    }
                }

                while (length >= 4)
                {
                    length -= 4;

                    ref TValue current = ref Unsafe.Add(ref searchSpace, offset);
                    lookUp = current;
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset;
                    lookUp = Unsafe.Add(ref current, 1);
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 1;
                    lookUp = Unsafe.Add(ref current, 2);
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 2;
                    lookUp = Unsafe.Add(ref current, 3);
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset + 3;

                    offset += 4;
                }

                while (length > 0)
                {
                    length -= 1;

                    lookUp = Unsafe.Add(ref searchSpace, offset);
                    if (DontNegate<TValue>.NegateIfNeeded(lookUp == value0 || lookUp == value1 || lookUp == value2)) return (int)offset;

                    offset += 1;
                }

                return (int)offset;
            }

            if (Vector256.IsHardwareAccelerated && length >= Vector256<TValue>.Count)
            {
                Vector256<TValue> equals, current, values0 = Vector256.Create(value0), values1 = Vector256.Create(value1), values2 = Vector256.Create(value2);
                ref TValue currentSearchSpace = ref Unsafe.Add(ref searchSpace, start);
                ref TValue oneVectorAwayFromEnd = ref Unsafe.Add(ref Unsafe.Add(ref searchSpace, start), length - Vector256<TValue>.Count);

                // Loop until either we've finished all elements or there's less than a vector's-worth remaining.
                do
                {
                    current = Vector256.LoadUnsafe(ref currentSearchSpace);
                    equals = DontNegate<TValue>.NegateIfNeeded(Vector256.Equals(values0, current) | Vector256.Equals(values1, current) | Vector256.Equals(values2, current));
                    if (equals == Vector256<TValue>.Zero)
                    {
                        currentSearchSpace = ref Unsafe.Add(ref currentSearchSpace, Vector256<TValue>.Count);
                        continue;
                    }

                    return ComputeFirstIndex(ref searchSpace, ref currentSearchSpace, equals);
                }
                while (!Unsafe.IsAddressGreaterThan(ref currentSearchSpace, ref oneVectorAwayFromEnd));

                // If any elements remain, process the last vector in the search space.
                if ((uint)length % Vector256<TValue>.Count != 0)
                {
                    current = Vector256.LoadUnsafe(ref oneVectorAwayFromEnd);
                    equals = DontNegate<TValue>.NegateIfNeeded(Vector256.Equals(values0, current) | Vector256.Equals(values1, current) | Vector256.Equals(values2, current));
                    if (equals != Vector256<TValue>.Zero)
                    {
                        return ComputeFirstIndex(ref searchSpace, ref oneVectorAwayFromEnd, equals);
                    }
                }
            }
            else
            {
                Vector128<TValue> equals, current, values0 = Vector128.Create(value0), values1 = Vector128.Create(value1), values2 = Vector128.Create(value2);
                ref TValue currentSearchSpace = ref Unsafe.Add(ref searchSpace, start);
                ref TValue oneVectorAwayFromEnd = ref Unsafe.Add(ref Unsafe.Add(ref searchSpace, start), length - Vector128<TValue>.Count);

                // Loop until either we've finished all elements or there's less than a vector's-worth remaining.
                do
                {
                    current = Vector128.LoadUnsafe(ref currentSearchSpace);
                    equals = DontNegate<TValue>.NegateIfNeeded(Vector128.Equals(values0, current) | Vector128.Equals(values1, current) | Vector128.Equals(values2, current));
                    if (equals == Vector128<TValue>.Zero)
                    {
                        currentSearchSpace = ref Unsafe.Add(ref currentSearchSpace, Vector128<TValue>.Count);
                        continue;
                    }

                    return ComputeFirstIndex(ref searchSpace, ref currentSearchSpace, equals);
                }
                while (!Unsafe.IsAddressGreaterThan(ref currentSearchSpace, ref oneVectorAwayFromEnd));

                // If any elements remain, process the first vector in the search space.
                if ((uint)length % Vector128<TValue>.Count != 0)
                {
                    current = Vector128.LoadUnsafe(ref oneVectorAwayFromEnd);
                    equals = DontNegate<TValue>.NegateIfNeeded(Vector128.Equals(values0, current) | Vector128.Equals(values1, current) | Vector128.Equals(values2, current));
                    if (equals != Vector128<TValue>.Zero)
                    {
                        return ComputeFirstIndex(ref searchSpace, ref oneVectorAwayFromEnd, equals);
                    }
                }
            }

            return length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ComputeFirstIndex<T>(ref T searchSpace, ref T current, Vector128<T> equals) where T : struct
        {
            uint notEqualsElements = equals.ExtractMostSignificantBits();
            int index = BitOperations.TrailingZeroCount(notEqualsElements);
            return index + (int)(Unsafe.ByteOffset(ref searchSpace, ref current) / Unsafe.SizeOf<T>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ComputeFirstIndex<T>(ref T searchSpace, ref T current, Vector256<T> equals) where T : struct
        {
            uint notEqualsElements = equals.ExtractMostSignificantBits();
            int index = BitOperations.TrailingZeroCount(notEqualsElements);
            return index + (int)(Unsafe.ByteOffset(ref searchSpace, ref current) / Unsafe.SizeOf<T>());
        }

        private readonly struct DontNegate<T> where T : struct
        {
            public static bool NegateIfNeeded(bool equals) => equals;
            public static Vector128<T> NegateIfNeeded(Vector128<T> equals) => equals;
            public static Vector256<T> NegateIfNeeded(Vector256<T> equals) => equals;
        }
    }
}
