using System.IO;
using System.Xml.Serialization;
using Checkers.Servicies;

public class SerializationActions
{
    public void SerializeGame(GameSerialization gameSerialization)
    {
        XmlSerializer xmlser = new XmlSerializer(typeof(GameSerialization));
        FileStream fileStr = new FileStream("gameData.xml", FileMode.Create);
        xmlser.Serialize(fileStr, gameSerialization);
        fileStr.Dispose();
    }

    public GameSerialization DeserializeGame()
    {
        XmlSerializer xmlser = new XmlSerializer(typeof(GameSerialization));
        FileStream file = new FileStream("gameData.xml", FileMode.Open);
        var gameSerialization = xmlser.Deserialize(file) as GameSerialization;
        file.Dispose();
        return gameSerialization;
    }
}
