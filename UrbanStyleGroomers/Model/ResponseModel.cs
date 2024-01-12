using System;
namespace UrbanStyleGroomers.Model
{
    public class ResponseModel
    {
        public string recordId { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public string entity { get; set; }
        public string operation { get; set; }

        public ResponseModel()
        {
        }
    }
}
