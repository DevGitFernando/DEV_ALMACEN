// CommandLineSelfExtractorStub.cs
// ------------------------------------------------------------------
//
// Copyright (c)  2008, 2009 Dino Chiesa.  
// All rights reserved.
//
// This code module is part of DotNetZip, a zipfile class library.
//
// ------------------------------------------------------------------
//
// This code is licensed under the Microsoft Public License. 
// See the file License.txt for the license details.
// More info on: http://dotnetzip.codeplex.com
//
// ------------------------------------------------------------------
//
// last saved (in emacs): 
// Time-stamp: <2009-July-03 15:02:41>
//
// ------------------------------------------------------------------
//
// This is a the source module that implements the stub of a
// command-line self-extracting Zip archive - the code included in all
// command-line SFX files.  This code is included as a resource into the
// DotNetZip DLL, and then is compiled at runtime when a SFX is saved. 
//
// ------------------------------------------------------------------


namespace SC_CompressLib.Zip
{

    // include the using statements inside the namespace decl, because
    // source code will be concatenated together before compilation. 
    using System;
    using System.Reflection;
    using System.Resources;
    using System.IO;
    using SC_CompressLib.Zip;

    public class SelfExtractor
    {
        const string DllResourceName = "SC_CompressLib.Zip.dll";

        string TargetDirectory = "@@EXTRACTLOCATION";
        string PostUnpackCmdLine = "@@POST_UNPACK_CMD_LINE";
        bool WantOverwrite = false;
        bool ListOnly = false;
        bool Verbose = false;
        string Password = null;
        
        private bool PostUnpackCmdLineIsSet()
        {
            // What is going on here?
            // The PostUnpackCmdLine is initialized to a particular value, then
            // we test to see if it begins with the first two chars of that value,
            // and ends with the last part of the value.  Why?

            // Here's the thing.  In order to insure the code is right, this module has
            // to compile as it is, as a standalone module.  But then, inside
            // DotNetZip, when generating an SFX, we do a text.Replace on the source
            // code, potentially replacing @@POST_UNPACK_CMD_LINE with an actual value.
            // The test here checks to see if it has been set. 

            return !(PostUnpackCmdLine.StartsWith("@@") && 
                     PostUnpackCmdLine.EndsWith("POST_UNPACK_CMD_LINE"));
        }

        private bool TargetDirectoryIsSet()
        {
            return !(TargetDirectory.StartsWith("@@") && 
                     TargetDirectory.EndsWith("EXTRACTLOCATION"));
        }

        // ctor
        private SelfExtractor() { }

        // ctor
        public SelfExtractor(string[] args)
        {
            string specifiedDirectory = null;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-p":
                        i++;
                        if (args.Length <= i)
                        {
                            Console.WriteLine("please supply a password.\n");
                            GiveUsageAndExit();
                        }
                        if (Password != null) 
                        {
                            Console.WriteLine("You already provided a password.\n");
                            GiveUsageAndExit();
                        }
                        Password = args[i];
                        break;
                    case "-o":
                        WantOverwrite = true;
                        break;
                    case "-l":
                        ListOnly = true;
                        break;
                    case "-?":
                        GiveUsageAndExit();
                        break;
                    case "-v":
                        Verbose = true;
                        break;
                    default:
                        // positional args
                        if (specifiedDirectory!=null)
                        {
                            Console.WriteLine("unrecognized argument: '{0}'\n", args[i]);
                            GiveUsageAndExit();
                        }
                        specifiedDirectory = args[i];
                        break;
                }
            }


            if (!ListOnly)
            {
                if (specifiedDirectory!=null)
                    TargetDirectory = specifiedDirectory;
                else if (!TargetDirectoryIsSet())
                    TargetDirectory = ".";  // cwd
            }

            if (ListOnly && (WantOverwrite || Verbose || (specifiedDirectory != null)))
            {
                Console.WriteLine("Inconsistent options.\n");
                GiveUsageAndExit();
            }
        }



        static SelfExtractor()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Resolver);
        }


        static System.Reflection.Assembly Resolver(object sender, ResolveEventArgs args)
        {
            // super defensive
            Assembly a1 = Assembly.GetExecutingAssembly();
            if (a1==null)
                throw new Exception("GetExecutingAssembly returns null.");

            string[] tokens = args.Name.Split(',');
            
            String[] names = a1.GetManifestResourceNames();
            
            if (names==null)
                throw new Exception("GetManifestResourceNames returns null.");

            // workitem 7978
            Stream s = null;
            foreach (string n in names)
            {
                string root = n.Substring(0,n.Length-4);
                string ext = n.Substring(n.Length-3);
                if (root.Equals(tokens[0])  && ext.ToLower().Equals("dll"))
                {
                    s= a1.GetManifestResourceStream(n);
                    if (s!=null) break;
                }
            }
            
            if (s==null)
                throw new Exception(String.Format("GetManifestResourceStream returns null. Available resources: [{0}]",
                                                  String.Join("|", names)));

            byte[] block = new byte[s.Length];
            
            if (block==null)
                throw new Exception(String.Format("Cannot allocated buffer of length({0}).", s.Length));

            s.Read(block, 0, block.Length);
            Assembly a2 = Assembly.Load(block);
            if (a2==null)
                throw new Exception("Assembly.Load(block) returns null");
            
            return a2;
        }


        public int Run()
        {
            //System.Diagnostics.Debugger.Break();
            

            // There way this works:  the EXE is a ZIP file.  So
            // read from the location of the assembly, in other words the path to the exe. 
            Assembly a = Assembly.GetExecutingAssembly();

            int rc = 0;
            try
            {
                // workitem 7067
                using (global::SC_CompressLib.Zip.ZipFile zip = global::SC_CompressLib.Zip.ZipFile.Read(a.Location))
                {
                    bool header = true;
                    foreach (global::SC_CompressLib.Zip.ZipEntry entry in zip)
                    {
                        if (ListOnly || Verbose)
                        {
                            if (header)
                            {
                                System.Console.WriteLine("Extracting Zip file: {0}", zip.Name);
                                if ((zip.Comment != null) && (zip.Comment != ""))
                                    System.Console.WriteLine("Comment: {0}", zip.Comment);

                                System.Console.WriteLine("\n{1,-22} {2,9}  {3,5}   {4,9}  {5,3} {6,8} {0}",
                                             "Filename", "Modified", "Size", "Ratio", "Packed", "pw?", "CRC");
                                System.Console.WriteLine(new System.String('-', 80));
                                header = false;
                            }

                            System.Console.WriteLine("{1,-22} {2,9} {3,5:F0}%   {4,9}  {5,3} {6:X8} {0}",
                                         entry.FileName,
                                         entry.LastModified.ToString("yyyy-MM-dd HH:mm:ss"),
                                         entry.UncompressedSize,
                                         entry.CompressionRatio,
                                         entry.CompressedSize,
                                         (entry.UsesEncryption) ? "Y" : "N",
                                         entry.Crc32);

                        }

                        if (!ListOnly)
                        {
                            if (entry.Encryption == global::SC_CompressLib.Zip.EncryptionAlgorithm.None)
                            {
                                try
                                {
                                    // entry.Extract(TargetDirectory, WantOverwrite);
                                    entry.Extract(TargetDirectory, ExtractExistingFileAction.OverwriteSilently);
                                }
                                catch (Exception ex1)
                                {
                                    Console.WriteLine("Failed to extract entry {0} -- {1}", entry.FileName, ex1.Message);
                                    rc++;
                                    break;
                                }
                            }
                            else
                            {
                                if (Password == null)
                                    Console.WriteLine("Cannot extract entry {0} without a password.", entry.FileName);
                                else
                                {
                                    try
                                    {
                                        // entry.ExtractWithPassword(TargetDirectory, WantOverwrite, Password);
                                        entry.ExtractWithPassword(TargetDirectory, ExtractExistingFileAction.OverwriteSilently, Password);
                                    }
                                    catch (Exception ex2)
                                    {
                                        // probably want a retry here in the case of bad password.
                                        Console.WriteLine("Failed to extract entry {0} -- {1}", entry.FileName, ex2.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("The self-extracting zip file is corrupted.");
                return 4;
            }

            if (rc != 0) return rc;
            
            // potentially execute the embedded command
            if (PostUnpackCmdLineIsSet())
            {
                if (ListOnly)
                {
                    Console.WriteLine("\nExecute on unpack: {0}", PostUnpackCmdLine);
                }
                else
                {
                    try
                    {
                        string[] args = PostUnpackCmdLine.Split( new char[] {' '}, 2);

                        Directory.SetCurrentDirectory(TargetDirectory);
                        System.Diagnostics.Process p = null;
                        if (args.Length > 1)
                            p = System.Diagnostics.Process.Start(args[0], args[1]);
                    
                        else if (args.Length == 1)
                            p = System.Diagnostics.Process.Start(args[0]);
                        // else, nothing.

                        if (p!=null)
                        {
                            p.WaitForExit();
                            rc = p.ExitCode;
                        }
                                             
                    }
                    catch
                    {
                        rc = 5;
                    }
                    
                }
            }

            return rc;
        }


        private void GiveUsageAndExit()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            string s = Path.GetFileName(a.Location);
            Console.WriteLine("DotNetZip Command-Line Self Extractor, see http://www.codeplex.com/DotNetZip");
            Console.WriteLine("usage:\n  {0} [-o] [-v] [-p password] [<directory>]", s);
            Console.WriteLine("    Extracts entries from the archive.\n" +
                              "    -o   - overwrite any existing files upon extraction.\n" +
                              "    -v   - verbose.\n");
            
            if (TargetDirectoryIsSet()) 
                Console.WriteLine("  default extract dir: {0}\n",TargetDirectory);


            Console.WriteLine("  {0} -l", s);
            Console.WriteLine("    Lists entries in the archive.");
            Environment.Exit(1);
        }



        //public static int Main(string[] args)
        //{
        //    int rc = 0;
        //    try
        //    {
        //        SelfExtractor me = new SelfExtractor(args);
        //        rc = me.Run();
        //    }
        //    catch (System.Exception exc1)
        //    {
        //        Console.WriteLine("Exception while extracting: {0}", exc1.ToString());
        //        rc = 255;
        //    }
        //    return rc;
        //}

    }
}
