using Lab_1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public static class RecordTypeGetter
    {
        public static int Get(TaskRecord record) => 1;
        public static int Get(TaskWithCustomerRecord record) => 2;
        public static int Get(TaskWithMoneyRecord record) => 3; 
    }

    public static class RecordReader
    {
        public static void Read(StreamReaderHelper helper, TaskRecord task)
        {
            Read(helper, task as AbstractTaskRecord);
        }

        public static void Read(StreamReaderHelper helper, TaskWithCustomerRecord task)
        {
            Read(helper, task as AbstractTaskRecord);

            task.customer = helper.ReadString();
        }

        public static void Read(StreamReaderHelper helper, TaskWithMoneyRecord task)
        {
            Read(helper, task as AbstractTaskRecord);

            task.money = (uint)helper.ReadInt();
        }

        private static void Read(StreamReaderHelper helper, AbstractTaskRecord task)
        {
            task.id = helper.ReadInt();
            task.description = helper.ReadString();
            task.status = helper.ReadString();
            task.executor = helper.ReadString();
            task.dateEnd = helper.ReadDateTime();
        }
    }

    public class FileDataSource : IDataSource
    {
        string     signature  = "daniilba";
        int bytesOnRecord = 414;

        FileStream fileStream;
        Dictionary<byte, Func<AbstractTaskRecord>> recordFabric;

        StreamWriterHelper streamWriter;
        StreamReaderHelper streamReader;

        public FileDataSource(string path, Dictionary<byte, Func<AbstractTaskRecord>> recordFabric)
        {
            this.recordFabric = recordFabric;

            if (File.Exists(path))
            {
                fileStream = new FileStream(path, FileMode.Open);
                if (ValidateSignature() == false)
                    throw new Exception("Неверная сигнатура файла");
            }
            else
            {
                fileStream = new FileStream(path, FileMode.Create);
                var buf = Encoding.ASCII.GetBytes(signature);
                fileStream.Write(buf);
                fileStream.Flush();
            }
            streamWriter = new StreamWriterHelper(fileStream);
            streamReader = new StreamReaderHelper(fileStream);
        }

        private bool ValidateSignature()
        {
            var buf = new byte[8];
            fileStream.Read(buf);
            var str = Encoding.Default.GetString(buf);
            return signature == str;
        }

        private void StreamSetStart()
        {
            fileStream.Seek(8, SeekOrigin.Begin);
        }

        private bool SkipRecords(int count)
        {
            var bytesCount = count * bytesOnRecord;
            if (bytesCount >= fileStream.Length) return false;
            fileStream.Seek(bytesCount + 8, SeekOrigin.Begin);
            return true;
        }

        private void Alignment()
        {
            var mod = ((fileStream.Position - 8) % bytesOnRecord);
            var d = (bytesOnRecord - mod) % bytesOnRecord;
            for (int i = 0; i < d; i++)
            {
                fileStream.WriteByte(0);
            }
            ;
        }

        private int ReadId()
        {
            fileStream.Seek(2, SeekOrigin.Current);
            return streamReader.ReadInt();
        }

        private AbstractTaskRecord ReadRecord()
        {
            if (fileStream.ReadByte() == 1)
            {
                fileStream.Seek(bytesOnRecord - 1, SeekOrigin.Current);
                return null;
            }

            var recordType = (byte)fileStream.ReadByte();
            var created = recordFabric[recordType]();

            RecordReader.Read(streamReader, (dynamic)created);

            Alignment();

            return created;
        }

        public bool Delete(int id)
        {
            if (SkipRecords(id - 1) == false)
                return false;

            fileStream.WriteByte(1);
            return true;
        }

        public AbstractTaskRecord Get(int id)
        {
            if (SkipRecords(id - 1) == false)
                return null;

            return ReadRecord();
        }

        public List<AbstractTaskRecord> GetAll()
        {
            StreamSetStart();
            var allRecord = new List<AbstractTaskRecord>();

            while(fileStream.Position < fileStream.Length)
            {
                var record = ReadRecord();
                if (record == null) continue;
                allRecord.Add(record);
            }

            return allRecord;
        }

        public AbstractTaskRecord Save(AbstractTaskRecord record)
        {
            var id = record.id;
            var cloned = record.Clone();

            if (id != 0)
                SkipRecords(id - 1);
            else
            {
                if (fileStream.Length == 8) cloned.id = 1;
                else
                {
                    fileStream.Seek(-bytesOnRecord, SeekOrigin.End);
                    cloned.id = ReadId() + 1;
                    fileStream.Seek(0, SeekOrigin.End);
                }
            }

            
            fileStream.WriteByte(0);
            var recordType = RecordTypeGetter.Get((dynamic)cloned);
            fileStream.WriteByte((byte)recordType);

            var childFields = cloned
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .ToList();

            var parentFields = cloned
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(v => !childFields.Contains(v))
                .ToList();

            foreach (var field in parentFields.Union(childFields))
            {
                dynamic value = field.GetValue(cloned);
                streamWriter.Write(value);
            }

            Alignment();

            return cloned;
        }
    }

    public class StreamWriterHelper
    {
        FileStream fileStream;

        public StreamWriterHelper(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        public void Write(string value)
        {
            var buf = Encoding.Default.GetBytes(value);
            var d = 100 - buf.Length;
            fileStream.Write(buf);
            for (int i = 0; i < d; i++) fileStream.WriteByte(0);
        }

        public void Write(int value)
        {
            fileStream.Write(BitConverter.GetBytes(value));
        }

        public void Write(uint value)
        {
            fileStream.Write(BitConverter.GetBytes(value));
        }

        public void Write(DateTime value)
        {
            fileStream.Write(BitConverter.GetBytes(value.ToBinary()));
        }
    }

    public class StreamReaderHelper
    {
        FileStream fileStream;

        public StreamReaderHelper(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        public string ReadString()
        {
            return ReadString(100).Trim('\0');  
        }

        public int ReadInt()
        {
            var buf = new byte[4];
            fileStream.Read(buf);
            return BitConverter.ToInt32(buf);
        }

        public uint ReadUInt()
        {
            var buf = new byte[4];
            fileStream.Read(buf);
            return BitConverter.ToUInt32(buf);
        }

        public DateTime ReadDateTime()
        {
            var buf = new byte[8];
            fileStream.Read(buf);
            return DateTime.FromBinary(BitConverter.ToInt64(buf));
        }

        private string ReadString(int count)
        {
            var buf = new byte[count];
            fileStream.Read(buf, 0, count);
            return Encoding.Default.GetString(buf);
        }
    }
}
