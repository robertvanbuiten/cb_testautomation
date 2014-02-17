using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDAPIOLELib;

namespace CoreBank
{
    public class QC : Repository
    {
        protected TDConnection td;

        protected SubjectNode ProcessFolder;
        protected TestFactory ProcessFactory;
        protected SubjectNode ProcessDestFolder;

        protected List<Test> TestCases;
        protected List ProcessList;
        protected string ProcessFolderName;
        public int ProcessIndex;
        public int ProcessMax;

        protected TestSetFolder ProcessTestSetFolder;
        protected TestSetFolder ProcessTestSetNewFolder;
        protected TestSetFactory ProcessTestSetFactory;
        protected TestSet ProcessTestSet;
        protected List ProcessTestSetList;

        protected SubjectNode ProjectFolder;
        protected TestFactory ProjectFactory;
        protected List ProjectList;
        protected int ProjectIndex;

        protected Test ProcessTemplate;
        protected Test ActiveTest;

        public QC(ConnectionSettings settings): base(settings)
        {
           
        }

        /// <summary>
        /// OVERRIDE FUNCTIONS
        /// </summary>
        /// <returns></returns>

        public override bool Connect()
        {
            bool blnResult = false;
            this.Message = "";

            try
            {
                td = new TDConnection();
                td.InitConnectionEx(this.Settings.Address);
                td.ConnectProjectEx(this.Settings.Domain, this.Settings.Database, this.Settings.User, this.Settings.Password);
                blnResult = td.Connected;

            }
            catch (Exception ex)
            {
                Framework.Log.AddError("Cannot connect to QC / ALM", ex.Message, ex.StackTrace);
            }

            if (blnResult)
            {
                Framework.Log.AddCorrect("Connected with HP QC / ALM");
            }
            else
            {
                Framework.Log.AddIssue("Cannot connect to QC / ALM");
            }

            return blnResult;
        }

        /// <summary>
        /// Upload a file to an existing resource.
        /// </summary>
        /// <returns></returns>
        /// 
        public override bool GetProcess()
        {
            bool blnResult = false;

            if (DownloadResource(Framework.ActiveProcess.Name))
            {
                Framework.Log.AddCorrect("Downloaded Process '" + Framework.ActiveProcess.Name + "'.");
                blnResult = true;
            }

            return blnResult;
        }

        public override bool SaveProcess()
        {
            bool blnResult = false;

            if (UploadResource(Framework.Process.Base.FullName, Framework.ActiveProcess.Name))
            {
                Framework.Log.AddCorrect("Saved Process '" + Framework.ActiveProcess.Name + "'.");
                blnResult = true;
            }

            return blnResult;
        }

        public override bool GetConfig()
        {
            bool blnResult = false;

            if (DownloadResource("Config"))
            {
                Framework.Log.AddCorrect("Downloaded config.xml.");
                blnResult = true;
            }
            
            return blnResult;
        }

        public override bool SaveTest(ExcelTest source)
        {
            bool blnResult = false;

            if (UploadTestCase(source))
            {
                Framework.Log.AddCorrect("Saved test '" + source.Name + "'.");
                blnResult = true;
            }

            return blnResult;
        }

        /// <summary>
        /// Get resource
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns></returns>

        private QCResource FindResource(string name)
        {
            QCResource res = null;
            bool blnFound = false;

            List list = null;

            if (name.IndexOf(" ") > 0)
            {
                name = "'" + name + "'";
            }

            try
            {
                QCResourceFactory factory = (QCResourceFactory)td.QCResourceFactory;
                TDFilter filter = (TDFilter)factory.Filter;
                filter["RSC_NAME"] = name;

                list = (List)factory.NewList(filter.Text);
            }
            catch (Exception ex)
            {
                Framework.Log.AddError("Error finding resource " + name, ex.Message, ex.StackTrace);
            }

            if (list != null)
            {
                if (list.Count == 1)
                {
                    QCResource resource = list[1] as QCResource;
                    res = resource;
                    blnFound = true;
                }
                else if (list.Count == 0)
                {
                    Framework.Log.AddError("Cannot find resource with name '" + name + "' in ALM Test Resources.","","");
                }
                else
                {
                    Framework.Log.AddError("Found multiple test resources with name " + name + " in ALM.","","");
                }
            }
              
            return res;
        }

        /// <summary>
        /// Download resource from ALM to temp map
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        private bool DownloadResource(string name)
        {
            bool blnResult = true;
            
            QCResource resource = null;
            resource = FindResource(name);

            if (resource != null)
            {
                try
                {
                    IResourceStorage res = resource as IResourceStorage;
                    res.DownloadResource(Framework.Paths.TempPath, false);
                }
                catch (Exception ex)
                {
                    blnResult = false;
                    Framework.Log.AddError("Could not download Test Resource " + name + " from ALM to " + Framework.Paths.TempPath + " \n", ex.Message, "");
                }
            }
                        
            return blnResult;
        }
        
        
        private bool UploadResource(string filename, string resourcename)
        {
            bool blnResult = true;
            
            QCResource resource = null;
            resource = FindResource(Framework.ActiveProcess.Name);

            if (resource != null)
            {
                try
                {
                    resource.FileName = Framework.ActiveProcess.File;
                    IResourceStorage res = resource as IResourceStorage;
                    res.UploadResource(Framework.Paths.TempPath, true);
                }
                catch (Exception ex)
                {
                    blnResult = false;
                    Framework.Log.AddError("Could not upload " + filename + " to ALM Test Resource " + resourcename + " \n",ex.Message,"");
                }
            }
            else
            {
                Framework.Log.AddError("Could not upload " + filename + " to ALM Test Resource " + resourcename,"","");
            }

            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        public bool DeleteTestCases()
        {
            bool blnResult = true;

            TDFilter filter = (TDFilter)ProcessFactory.Filter;
            filter["TS_NAME"] = "'4 - Repair'";
            ProcessList = ProcessFactory.NewList(filter.Text);
            
            foreach (object item in ProcessList)
            {
                if (item is Test)
                {
                    Test test = item as Test;
                    if (!test.IsLocked)
                    {
                        ProcessFactory.RemoveItem(test.ID);
                    }
                    else
                    {
                        blnResult = false;
                        
                    }
                }
            }

            // reset list
            ProcessList = ProcessFactory.NewList("");

            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Testcase"></param>
        /// <returns></returns>

        public bool PrepareUpload()
        {
            bool blnResult = false;
            
            if (ReadProject())
            {   
                if (ResetProcess())
                {
                    if (SetStatusExistingTest())
                    {
                        if (PrepareTestSet())
                        {
                            if (CreateTestSetFolder())
                            {
                                if (ClearTestSetFolder())
                                {
                                    blnResult = true;
                                }
                            }
                        }
                    }
                }
            }

            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        public bool ResetProcess()
        {
            bool blnResult = false;

            ProcessFactory = null;
            ProcessFolder = FindFolder(Framework.ActiveProcess.QCTestPlan);
            
            if (ProcessFolder != null)
            {
                try
                {
                    ProcessFactory = (TestFactory)ProcessFolder.TestFactory;
                }
                catch (Exception ex)
                {
                    Framework.Log.AddError("Could not set Process Factory '" + Framework.ActiveProcess.QCTestPlan + "' in ALM.\n", ex.Message,"");
                }

                if (ProcessFactory != null)
                {
                    ProcessList = ProcessFactory.NewList("");
                    ProcessIndex = 1;
                    ProcessMax = ProcessList.Count;

                    blnResult = true;
                }
            }
            else
            {
                Framework.Log.AddError("Cannot find Process folder '" + Framework.ActiveProcess.QCTestPlan + "' in ALM Testplan'", "", "");
            }
                               
            return blnResult;
        }

        private bool SetStatusExistingTest()
        {
            bool blnResult = true;
            
            foreach (object item in ProcessList)
            {
                if (item is Test)
                {
                    Test test = item as Test;
                    if (!test.IsLocked)
                    {
                        try
                        {
                            test["TS_STATUS"] = "4 - Repair";
                        }
                        catch (Exception ex)
                        {
                            Framework.Log.AddError("Test case '" + test.Name + "' is locked.", ex.Message, "");
                        }
                    }
                    else
                    {
                        test.UnLockObject();
                        if (!test.IsLocked)
                        {
                            blnResult = false;
                            Framework.Log.AddError("Test case '" + test.Name + "' is locked.","","");
                            break;
                        }
                    }
                }
            }

            return blnResult;
        }

        private bool PrepareTestSet()
        {
            bool blnResult = true;

            try
            {
                // Set folder name for testplan and test set
                DateTime time = DateTime.Now;
                string timestamp = Framework.ActiveProcess.Name + time.ToString("ddMMyyyy_hhmmss]");
                ProcessFolderName = timestamp;

                // Create a new list with testcases
                this.TestCases = new List<Test>();
            }
            catch (Exception ex)
            {
                Message = "Cannot create test set in ALM '" + ProcessFolderName + "'.\n" + ex.Message;
            }

            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public bool CreateFolder()
        {
            bool blnResult = false;

            ////ProcessDestFolder = (SubjectNode)ProcessFolder.AddNode(ProcessFolderName);
            
            return blnResult;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public bool CreateTestSetFolder()
        {
            bool blnResult = true;

            //try
            //{
            //    TestSetTreeManager tm = td.TestSetTreeManager;
            //    ProcessTestSetFolder = tm.get_NodeByPath(Framework.ActiveProcess.QCTestSet);
            //    //ProcessTestSetNewFolder = (TestSetFolder)ProcessTestSetFolder.AddNode(ProcessFolderName);
            //    ProcessTestSetFactory = (TestSetFactory)ProcessTestSetFolder.TestSetFactory;

            //    ProcessTestSetList = ProcessTestSetFactory.NewList("");

            //    foreach (object item in ProcessTestSetList)
            //    {
            //        if (item is TestSet)
            //        {
            //            TestSet testset = item as TestSet;

            //            if (testset.Name.ToLower() == Framework.ActiveProcess.Name)
            //            {
            //                ProcessTestSet = testset;
            //                blnResult = true;
            //                break;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            ////{
            //    Message = "Could not add folder '"  + ProcessFolderName +  "' to '" + Framework.ActiveProcess.QCTestSet + "'./n" +  ex.Message;
            //    blnResult = false;
            //}

            return blnResult;
        }

        public bool ClearTestSetFolder()
        {
            bool blnResult = true;
            
            //// remove testset item
            //foreach (object item in ProcessTestSetList)
            //{
            //    if (item is TSTest)
            //    {
            //        TSTest test = item as TSTest;
            //        try
            //        {
            //            ProcessTestSetFactory.RemoveItem(test.ID);
            //        }
            //        catch (Exception ex)
            //        {
            //            Message = "Could not remove '" + test.Name + "' from test set '" + Framework.ActiveProcess.Name + ".\n" + ex.Message;
            //            blnResult = false;
            //            break;
            //        }

            //    }
            //}

            // Reset list            
            //ProcessTestSetList = ProcessTestSetFactory.NewList("");

            return blnResult;
        }

        public bool AddToTestSet()
        {
            bool blnResult = true;

            //try
            //{
            //    TSTestFactory factory = (TSTestFactory)ProcessTestSet.TSTestFactory();
            //    TSTest test = (TSTest)factory.AddItem(null);
            //    test["TC_TEST_ID"] = ActiveTest.ID;
            //    test.Post();
            //}
            //catch (Exception Ex)
            //{
            //    Message = "Could not add test '" + ActiveTest.Name + "' to testset '" + ProcessFolderName + "'.\n" + Ex.Message;
            //    blnResult = false;
            //}


            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public bool ReadProject()
        {
            bool blnResult = false;

            ProjectFactory = null;
            ProjectFolder = null;
            ProjectList = null;

            ProjectFolder = FindFolder(Framework.ActiveApplication.QCTestPlan);

            if (ProjectFolder != null)
            {
                ProjectFactory = (TestFactory)ProjectFolder.TestFactory;

                if (ProjectFactory != null)
                {
                    TDFilter filter = (TDFilter)ProjectFactory.Filter;
                    filter["TS_NAME"] = "Process_template";
                    ProjectList = ProjectFactory.NewList(filter.Text);

                    if (ProjectList.Count >= 1)
                    {
                        foreach (object item in ProjectList)
                        {
                            if (item is Test)
                            {
                                ProcessTemplate = item as Test;
                                blnResult = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Framework.Log.AddError("Cannnot find Process_Template in '" + Framework.ActiveProcess.QCTestPlan + "' in ALM Testplan.", "", "");
                    }
                }
                else
                {
                    Framework.Log.AddError("Cannot determine project factory '" + Framework.ActiveProcess.QCTestPlan + "'.", "", "");
                }
            }
            else
            {
                Framework.Log.AddError("Cannot find Project folder '" + Framework.ActiveProcess.QCTestPlan + "' in ALM Testplan.'", "", "");
            }


            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TestCase"></param>
        /// <returns></returns>

        public bool CopyTestCase(ExcelTest TestCase)
        {
            bool blnResult = false;
            
            try
            {
                // Copy template
                ISupportCopyPaste copy = (ISupportCopyPaste)ProjectFactory;
                string clipboard = copy.CopyToClipBoard(ProcessTemplate.ID, 0, "");
                
                // Paste template
                ISupportCopyPaste paste = (ISupportCopyPaste)ProcessFactory;
                paste.PasteFromClipBoard(clipboard, ProcessFolder.NodeID.ToString(), 0, 0);

                // find the just pasted process template;
                blnResult = FindTestCase("Process_template");
            }
            catch (Exception ex)
            {
                Framework.Log.AddError("Could not paste Process_template from " + Framework.ActiveApplication.QCTestPlan + " to " + Framework.ActiveProcess.QCTestPlan + " in ALM.", ex.Message, "");
            }

            return blnResult;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TestCase"></param>
        /// <returns></returns>

        public bool UploadTestCase(ExcelTest TestCase)
        {
            bool blnFound = false;
            bool blnResult = false;
            Message = "";

            // Check if testcase is found. Set active test
            if (FindTestCase(TestCase.Name))
            {
                blnFound = true;
            }
            // copy test case
            else
            {
                if (CopyTestCase(TestCase))
                {
                    blnFound = true;
                }
            }
          
            if (blnFound)
            {
                if (ChangeTestCase(TestCase))
                {
                    if (AddToTestSet())
                    {
                        blnResult = true;
                    }
                }
            }

            return blnResult;
        }

        /// <summary>
        /// Set QC Test properties.
        /// </summary>
        /// <param name="QCTest"></param>
        /// <param name="tc"></param>
        /// <returns></returns>

        private bool ChangeTestCase(ExcelTest tc)
        {
            bool blnResult = false;

            if (ActiveTest != null)
            {
                try
                {
                    ActiveTest.Name = tc.Name;
                    ActiveTest["TS_DESCRIPTION"] = tc.Message;
                    ActiveTest["TS_STATUS"] = "5 - Ready";
                    ActiveTest.Post();
                    blnResult = true;
                }
                catch (Exception ex)
                {
                    Framework.Log.AddError("Could not change properties of test" + ActiveTest.Name + "\n", ex.Message, "");
                }
            }

            return blnResult;
        }


        /// <summary>
        /// Find Testcase in folder.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="filename"></param>
        /// <returns></returns>

        private bool FindTestCase(string name)
        {
            ActiveTest = null;
            bool blnResult = false;

            if (name.IndexOf(" ") > 0)
            {
                name = "'" + name + "'";
            }

            TDFilter filter = (TDFilter)ProcessFactory.Filter;
            filter["TS_NAME"] = name;         
            
            ProcessList = ProcessFactory.NewList(filter.Text);
            
            foreach (Object test in ProcessList)
            {
                if (test is Test)
                {
                    ActiveTest = test as Test;
                    blnResult = true;
                    break;
                }
            }

            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foldername"></param>
        /// <returns></returns>

        private SubjectNode FindFolder(string foldername)
        {
            SubjectNode node = null;
            SubjectNode root = null;

            if (td == null)
                Connect();

            try
            {
                TreeManager manager = (TreeManager)td.TreeManager;
                node = (SubjectNode)manager.get_NodeByPath(foldername);
            }
            catch (Exception ex)
            {
                Framework.Log.AddError("Cannot find folder " + foldername + "\n", ex.Message, "");
            }

            return node;
        }            
    }
}
