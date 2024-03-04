using System.Collections.Generic;

public static class Extensions
{
    public static bool TryPeek<T>(Stack<T> col, out T obj)
    {
#if NET_35_SUBSET
        if (col == null || col.Count == 0)
        {
            obj = default;
            return false;
        }
        else
        {
            obj = col.Peek();
            return true;
        }
#else
		return col.TryPeek(out obj);
#endif
    }
}