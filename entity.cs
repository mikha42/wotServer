using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wotServer
{
    class entity
    {
        public bool rootentity = true;
        public bool enabled = true;
        List<module> components = new List<module>();
        public string name;
        public object parent;
        private place _position;
        public place position {
            set
            {
                place old = _position;
                _position = value;
                world.move(old, _position, this);
                invoke("moved", _position);
            }
            get
            {
                return _position;
            }
        }

        public entity(object parent)
        {
            this.parent = parent;
        }

        public bool hasParent()
        {
            return parent != null;
        }


        public void addModule(Type m)
        {
            string name = m.Name;
            if (typeof(module).IsAssignableFrom(m))
            {
                components.Add((module)Activator.CreateInstance(m, this, name));
            }
        }

        public module module(string name)
        {
            foreach (module m in components)
            {
                Type mType = m.GetType();
                if (name == mType.Name)
                    return m;
            }
            return null;
        }
        public module module(Type type)
        {
            foreach (module m in components)
                if (m.GetType() == type)
                    return m;
            return null;
        }

        public bool hasModule(string name)
        {
            foreach (module m in components)
            {
                Type mType = m.GetType();
                if (name == mType.ToString())
                    return true;
            }
            return false;
        }

        public object[] invoke(string name, params object[] args)
        {
            List<object> returns = new List<object>();
            foreach (module m in components)
            {
                Type mtype = m.GetType();
                MethodInfo theMethod = mtype.GetMethod(name);
                if (theMethod == null)
                    continue;
                object return_value = theMethod.Invoke(m, args);
                if (return_value != null)
                    returns.Add(return_value);
            }
            return returns.ToArray();
        }

        public bool can(string name)
        {
            List<object> returns = new List<object>();
            foreach (module m in components)
            {
                Type mtype = m.GetType();
                MethodInfo theMethod = mtype.GetMethod(name);
                if (theMethod != null)
                    return true;
            }
            return false;
        }

        public string[] prefix(char p)
        {
            List<string> outp = new List<string>();
            foreach (module m in components)
                foreach (var method in m.GetType().GetMethods())
                    if (method.Name.Substring(0, 2) == p + "_")
                        outp.Add(method.Name);
            return outp.ToArray();
        }


    }
}
