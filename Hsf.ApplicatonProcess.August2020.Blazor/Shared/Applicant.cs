using System.ComponentModel.DataAnnotations;

namespace Hsf.ApplicatonProcess.August2020.Blazor.Shared
{
    public class Applicant
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Family name is required!")]
        [MinLength(5)]
        public string FamilyName { get; set; }
        [Required(ErrorMessage = "Address is required!")]
        [MinLength(10)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Country is required!")]
        [MinLength(5)]
        public string CountryOfOrigin { get; set; }
        [Required(ErrorMessage = "Email address is required!")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Age is required!")]
        [Range(20, 60)]
        public int Age { get; set; }
        public bool Hired { get; set; }
    }
}
