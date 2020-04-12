using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LoAAssist.DI
{
    class ObjectManager
    {
        protected Dictionary<Type, object> instances = new Dictionary<Type, object> { };

        /// <summary>
        /// Set instance of defined type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        public void set(Type type, object obj)
        {
            this.instances[type] = obj;
        }

        /// <summary>
        /// Get instance of defined type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object get(Type type)
        {
            if (!instances.ContainsKey(type)) {
                // TODO: get preference
                this.instances[type] = Activator.CreateInstance(type, args: this.resolveArguments(type));
            }

            return this.instances[type];
        }

        /// <summary>
        /// Resolve arguments for type
        /// Recursively instantiate objects depending of defined type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected object[] resolveArguments(Type type)
        {
            ConstructorInfo[] constructorsInfo = type.GetConstructors();
            ParameterInfo[] parametersInfo = constructorsInfo[0].GetParameters();
            List<object> result = new List<object> { };
            foreach (ParameterInfo parameterInfo in parametersInfo)
            {
                Type parameterType = parameterInfo.ParameterType;
                result.Add(this.get(parameterType));
            }
            return result.ToArray();
        }
    }
}
