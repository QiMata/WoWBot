using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WoWBot.Client.Plugins.Logging
{
    class Logger<T>
    {
        private const int MaxBufferDefault = 10;

        private readonly string _fileName;
        private readonly int _maxBuffer;

        private readonly List<T> _items;

        public Logger(string fileName, int maxBuffer = MaxBufferDefault)
        {
            _fileName = fileName;
            _maxBuffer = maxBuffer;

            _items = new List<T>();
        }

        public void AddRecord(T item)
        {
            _items.Add(item);

            if (_items.Count > _maxBuffer)
            {
                WriteToFile();
            }
        }

        private void WriteToFile()
        {
            var json = JsonConvert.SerializeObject(_items);
            _items.Clear();
            File.AppendAllText(_fileName,json);
        }
    }
}
