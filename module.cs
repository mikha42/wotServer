using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{

    class debugmodule : module
    {
        public debugmodule(entity self, string name) : base(self, name) {  }

        private float testvalue;

        public void setvalue(object o)
        {
            float f = (float)o;
            testvalue = f;
        }

        public float getvalue()
        {
            return testvalue;
        }

    }

    class module
    {
        readonly public entity self;
        readonly public string name;

        public override string ToString()
        {
            return base.ToString();
        }
        public override bool Equals(object obj)
        {
            return obj.ToString() == ToString();
        }
        public override int GetHashCode()
        {
            return 0;
        }

        public module(entity self, string name)
        {
            this.self = self;
            this.name = name;
        }

        public void pickup()
        {

        }
        public void update()
        {

        }
        public void initialize()
        {

        }
        public void use()
        {

        }
}
}
