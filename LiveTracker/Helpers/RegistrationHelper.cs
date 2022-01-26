using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LiveTracker.Helpers
{
    public static class RegistrationHelper
    {
        public const UInt32 programID = 442253253;

        [Flags]
        public enum FileSystemFeature : uint
        {
            // The file system supports case-sensitive file names.
            CaseSensitiveSearch = 1,
            // The file system preserves the case of file names when it places a name on disk.
            CasePreservedNames = 2,
            // The file system supports Unicode in file names as they appear on disk.
            UnicodeOnDisk = 4,
            // The file system preserves and enforces access control lists (ACL).
            PersistentACLS = 8,
            // The file system supports file-based compression.
            FileCompression = 0x10,
            // The file system supports disk quotas.
            VolumeQuotas = 0x20,
            // The file system supports sparse files.
            SupportsSparseFiles = 0x40,
            // The file system supports re-parse points.
            SupportsReparsePoints = 0x80,
            // The specified volume is a compressed volume, for example, a DoubleSpace volume.
            VolumeIsCompressed = 0x8000,
            // The file system supports object identifiers.
            SupportsObjectIDs = 0x10000,
            // The file system supports the Encrypted File System (EFS).
            SupportsEncryption = 0x20000,
            // The file system supports named streams.
            NamedStreams = 0x40000,
            // The specified volume is read-only.
            ReadOnlyVolume = 0x80000,
            // The volume supports a single sequential write.
            SequentialWriteOnce = 0x100000,
            // The volume supports transactions.
            SupportsTransactions = 0x200000,
        }

        // Declare the GetVolumeInformation API function.
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern static bool GetVolumeInformation(
          string RootPathName,
          StringBuilder VolumeNameBuffer,
          int VolumeNameSize,
          out UInt32 VolumeSerialNumber,
          out UInt32 MaximumComponentLength,
          out FileSystemFeature FileSystemFlags,
          StringBuilder FileSystemNameBuffer,
          int nFileSystemNameSize);

        // Simple encryption and decryption.
        public static UInt32 Encrypt(UInt32 seed, UInt32 value)
        {
            Random rand = new Random((int)seed);
            return (value ^ (UInt32)(UInt32.MaxValue * rand.NextDouble()));
        }

        /// <summary>
        /// Register the program
        /// </summary>
        public static bool Register(UInt32 productKey)
        {
            // leave if not a valid reg code
            if (Encrypt(programID, GetSerialNumber()) != productKey) return false;

            // update product key and registered value
            Tournament_Life.Properties.Settings.Default.ProductKey = productKey;
            Tournament_Life.Properties.Settings.Default.IsRegister = true;
            Tournament_Life.Properties.Settings.Default.Save();

            // registered
            return true;
        }

        /// <summary>
        /// true if registered
        /// </summary>
        /// <param name="productKey"></param>
        /// <returns></returns>
        public static bool IsRegistered()
        {
            //if (Tournament_Life.Properties.Settings.Default.IsRegister) return true;

            //return false;

            return true;
        }

        /// <summary>
        /// get drive serial number
        /// </summary>
        /// <returns></returns>
        public static UInt32 GetSerialNumber()
        {
            var volume_name = new StringBuilder(1024);
            var file_system_name = new StringBuilder(1024);
            UInt32 serial_number, max_component_length;
            FileSystemFeature file_system_flags;

            // Get the startup directory's drive letter.
            // Get the drive where the program is running.
            string drive_letter = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);

            // Get the information. If we fail, return the default value.
            if (GetVolumeInformation(drive_letter, volume_name,
                volume_name.Capacity, out serial_number,
                out max_component_length, out file_system_flags,
                file_system_name, file_system_name.Capacity) is false)
            {
                return 0;
            }

            return serial_number;
        }
    }
}
