using System;
namespace UrbanStyleGroomers.Model
{
    public class GroomersProfileModel
    {
        public int GroomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageURL { get; set; }
        public bool JobStatus { get; set; }
    }
}
