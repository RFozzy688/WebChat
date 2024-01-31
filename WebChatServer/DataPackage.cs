

namespace WebChatServer
{
    // тип пакета данных, для выбора типа десериализации строки с данными
    public enum TypeData
    {
        Message,
        Registration,
        Authorization,
        Verification
    }

    // пакет с данными для десериализации
    public class DataPackage
    {
        // тип данных для выбора типа десериализации строки с данными
        public TypeData Package { get; set; }

        // строка с сериализованными данными
        public string StringSerialize { get; set; }
    }
}
