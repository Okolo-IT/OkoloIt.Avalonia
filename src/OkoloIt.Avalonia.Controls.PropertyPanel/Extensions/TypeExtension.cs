namespace OkoloIt.Avalonia.Controls.Helpers;

/// <summary>
/// Provides a set of static methods for checking types.
/// </summary>
public static class TypeExtension
{
    /// <summary>
    /// Gets a value indicating whether the current System.Type represents an boolean.
    /// </summary>
    /// <param name="type">Checked type.</param>
    /// <returns>
    /// <see langword="true"/> if the current System.Type represents an boolean; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool IsBool(this Type type)
    {
        return type == typeof(bool);
    }

    /// <summary>
    /// Gets a value indicating whether the current System.Type represents an text.
    /// </summary>
    /// <param name="type">Checked type.</param>
    /// <returns>
    /// <see langword="true"/> if the current System.Type represents an text; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool IsText(this Type type)
    {
        return type == typeof(string);
    }

    /// <summary>
    /// Gets a value indicating whether the current System.Type represents an <see cref="Guid"/>.
    /// </summary>
    /// <param name="type">Checked type.</param>
    /// <returns>
    /// <see langword="true"/> if the current System.Type represents an <see cref="Guid"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsGuid(this Type type)
    {
        return type == typeof(Guid);
    }

    /// <summary>
    /// Gets a value indicating whether the current System.Type represents an numeric.
    /// </summary>
    /// <param name="type">Checked type.</param>
    /// <returns>
    /// <see langword="true"/> if the current System.Type represents an numeric; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool IsNumeric(this Type type)
    {
        return type == typeof(int)
            || type == typeof(decimal)
            || type == typeof(uint)
            || type == typeof(long)
            || type == typeof(ulong)
            || type == typeof(short)
            || type == typeof(ushort)
            || type == typeof(byte)
            || type == typeof(sbyte)
            || type == typeof(float)
            || type == typeof(double);
    }

    /// <summary>
    /// Gets a value indicating whether the current System.Type represents an
    /// <see cref="DateTime"/>.
    /// </summary>
    /// <param name="type">Checked type.</param>
    /// <returns>
    /// <see langword="true"/> if the current System.Type represents an <see cref="DateTime"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsDateTime(this Type type)
    {
#if NET6_0_OR_GREATER
        return type == typeof(DateTime)
            || type == typeof(DateOnly)
            || type == typeof(TimeOnly);
#else
        return type == typeof(DateTime);
#endif
    }
}
