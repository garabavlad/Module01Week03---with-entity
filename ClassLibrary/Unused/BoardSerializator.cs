using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary;

namespace Depricated
{
    public class BoardSerializator
    {

        // Method for serialization istance:
        public static Board DeserializeBoard()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("BoardDB.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            Board obj = null;
            try
            {
                obj = (Board)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e);
            }
            stream.Close();
            return obj;
        }

        // Method for deserialization istance:
        public void SerializeBoard(Board board)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("BoardDB.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, board);
            stream.Close();
        }

    }
}
