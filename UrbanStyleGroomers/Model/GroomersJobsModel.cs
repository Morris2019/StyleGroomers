using System;
namespace UrbanStyleGroomers.Model
{
    public class GroomersJobsModel
    {
        public int JobId { get; set; }
        public string JobService { get; set; }
        public string JobDate { get; set; }
        public string JobLantitude { get; set; }
        public string JobLontitude { get; set; }
        public bool JobStatus { get; set; }
    }
}
