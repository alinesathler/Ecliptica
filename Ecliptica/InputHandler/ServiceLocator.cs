using System;
using System.Collections.Generic;

namespace Ecliptica.InputHandler
{
    public static class ServiceLocator
    {
        #region Fields
        private static readonly Dictionary<Type, object> _services = new();
        #endregion

        #region Methods
        /// <summary>
        /// Method to register a service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        public static void Register<T>(T service)
        {
            _services[typeof(T)] = service;
        }

        /// <summary>
        /// Method to get a service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return _services.TryGetValue(typeof(T), out var service) ? (T)service : default;
        }
        #endregion
    }

}
