namespace IntegrationWithChatGpt
{
    public class Resposta
    {

        public List<Choice> choices { get; set; }
        public Data[] data { get; set; }
        public class Choice
        {
            public string text { get; set; }
        }

        public class Data
        {
            public string url { get; set; }

        }
       
    }
}
