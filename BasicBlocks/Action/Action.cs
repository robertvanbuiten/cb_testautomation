using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBank
{
    public class Action
    {
        public string Name;
        public Result Result;
        public ACTION Type;
        public FASE Fase;
        public long Row;

        public Action(string name, long row)
        {
            this.Name = name;
            this.Row = row;
            this.Result = new Result();
            this.Fase = Fase;
        }

        public Action(string name, FASE fase)
        {
            this.Name = name;
            this.Fase = fase;
        }

        protected virtual bool Start()
        {
            return false;

        }

        protected virtual bool DoWork()
        {
            return false;
        }

        protected virtual bool Stop()
        {
            return false;

        }

        protected virtual bool GetTechnical()
        {
            bool blnResult = false;



            return blnResult;
        }

        protected virtual void DeterminePlatform()
        {

        }

    }

    public class ScreenAction : Action
    {
        public Screenobject Technical;
        public PLATFORM_SCREEN Platform;
        
        public ScreenAction(string name, long row): base(name, row)
        {
            this.Technical = new Screenobject();
        }

        protected override bool GetTechnical()
        {
 	        bool blnResult = false;

            return blnResult;
        }

        protected override void DeterminePlatform()
        {
 	         
        }
    }

    public class CustomAction:Action
    {
        public Custom Technical;
        public FUNCTION_CUSTOM Function;
        
        public CustomAction(string name, long row) : base(name,row)
        {
            this.GetTechnical();
        }

        protected override bool GetTechnical()
        {
 	        bool blnResult = false;

            List<Custom> objects = new List<Custom>();

            //objects = (from cu in Framework.Config.Customs where string.Compare(cu.,this.Name,true) >= 1 select cu).ToList<Custom>();

            //if (objects.Count < 1)
            //{
            //    // Report
            //}
            //else if (objects.Count > 1)
            //{
            //    // Report
            //}
            //else
            //{
            //    this.Technical = objects[0];
            //    blnResult = true;
            //}

            return blnResult;
        }

        protected bool DetermineFunction()
        {
            bool blnResult = false;
            string _name = this.Name.Replace("_","");
            
            this.Function = FUNCTION_CUSTOM.UNKNOWN;
            FUNCTION_CUSTOM _function = (FUNCTION_CUSTOM)Enum.Parse(typeof(FUNCTION_CUSTOM),_name);

            if (_function != null)
            {
                this.Function = _function;
            }

            return blnResult;
            
        }

    }

    public class FrameworkAction:Action
    {   
        public FrameworkAction(string name, FASE fase) : base(name,fase)
        {
        
        }

    }

}
