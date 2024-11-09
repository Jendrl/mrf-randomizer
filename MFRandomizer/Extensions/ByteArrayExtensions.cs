namespace MFRandomizer.Extensions
{
    public static class ByteArrayExtensions
    {
        public static short ToShort(this byte[] data) => BitConverter.ToInt16(data, 0);
    }
}
