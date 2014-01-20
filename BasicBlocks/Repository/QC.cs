﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OTAClientLib;

namespace CoreBank
{
    public class QC : Repository
    {
        protected TDConnection td;

        protected SubjectNode ProcessFolder;
        protected TestFactory ProcessFactory;
        protected SubjectNode ProcessDestFolder;

        protected List<Test> TestCases;
        protected List<Process> ProcessList;
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


        public override bool Connect()
        {
            bool blnResult = false;
            this.Message = "";

            try
            {
                td = new TDConnection();
                td.IgnoreHtmlFormat = true;
                td.InitConnectionEx(this.Settings.Address);
                td.ConnectProjectEx(this.Settings.Domain, this.Settings.Database, this.Settings.User, this.Settings.Password);
                blnResult = td.Connected;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot connect to ALM. /n" + ex.Message);
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
            catch {}

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
                    System.Windows.Forms.MessageBox.Show("Cannot find resource with name '" + name + "' in ALM Test Resources.");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Found multiple test resources with name " + name + " in ALM.");
                }
            }
            //else
            //{
            //     QCResourceFactory factory = (QCResourceFactory)td.QCResourceFactory;
            //     list = (List)factory.NewList("");

            //    foreach (QCResource resource in list)
            //    {
            //        if (resource is QCResource)
            //        {
            //            QCResource _res = resource as QCResource;
            //            string _resname = _res.Name.ToString().Trim().ToLower();

            //            if (_resname == name)
            //            {
            //                res = _res;
            //                blnFound = true;
            //                break;
            //            }
            //        }
            //    }


            //    if (!blnFound)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Could not find Test Resources with name " + name + " in ALM.");
            //    }
            //}
            
            return res;
        }

        /// <summary>
        /// Download resource from ALM to temp map
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public bool GetResource(string name)
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
                    System.Windows.Forms.MessageBox.Show("Could not download Test Resource " + name + " from ALM to " + Framework.TempPath + " \n" + ex.Message );
                }
            }
                        
            return blnResult;
        }
        
        /// <summary>
        /// Upload a file to an existing resource.
        /// </summary>
        /// <returns></returns>

        public bool SaveResource(string filename, string resourcename)
        {
            bool blnResult = true;
            
            QCResource resource = null;
            resource = FindResource(resourcename);

            if (resource != null)
            {
                try
                {
                    resource.FileName = filename;
                    IResourceStorage res = resource as IResourceStorage;
                    res.UploadResource(Framework.TempPath, true);
                }
                catch (Exception ex)
                {
                    blnResult = false;
                    System.Windows.Forms.MessageBox.Show("Could not upload " + filename + " to ALM Test Resource " + resourcename + " \n" + ex.Message);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Could not upload " + filename + " to ALM Test Resource " + resourcename);
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

        public bool ReadProcess()
        {
            bool blnResult = false;
            
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
                    Message = "Could not set Process Factory '" + Framework.ActiveProcess.QCTestPlan + "' in ALM.\n" + ex.Message;
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
                Message = "Cannot find Process folder '" + Framework.ActiveProcess.QCTestPlan + "' in ALM Testplan'";
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
                        catch {}
                    }
                    else
                    {
                        test.UnLockObject();
                        if (!test.IsLocked)
                        {
                            blnResult = false;
                            Message = "Test case '" + test.Name + "' is locked.";
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
                        Message = "Cannnot find Process_Template in '" + Framework.ActiveProcess.QCTestPlan + "' in ALM Testplan.";
                    }
                }
                else
                {
                    Message = "Cannot determine project factory '" + Framework.ActiveProcess.QCTestPlan + "'.";
                }
            }
            else
            {
                Message = "Cannot find Project folder '" + Framework.ActiveProcess.QCTestPlan + "' in ALM Testplan.'";
            }


            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TestCase"></param>
        /// <returns></returns>

        public bool CopyTestCase(Test TestCase)
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
                Message = "Could not paste Process_template from " + Framework.ActiveApplication.QCTestPlan + " to " + Framework.ActiveProcess.QCTestPlan + " in ALM.";
            }

            return blnResult;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TestCase"></param>
        /// <returns></returns>

        public bool UploadTestCase(TestCase TestCase)
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

        //private bool ChangeTestCase(TestCase tc)
        //{
        //    bool blnResult = false;

        //    if (ActiveTest != null)
        //    {
        //        try
        //        {
        //            ActiveTest.Name = tc.Name;
        //            ActiveTest["TS_DESCRIPTION"] = tc.Message;
        //            ActiveTest["TS_STATUS"] = "5 - Ready";
        //            ActiveTest.Post();
        //            blnResult = true;
        //        }
        //        catch(Exception ex) 
        //        {
        //            Message = "Could not change properties of test" + ActiveTest.Name + "\n" + ex.Message;
        //        }
        //    }

        //    return blnResult;
        //}


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
                Message = "Cannot find folder " + foldername + "\n" + ex.Message;
            }

            return node;
        }            
    }
}
