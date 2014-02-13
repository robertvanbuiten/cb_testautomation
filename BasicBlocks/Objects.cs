using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBank
{
    /// <summary>
    /// 
    /// </summary>
    
    public class FrameworkObject
    {
        public long row;

        public FrameworkObject()
        {
            this.row = 0;
        }

        public FrameworkObject(long row)
        {
            this.row = row;
        }
    }

    /// <summary>
    /// 
    /// </summary>
        
    public class ChildObject: FrameworkObject 
    {
        public string Name;
        public ParentObject Parent;
        public Screenobject Technical;

        public ChildObject() : base()
        {
            this.Name = "";
            this.Parent = new ParentObject();
        }

        public ChildObject(long row)
            : base(row)
        {
            this.Name = "";
            this.Parent = new ParentObject();
        }


    }

    /// <summary>
    /// 
    /// </summary>

    public class ParentObject: FrameworkObject
    {
        public string Name;
        public BASE_TYPE Base;

        public ParentObject() : base()
        {
            this.Name = "";
            this.Base = BASE_TYPE.UNKNOWN;
        }

        public ParentObject(long row)
            : base(row)
        {

        }
    }
}
