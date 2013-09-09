using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimera
{
    /// <summary>
    /// The ObjectDispatcher manages an array of objects, which may be loaned out
    /// and returned. This is a useful way to manage resources in multithreaded 
    /// analysis (say where each object represents a complex model and is too heavy 
    /// to clone for every thread).
    /// </summary>
    public class ObjectDispatcher
    {
        private object[] Objects;
        private bool[] IsAvailable;

        //Dummy private object for locking to ensure that only one thread can run the 
        //locked code at a time
        private object lockMe = new object();
        

        /// <summary>
        /// Create a new empty ObjectDispatcher
        /// </summary>
        public ObjectDispatcher()
        {
        }


        /// <summary>
        /// Create a new ObjectDispatcher and initialize it with an array of objects
        /// </summary>
        /// <param name="objects"></param>
        public ObjectDispatcher(object[] objects)
        {
            SetObjects(objects);
        }


        /// <summary>
        /// Initialize the ObjectDispatcher with a new array of objects
        /// </summary>
        /// <param name="objects"></param>
        public void SetObjects(object[] objects)
        {
            Objects = objects;
            IsAvailable = new bool[objects.Length];
            for (int i = 0; i < objects.Length; i++) IsAvailable[i] = true;
        }


        /// <summary>
        /// Borrow an object from the ObjectDispatcher
        /// </summary>
        /// <returns></returns>
        public object Borrow()
        {
            lock (lockMe)
            {
                int n = Objects.Length;
                for (int i = 0; i < n; i++)
                {
                    if (IsAvailable[i])
                    {
                        IsAvailable[i] = false;
                        return Objects[i];
                    }
                }
            }
            throw new Exception("ObjectDispacher could not provide an object because they were all already checked out.");
        }


        /// <summary>
        /// Return an object to the ObjectDispatcher
        /// </summary>
        /// <param name="o"></param>
        public void Return(object o)
        {
            //I dont think we need to lock this code because we are only setting a bool primative
            int n = Objects.Length;
            for (int i = 0; i < n; i++)
            {
                if (Objects[i] == o)
                {
                    IsAvailable[i] = true;
                    return;
                }
            }
        }
    }
}
