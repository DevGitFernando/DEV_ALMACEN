// WinFormsSelfExtractorStub.cs
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
// Time-stamp: <2009-July-03 15:03:01>
//
// ------------------------------------------------------------------
//
// Implements the "stub" of a WinForms self-extracting Zip archive. This
// code is included in all GUI SFX files.  It is included as a resource
// into the DotNetZip DLL, and then is compiled at runtime when a SFX is
// saved.  This code runs when the SFX is run.
//
// ------------------------------------------------------------------

namespace SC_CompressLib.Zip
{
    // The using statements must be inside the namespace scope, because when the SFX is being 
    // generated, this module gets concatenated with other source code and then compiled.

    using System;
    using System.Reflection;
    using System.IO;
    using System.Windows.Forms;

    public partial class WinFormsSelfExtractorStub : Form
    {
        //const string IdString = "DotNetZip Self Extractor, see http://www.codeplex.com/DotNetZip";
        const string DllResourceName = "SC_CompressLib.Zip.dll";

        int entryCount;

        delegate void ExtractEntryProgress(ExtractProgressEventArgs e);

        void _SetDefaultExtractLocation()
        {
            // Design Note:
            
            // What follows may look odd.  The textbox is set to a particular value.
            // Then the value is tested, and if the value begins with the first part
            // of the string and ends with the last part, and if it does, then we
            // change the value.  When would that not get replaced?
            //

            // Well, here's the thing.  This module has to compile as it is, as a
            // standalone sample.  But then, inside DotNetZip, when generating an SFX,
            // we do a text.Replace on @@EXTRACTLOCATION and insert a different value.

            // So the effect is, with a straight compile, the value gets
            // SpecialFolder.Personal.  If you replace @@EXTRACTLOCATION with
            // something else, it stays and does not get replaced.

            this.txtExtractDirectory.Text = "@@EXTRACTLOCATION";

            if (this.txtExtractDirectory.Text.StartsWith("@@") && 
                this.txtExtractDirectory.Text.EndsWith("EXTRACTLOCATION"))
            {
                this.txtExtractDirectory.Text = 
                    System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                           ZipName);
            }
        }


        
        void _SetPostUnpackCmdLine()
        {
            // See the design note in _SetDefaultExtractLocation() for
            // an explanation of what is going on here.
            
            this.txtPostUnpackCmdLine.Text = "@@POST_UNPACK_CMD_LINE";

            if (this.txtPostUnpackCmdLine.Text.StartsWith("@@") && 
                this.txtPostUnpackCmdLine.Text.EndsWith("POST_UNPACK_CMD_LINE"))
            {
                // If there is nothing set for the CMD to execute after unpack, then
                // disable all the UI associated to that bit.
                this.txtPostUnpackCmdLine.Enabled = false;
                this.txtPostUnpackCmdLine.Visible = false;
                this.chk_ExeAfterUnpack.Enabled = false;
                this.chk_ExeAfterUnpack.Visible = false;

                // adjust the position of all the remaining UI
                int delta = this.txtPostUnpackCmdLine.Height;

                this.MinimumSize = new System.Drawing.Size(this.MinimumSize.Width, this.MinimumSize.Height - (delta + 4));
                
                 MoveDown(this.chk_Overwrite, delta);
                 MoveDown(this.chk_OpenExplorer, delta);
                 MoveDown(this.btnDirBrowse, delta);
                 MoveDown(this.txtExtractDirectory, delta);
                 MoveDown(this.label1, delta);
                          
//                 moveup(this.btnContents, delta);
//                 MoveUp(this.btnCancel, delta);
//                 MoveUp(this.btnExtract, delta);
                
                 // finally, adjust the size of the form
                this.Size = new System.Drawing.Size(this.Width, this.Height - (delta + 4));
            }
        }

        private void MoveDown (System.Windows.Forms.Control c, int delta)
        {
            c.Location = new System.Drawing.Point(c.Location.X, c.Location.Y + delta);
        }
        
        public WinFormsSelfExtractorStub()
        {
            InitializeComponent();
            _setCancel = true;
            entryCount= 0;
            _SetDefaultExtractLocation();
            _SetPostUnpackCmdLine();
            
            try
            {
                if ((zip.Comment != null) && (zip.Comment != ""))
                {
                    txtComment.Text = zip.Comment;
                }
                else
                {
                    label2.Visible = false;
                    txtComment.Visible = false;
                    this.Size = new System.Drawing.Size(this.Width, this.Height - (this.txtComment.Height+18));
                }
            }
            catch
            {
                // why would this ever fail?  Not sure. 
                label2.Visible = false;
                txtComment.Visible = false;
                this.Size = new System.Drawing.Size(this.Width, this.Height - (this.txtComment.Height+18));
            }
        }


        static WinFormsSelfExtractorStub()
        {
            // This is important to resolve the SC_CompressLib.Zip.dll inside the extractor. 
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Resolver);
        }


        #if ORIG
        static System.Reflection.Assembly Resolver(object sender, ResolveEventArgs args)
        {
            Assembly a1 = Assembly.GetExecutingAssembly();
            Stream s = a1.GetManifestResourceStream(DllResourceName);
            byte[] block = new byte[s.Length];
            s.Read(block, 0, block.Length);
            Assembly a2 = Assembly.Load(block);
            return a2;
        }
        #else
            
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
            
        #endif


        private void btnDirBrowse_Click(object sender, EventArgs e)
        {
            SC_CompressLib.Utils.FolderBrowserDialogEx dlg1 = new SC_CompressLib.Utils.FolderBrowserDialogEx();
            dlg1.Description = "Select a folder for the extracted files:";
            dlg1.ShowNewFolderButton = true;
            dlg1.ShowEditBox = true;
            //dlg1.NewStyle = false;
            dlg1.SelectedPath = txtExtractDirectory.Text;
            dlg1.ShowFullPathInEditBox = true;
            dlg1.RootFolder = System.Environment.SpecialFolder.MyComputer;

            // Show the FolderBrowserDialog.
            DialogResult result = dlg1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtExtractDirectory.Text = dlg1.SelectedPath;
            }
        }


        private void btnExtract_Click(object sender, EventArgs e)
        {
            KickoffExtract();
        }


        private void KickoffExtract()
        {
            // disable most of the UI: 
            this.btnContents.Enabled = false;
            this.btnExtract.Enabled = false;
            this.chk_OpenExplorer.Enabled = false;
            this.chk_Overwrite.Enabled = false;
            this.chk_ExeAfterUnpack.Enabled = false;
            this.txtExtractDirectory.Enabled = false;
            this.txtPostUnpackCmdLine.Enabled = false;
            this.btnDirBrowse.Enabled = false;
            this.btnExtract.Text = "Extracting...";
            System.Threading.Thread _workerThread = new System.Threading.Thread(this.DoExtract);
            _workerThread.Name = "Zip Extractor thread";
            _workerThread.Start(null);
            this.Cursor = Cursors.WaitCursor;
        }


        private void DoExtract(Object p)
        {
            string targetDirectory = txtExtractDirectory.Text;
            global::SC_CompressLib.Zip.ExtractExistingFileAction WantOverwrite = chk_Overwrite.Checked
                ? global::SC_CompressLib.Zip.ExtractExistingFileAction.OverwriteSilently
                : global::SC_CompressLib.Zip.ExtractExistingFileAction.Throw;
            bool extractCancelled = false;
            System.Collections.Generic.List<String> didNotOverwrite =
                new System.Collections.Generic.List<String>();
            _setCancel = false;
            string currentPassword = "";
            SetProgressBars();

            try
            {
                // zip has already been set, when opening the exe.
                
                zip.ExtractProgress += ExtractProgress;
                foreach (global::SC_CompressLib.Zip.ZipEntry entry in zip)
                {
                    if (_setCancel) { extractCancelled = true; break; }
                    if (entry.Encryption == global::SC_CompressLib.Zip.EncryptionAlgorithm.None)
                    {
                        try
                        {
                            entry.Extract(targetDirectory, WantOverwrite);
                            entryCount++;
                        }
                        catch (Exception ex1)
                        {
                            if (WantOverwrite == global::SC_CompressLib.Zip.ExtractExistingFileAction.OverwriteSilently ||
                                (ex1.Message.ToString() != "The file already exists."))
                            {
                                DialogResult result = MessageBox.Show(String.Format("Failed to extract entry {0} -- {1}", entry.FileName, ex1.Message.ToString()),
                                                                      String.Format("Error Extracting {0}", entry.FileName), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                                if (result == DialogResult.Cancel)
                                {
                                    _setCancel = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        bool done = false;
                        while (!done)
                        {
                            if (currentPassword == "")
                            {
                                string t = PromptForPassword(entry.FileName);
                                if (t == "")
                                {
                                    done = true; // escape ExtractWithPassword loop
                                    continue;
                                }
                                currentPassword = t;
                            }

                            if (currentPassword == null) // cancel all 
                            {
                                _setCancel = true;
                                currentPassword = "";
                                break;
                            }

                            try
                            {
                                entry.ExtractWithPassword(targetDirectory, WantOverwrite, currentPassword);
                                entryCount++;
                                done= true;
                            }
                            catch (Exception ex2)
                            {
                                // Retry here in the case of bad password.
                                if (ex2 as SC_CompressLib.Zip.BadPasswordException != null)
                                {
                                    currentPassword = "";
                                    continue; // loop around, ask for password again
                                }
                                else if (WantOverwrite != global::SC_CompressLib.Zip.ExtractExistingFileAction.OverwriteSilently 
                                        && (ex2.Message.ToString() == "The file already exists."))
                                {
                                    // The file exists, but the user
                                    // did not ask for overwrite.
                                    didNotOverwrite.Add("    " + entry.FileName);
                                    done = true;
                                }
                                else if (WantOverwrite == global::SC_CompressLib.Zip.ExtractExistingFileAction.OverwriteSilently 
                                        && (ex2.Message.ToString() != "The file already exists."))
                                {
                                    DialogResult result = MessageBox.Show(String.Format("Failed to extract the password-encrypted entry {0} -- {1}", entry.FileName, ex2.Message.ToString()),
                                                                          String.Format("Error Extracting {0}", entry.FileName), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    
                                    done= true;
                                    if (result == DialogResult.Cancel)
                                    {
                                        _setCancel = true;
                                        break;
                                    }
                                }
                            } // catch
                        } // while
                    } // else (encryption)
                } // foreach

            }
            catch (Exception)
            {
                MessageBox.Show("The self-extracting zip file is corrupted.",
                                "Error Extracting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Application.Exit();
            }

            if (didNotOverwrite.Count > 0)
            {
                string msg = String.Format("These files were not extracted because the target files already exist:\n{0}", String.Join("\n", didNotOverwrite.ToArray()));
                MessageBox.Show(msg,
                                "DotNetZip: Just so you know...",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
            }

            SetUiDone();

            if (extractCancelled) return;

            
            // optionally open explorer
            if (chk_OpenExplorer.Checked)
            {
                string w = System.Environment.GetEnvironmentVariable("WINDIR");
                if (w == null) w = "c:\\windows";
                try
                {
                    System.Diagnostics.Process.Start(Path.Combine(w, "explorer.exe"), targetDirectory);
                }
                catch { }
            }

            // optionally execute a command
            if (this.chk_ExeAfterUnpack.Visible && this.chk_ExeAfterUnpack.Checked)
            {
                try
                {
                    string[] args = this.txtPostUnpackCmdLine.Text.Split( new char[] {' '}, 2);
                    
                    if (args.Length > 1)
                        System.Diagnostics.Process.Start(args[0], args[1]);
                    else if (args.Length == 1)
                        System.Diagnostics.Process.Start(args[0]);
                    // else, nothing.
                                             
                }
                catch {  }
            }
        }
        

        private void SetUiDone()
        {
            if (this.btnExtract.InvokeRequired)
            {
                this.btnExtract.Invoke(new MethodInvoker(this.SetUiDone));
            }
            else
            {
                this.lblStatus.Text = String.Format("Finished extracting {0} entries.", entryCount);
                btnExtract.Text = "Extracted.";
                btnExtract.Enabled = false;
                btnCancel.Text = "Quit";
                _setCancel = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Extracting_EntryBytesWritten)
            {
                StepEntryProgress(e);
            }

            else if (e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry)
            {
                StepArchiveProgress(e);
            }
            if (_setCancel)
                e.Cancel = true;
        }

        private void StepArchiveProgress(ExtractProgressEventArgs e)
        {
            if (this.progressBar1.InvokeRequired)
            {
                this.progressBar2.Invoke(new ExtractEntryProgress(this.StepArchiveProgress), new object[] { e });
            }
            else
            {
                this.progressBar1.PerformStep();

                // reset the progress bar for the entry:
                this.progressBar2.Value = this.progressBar2.Maximum = 1;
                this.lblStatus.Text = "";
                this.Update();
            }
        }

        private void StepEntryProgress(ExtractProgressEventArgs e)
        {
            if (this.progressBar2.InvokeRequired)
            {
                this.progressBar2.Invoke(new ExtractEntryProgress(this.StepEntryProgress), new object[] { e });
            }
            else
            {
                if (this.progressBar2.Maximum == 1)
                {
                    // reset
                    Int64 max = e.TotalBytesToTransfer;
                    _progress2MaxFactor = 0;
                    while (max > System.Int32.MaxValue)
                    {
                        max /= 2;
                        _progress2MaxFactor++;
                    }
                    this.progressBar2.Maximum = (int)max;
                    this.lblStatus.Text = String.Format("Extracting {0}/{1}: {2} ...",
                                                        this.progressBar1.Value, zip.Entries.Count, e.CurrentEntry.FileName);
                }

                int xferred = (int)(e.BytesTransferred >> _progress2MaxFactor);

                this.progressBar2.Value = (xferred >= this.progressBar2.Maximum)
                    ? this.progressBar2.Maximum
                    : xferred;

                this.Update();
            }
        }

        private void SetProgressBars()
        {
            if (this.progressBar1.InvokeRequired)
            {
                this.progressBar1.Invoke(new MethodInvoker(this.SetProgressBars));
            }
            else
            {
                this.progressBar1.Value = 0;
                this.progressBar1.Maximum = zip.Entries.Count;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Step = 1;
                this.progressBar2.Value = 0;
                this.progressBar2.Minimum = 0;
                this.progressBar2.Maximum = 1; // will be set later, for each entry.
                this.progressBar2.Step = 1;
            }
        }

        private String ZipName
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }
        }

        private Stream ZipStream
        {
            get
            {
                if (_s != null) return _s;
                Assembly a = Assembly.GetExecutingAssembly();

                // workitem 7067
                _s= System.IO.File.OpenRead(a.Location);

                return _s;
            }
        }

        private ZipFile zip
        {
            get
            {
                if (_zip == null)
                    _zip = global::SC_CompressLib.Zip.ZipFile.Read(ZipStream);
                return _zip;
            }
        }

        private string PromptForPassword(string entryName)
        {
            PasswordDialog dlg1 = new PasswordDialog();
            dlg1.EntryName = entryName;

            // ask for password in a loop until user enters a proper one, 
            // or clicks skip or cancel.
            bool done= false;
            do {
                dlg1.ShowDialog();
                done = (dlg1.Result != PasswordDialog.PasswordDialogResult.OK || 
                        dlg1.Password != "");
            } while (!done);

            if (dlg1.Result == PasswordDialog.PasswordDialogResult.OK)
                return dlg1.Password;

            else if (dlg1.Result == PasswordDialog.PasswordDialogResult.Skip)
                return "";

            // cancel
            return null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_setCancel == false)
                _setCancel = true;
            else
                Application.Exit();
        }

        // workitem 6413
        private void btnContents_Click(object sender, EventArgs e)
        {
            ZipContentsDialog dlg1 = new ZipContentsDialog();
            dlg1.ZipFile = zip;
            dlg1.ShowDialog();
            return;
        }


        private int _progress2MaxFactor;
        private bool _setCancel;
        Stream _s;
        global::SC_CompressLib.Zip.ZipFile _zip;

    }



    class WinFormsSelfExtractorStubProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new WinFormsSelfExtractorStub());
        //}
    }
}
