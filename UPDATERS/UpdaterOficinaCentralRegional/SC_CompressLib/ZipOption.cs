using System;
using System.Collections.Generic;
using System.Text;

using SC_CompressLib.Utils;
using SC_CompressLib.Zip;
using SC_CompressLib.Zlib;

namespace SC_CompressLib
{
    class ZipOption
    {
    }

    public class ExtractWorkerOptions
    {
        public string ExtractLocation;
        public ExtractExistingFileAction ExtractExisting;
        public bool OpenExplorer;
        public String Selection;
    }

    public class SaveWorkerOptions
    {
        public string ZipName;
        public string Selection;
        //public String DirInArchive;
        public string Encoding;
        public string Comment;
        public string Password;
        public string ExeOnUnpack;
        public string ExtractDirectory;
        public SelfExtractorFlavor ZipFlavor = SelfExtractorFlavor.DefaultZip;
        public CompressionLevel CompressionLevel = CompressionLevel.Level6;
        public EncryptionAlgorithm Encryption = EncryptionAlgorithm.WinZipAes256;
        public Zip64Option Zip64 = Zip64Option.Always;
        public ItemToAdd[] Entries;
    }

    public class ItemToAdd
    {
        public string LocalFileName;
        public string DirectoryInArchive;
        public string FileNameInArchive;
    }

}
