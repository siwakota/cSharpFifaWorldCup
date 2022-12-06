 namespace A2AnishSiwakoti
{
    public class Data
    {
        private static string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                          Initial Catalog=FifaWorldCupDB;
                                          Integrated Security=True";

        public static string ConnectionStr { get { return connStr; } }
    }
}
