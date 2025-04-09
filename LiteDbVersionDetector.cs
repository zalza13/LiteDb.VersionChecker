using System.Text;

namespace LiteDBVersionChecker
{
    public static class LiteDbVersionDetector
    {
        private const int BufferSize = 1024;
        private const string HEADER_INFO = "** This is a LiteDB file **";

        // LiteDB 4 offsets
        private const int HEADER4_OFFSET = 25;
        private const int VERSION4_OFFSET = 52;
        private const byte VERSION4 = 7;

        // LiteDB 5 offsets
        private const int HEADER5_OFFSET = 32;
        private const int VERSION5_OFFSET = 59;
        private const byte VERSION5 = 8;

        public static string DetectLiteDbVersion(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Database file not found.", filePath);

            byte[] buffer = new byte[BufferSize];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                stream.Read(buffer, 0, BufferSize);

                if (IsLiteDb4(buffer)) return "LiteDB 4";
                if (IsLiteDb5(buffer)) return "LiteDB 5";

                return "No LiteDB file";
            }
        }

        private static bool IsLiteDb4(byte[] buffer)
        {
            string header = Encoding.UTF8.GetString(buffer, HEADER4_OFFSET, HEADER_INFO.Length);
            return header == HEADER_INFO && buffer[VERSION4_OFFSET] == VERSION4;
        }

        private static bool IsLiteDb5(byte[] buffer)
        {
            string header = Encoding.UTF8.GetString(buffer, HEADER5_OFFSET, HEADER_INFO.Length);
            byte version = buffer[VERSION5_OFFSET];

            // Also check if it's an encrypted LiteDB file (only supported in v5+)
            return (header == HEADER_INFO && version == VERSION5) || buffer[0] == 1;
        }
    }
}
