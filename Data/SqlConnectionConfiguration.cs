namespace RealbnbBeta.Data
{
    public class SqlConnectionConfiguration
    {
        public static string ConnectionString { get; set; }
        public SqlConnectionConfiguration(string value) => Value = value;
        public string Value { get; }
    }
}
