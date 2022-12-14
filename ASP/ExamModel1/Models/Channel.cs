namespace ExamModel1.Models
{
    public class Channel
    {
        public int id { get; set; }
        public int ownerid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string subscribers { get; set; }
    }
}