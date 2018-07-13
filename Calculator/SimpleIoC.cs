using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalc
{
    /// <summary>
    /// This class is a take on a Very simple dependency container.
    /// Use RegisterType methods to register dependencies and its implementation
    /// </summary>
    public static class SimpleIoC
    {
        private static Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>();
        private static Dictionary<Type, object> typeInstances = new Dictionary<Type, object>();
        private static object lockObject = new object();
        /// <summary>
        /// This method should be used to register types and their implementation 
        /// </summary>
        /// <typeparam name="T">T should be the base interface</typeparam>
        /// <typeparam name="K">K should the class implementing given interface</typeparam>
        public static void RegisterType<T, K>() where K : T
        {
            var interfaceType = typeof(T);
            var classType = typeof(K);

            if (registeredTypes.ContainsKey(interfaceType.GetType()) == false)
            {
                registeredTypes.Add(interfaceType, classType);
            }
        }

        public static T ResolveSingle<T>() where T: class
        {
            if (registeredTypes.ContainsKey(typeof(T)) == false)
            {
                throw new TypeAccessException("Type not registered.");
            }
            else
            {
                var instanceType = registeredTypes[typeof(T)];
                object instanceObject;

                lock (lockObject)
                {
                    if (typeInstances.TryGetValue(instanceType, out instanceObject) == false)
                    {
                        instanceObject = (T)Activator.CreateInstance(instanceType);
                        typeInstances.Add(instanceType, instanceObject);
                    }
                }
                return (T)instanceObject;
            }
        }
    }
}
