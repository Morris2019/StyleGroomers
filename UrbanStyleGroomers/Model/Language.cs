using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class Language
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
