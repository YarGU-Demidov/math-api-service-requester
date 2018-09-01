using System;
using System.Collections;
using System.Collections.Generic;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public class MethodArgs : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly List<KeyValuePair<string, string>> _args;

        public MethodArgs()
        {
            _args = new List<KeyValuePair<string, string>>();
        }

        public MethodArgs(IDictionary<string, string> args) : this()
        {
            AddRange(args);
        }

        public MethodArgs(IDictionary<string, IEnumerable<string>> args) : this()
        {
            AddRange(args);
        }

        public void Add(string key, string value)
        {
            Add(new KeyValuePair<string, string>(key, value));
        }

        public void Add(string key, IEnumerable<string> value)
        {
            Add(new KeyValuePair<string, IEnumerable<string>>(key, value));
        }

        public void Add(KeyValuePair<string, string> arg)
        {
            _args.Add(arg);
        }

        public void Add(KeyValuePair<string, IEnumerable<string>> arg)
        {
            foreach (var value in arg.Value)
            {
                Add(new KeyValuePair<string, string>(arg.Key, value));
            }
        }

        public void AddRange(IDictionary<string, string> args)
        {
            foreach (var arg in args)
            {
                Add(arg);
            }
        }

        public void AddRange(IDictionary<string, IEnumerable<string>> args)
        {
            foreach (var arg in args)
            {
                Add(arg);
            }
        }

        [Obsolete("Use constructor.", true)]
        public static implicit operator MethodArgs (Dictionary<string, string> args)
        {
            return new MethodArgs(args);
        }

        [Obsolete("Use constructor.", true)]
        public static implicit operator MethodArgs (Dictionary<string, IEnumerable<string>> args)
        {
            return new MethodArgs(args);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _args.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}