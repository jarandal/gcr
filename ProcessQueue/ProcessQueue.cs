using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace ProcessQueue
{
    /// <summary>
    /// Represents a thread-safe (synchronized) in-memory generic Queue that automatically processes
    /// the contained items using a user-supplied delegate. Note that while traditional use of the 
    /// Queue (using Enqueue and Dequeue) will behave as expected, using more than one thread for
    /// processing removes any guarantee of the order in which queued items are processed.
    /// </summary>
    /// <typeparam name="T">The type of the data contained in the ProcessQueue</typeparam>
    public class ProcessQueue<T> : IEnumerable<T>, ICollection, IEnumerable, IDisposable
    {
        #region Instance Variables
        private object syncRoot = new object();
        private Queue<T> queue;
        private List<WorkerThread> threads = new List<WorkerThread>();
        private Action<T> operation;
        bool isDisposed;
        bool isRunning;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new ProcessQueue that is empty and has the default capacity and one worker thread
        /// </summary>
        /// <param name="action">The action to take on queued items.</param>
        public ProcessQueue(Action<T> action) : this(action, 1) { }
        /// <summary>
        /// Initializes a new ProcessQueue that is empty and has the specified capacity with one worker thread.
        /// </summary>
        /// <param name="capacity">The initial capacity of the ProcessQueue.</param>
        /// <param name="action">The action to take on queued items.</param>
        public ProcessQueue(int capacity, Action<T> action) : this(capacity, action, 1) { }
        /// <summary>
        /// Initializes a new ProcessQueue using the specified collection and one worker thread.
        /// </summary>
        /// <param name="collection">The data to copy to the ProcessQueue.</param>
        /// <param name="action">The action to take on queued items.</param>
        public ProcessQueue(IEnumerable<T> collection, Action<T> action) : this(collection, action, 1) { }

        /// <summary>
        /// Initializes a new ProcessQueue that is empty and has the default capacity and the specified 
        /// number of worker threads.
        /// </summary>
        /// <param name="action">The action to take on queued items.</param>
        /// <param name="threadCount">The number of worker threads to create.</param>
        public ProcessQueue(Action<T> action, int threadCount)
        {
            queue = new Queue<T>();
            operation = action;

            SetThreadCount(threadCount);
        }

        /// <summary>
        /// Initializes a new ProcessQueue
        /// </summary>
        /// <param name="capacity">The initial capacity of the ProcessQueue.</param>
        /// <param name="action">The action to take on queued items.</param>
        /// <param name="threadCount">The number of worker threads to create.</param>
        public ProcessQueue(int capacity, Action<T> action, int threadCount)
        {
            queue = new Queue<T>(capacity);
            operation = action;

            SetThreadCount(threadCount);
        }

        /// <summary>
        /// Initializes a new ProcessQueue
        /// </summary>
        /// <param name="collection">The data to copy to the ProcessQueue.</param>
        /// <param name="action">The action to take on queued items.</param>
        /// <param name="threadCount">The number of worker threads to create.</param>
        public ProcessQueue(IEnumerable<T> collection, Action<T> action, int threadCount)
        {
            queue = new Queue<T>(collection);
            operation = action;

            SetThreadCount(threadCount);
        }
        #endregion

        #region Processing Control
        /// <summary>
        /// Stops (suspends) automatic processing of queued items.
        /// </summary>
        public void Stop()
        {
            lock (syncRoot)
            {
                foreach (WorkerThread thread in threads)
                {
                    thread.Pause();
                }

                isRunning = false;
            }
        }

        /// <summary>
        /// Starts automatic processing of queued items.
        /// </summary>
        public void Start()
        {
            lock (syncRoot)
            {
                RegenerateIfDisposed();

                for (int i = 0; i < Math.Min(threads.Count, queue.Count); i++)
                {
                    threads[i].Signal();
                }

                isRunning = true;
            }
        }

        /// <summary>
        /// Gets the number of worker threads in use by this ProcessQueue. Use SetThreadCount to change this value.
        /// </summary>
        public int ThreadCount { get { return threads.Count; } }

        /// <summary>
        /// Sets the number of worker threads in use by this ProcessQueue, and allocates or deallocates threads 
        /// as necessary.
        /// </summary>
        /// <param name="threadCount">The number of threads to use for this ProcessQueue</param>
        public void SetThreadCount(int threadCount)
        {
            if (threadCount < 1) throw new ArgumentOutOfRangeException("threadCount", "The ProcessQueue class requires at least one worker thread.");

            lock (syncRoot)
            {
                int pending = queue.Count;

                for (int i = threads.Count; i < threadCount; i++) // add additional threads if necessary
                {
                    WorkerThread thread = new ProcessQueue<T>.WorkerThread(this);

                    threads.Add(thread);

                    thread.Start();

                    if (pending> 1) 
                    {
                        thread.Signal();
                    }

                    pending--;
                }

                int toRemove = threads.Count - threadCount;

                if (toRemove > 0)
                {
                    foreach (WorkerThread thread in threads.Where(t => !t.IsSignaled).ToList())
                    {
                        thread.Abort();
                        threads.Remove(thread);

                        toRemove--;
                    }

                    while (toRemove > 0)
                    {
                        WorkerThread thread = threads[threads.Count - 1];

                        thread.Abort();

                        threads.Remove(thread);

                        toRemove--;
                    }
                }
            }
        }

        private void ProcessItem(T item)
        {
            operation(item);
        }

        private void RegenerateIfDisposed()
        {
            if (isDisposed)
            {
                int threadCount = threads.Count;

                threads.Clear();

                SetThreadCount(threadCount);
            }

            isDisposed = false;
        }
        #endregion

        /// <summary>
        /// Clears all unprocessed items from the ProcessQueue
        /// </summary>
        public void Clear()
        {
            lock (syncRoot)
            {
                queue.Clear();
            }
        }

        /// <summary>
        /// Attempts to retrieve the next item from the ProcessQueue if one exists. If one does not exist, value is set to its default value.
        /// </summary>
        /// <param name="value">The container for the next value in the ProcessQueue. If none exists, this variable will be set to its default.</param>
        /// <returns>True if the ProcessQueue contained an item, False if it did not.</returns>
        public bool TryDequeue(out T value)
        {
            lock (syncRoot)
            {
                if (queue.Count > 0)
                {
                    value = queue.Dequeue();

                    return true;
                }
                else
                {
                    value = default(T);

                    return false;
                }
            }
        }

        /// <summary>
        /// Determines if the specified item exists in the ProcessQueue
        /// </summary>
        /// <param name="item">The item to locate</param>
        /// <returns>True if the item exists, False if it does not</returns>
        public bool Contains(T item)
        {
            lock (syncRoot)
            {
                return queue.Contains(item);
            }
        }

        /// <summary>
        /// Copies the contents of the ProcessQueue to an external array without
        /// affecting the contents of the ProcessQueue
        /// </summary>
        /// <param name="array">The array to copy the items into</param>
        /// <param name="arrayIndex">The starting index in the array</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (syncRoot)
            {
                queue.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Retrieves the next unprocessed item from the ProcessQueue and removes it.
        /// </summary>
        /// <returns>The next unprocessed item in the ProcessQueue</returns>
        public T Dequeue()
        {
            lock (syncRoot)
            {
                return queue.Dequeue();
            }
        }

        /// <summary>
        /// Adds an item to the end of the processing queue.
        /// </summary>
        /// <param name="item">The item to add</param>
        public void Enqueue(T item)
        {
            lock (syncRoot)
            {
                queue.Enqueue(item);

                if (isRunning)
                {
                    RegenerateIfDisposed();

                    WorkerThread firstThread = threads.Where(t => !t.IsSignaled).FirstOrDefault();

                    if (firstThread != null) firstThread.Signal();
                }
            }
        }

        /// <summary>
        /// Retrieves the next unprocessed item from the ProcessQueue without removing it
        /// </summary>
        /// <returns>The next unprocessed item in the ProcessQueue</returns>
        public T Peek()
        {
            lock (syncRoot)
            {
                return queue.Peek();
            }
        }

        /// <summary>
        /// Returns an array containing all of the unprocessed items in the ProcessQueue
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            lock (syncRoot)
            {
                return queue.ToArray();
            }
        }

        /// <summary>
        /// Sets the capacity of the ProcessQueue to the actual number of items it contains,
        /// unless that number is greather than 90% of the current capacity.
        /// </summary>
        public void TrimExcess()
        {
            lock (syncRoot)
            {
                queue.TrimExcess();
            }
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            lock (syncRoot)
            {
                ((ICollection)queue).CopyTo(array, index);
            }
        }

        public int Count
        {
            get
            {
                lock (syncRoot)
                {
                    return queue.Count;
                }
            }
        }

        bool ICollection.IsSynchronized
        {
            get { return true; }
        }

        object ICollection.SyncRoot
        {
            get { return syncRoot; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (WorkerThread thread in threads) thread.Abort();
            }

            isDisposed = true;
        }

        #endregion

        /// <summary>
        /// Encapsulates a .NET Thread object and manages the WaitHandles associated with controlling its behavior
        /// </summary>
        private class WorkerThread
        {
            private ManualResetEvent abortEvent;
            private ManualResetEvent signalEvent;
            private ProcessQueue<T> queue;

            private Thread thread;

            public WorkerThread(ProcessQueue<T> queue)
            {
                abortEvent = new ManualResetEvent(false);
                signalEvent = new ManualResetEvent(false);
                this.queue = queue;

                thread = new Thread(ThreadProc);
                thread.Name = "ProcessQueue Worker ID " + thread.ManagedThreadId;
            }

            public void Start()
            {
                thread.Start();
            }

            public void Abort()
            {
                abortEvent.Set();

                thread.Join();
            }

            /// <summary>
            /// Clears the signaling WaitHandle, causing the thread to pause after completing its current
            /// iteration
            /// </summary>
            public void Pause()
            {
                signalEvent.Reset();
            }

            /// <summary>
            /// Sets the signaling WaitHandle, causing it to resume operation (if paused)
            /// </summary>
            public void Signal()
            {
                signalEvent.Set();
            }

            public bool IsSignaled
            {
                get { return signalEvent.WaitOne(0); }
            }

            /// <summary>
            /// The overall thread method, consisting of an infinite loop that exits upon signaling the abort event
            /// </summary>
            private void ThreadProc()
            {
                WaitHandle[] handles = new WaitHandle[] { signalEvent, abortEvent };

                while (true)
                {
                    switch (WaitHandle.WaitAny(handles))
                    {
                        case 0: // signal
                            {
                                ProcessItems();
                            } break;
                        case 1: // abort
                            {
                                return;
                            }
                    }
                }
            }

            private void ProcessItems()
            {
                T item;

                while (queue.TryDequeue(out item))
                {
                    queue.ProcessItem(item);

                    if (!signalEvent.WaitOne(0) || abortEvent.WaitOne(0)) return;
                }

                signalEvent.Reset();
            }
        }
    }
}
