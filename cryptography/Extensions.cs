public static class Extensions
{
    public static string ToHex(this byte[] bytes)
    {
        return Convert.ToHexString(bytes).ToLower();
    }
}