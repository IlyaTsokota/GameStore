using System;

using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class Log
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string Thread { get; set; }

        [MaxLength(50)]
        public string Level { get; set; }

        [MaxLength(255)]
        public string Logger { get; set; }

        [MaxLength(4000)]
        public string Message { get; set; }

        [MaxLength(2000)]
        public string Exception { get; set; }
    }
}
