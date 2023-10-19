using System.Reflection;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace Homework4
{
    internal class StringSerializer
    {
        Type _type;
        FieldInfo[] _fields;
        private StringSerializer() { }
        public StringSerializer(Type type) {
            _type = type;
            _fields = type.GetFields();
        }
        public void Serialize(FileStream writer, object obj)
        {
            var sbName = new List<string>();
            var sbValue = new List<object?>(); 
            foreach (FieldInfo property in _fields)
            {  
                if (property.FieldType.IsSerializable) { 
                    sbName.Add(property.Name);
                    sbValue.Add(property.GetValue(obj));
                }
            }
            
            var name = String.Join(";", sbName.Select(p => p.ToString()).ToArray());
            var value = String.Join(";", sbValue.Select(p => p.ToString()).ToArray());
            var res = Encoding.Default.GetBytes($"{name}\n{value}");
            writer.WriteAsync(res, 0, res.Length);  
            writer.Flush();
        }
        public object? Deserialize(FileStream reader)
        { 
            var file = Encoding.Default.GetString(ReadFile(reader)).Split('\n');
            var names = file[0].Split(';');
            var values = file[1].Split(';'); 
            var res = Activator.CreateInstance(_type);
            
            for (int i = 0; i < names.Length; i++)
            {
                var typeConverter = TypeDescriptor.GetConverter(_type.GetField(names[i]).FieldType);
                var propValue = typeConverter.ConvertFromString(values[i]);
                _type.GetField(names[i]).SetValue(res, propValue);
            }
            return res;
        }
        private byte[] ReadFile(FileStream fs)
        { 
            var buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            return buffer;
        }
    }
}
