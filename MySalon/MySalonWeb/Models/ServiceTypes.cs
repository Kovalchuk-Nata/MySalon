using System.ComponentModel.DataAnnotations;

namespace MySalonWeb.Models
{
    public enum ServiceTypes
    {
        [Display(Name = "HairCut and Style")]
        HairCutStyle = 0,

        [Display(Name = "HairDyed")]
        HairDyed = 1,

        [Display(Name = "Shaving")]
        Shaving = 2,

        [Display(Name = "Manicure")]
        Manicure = 3,

        [Display(Name = "Eyebrows")]
        Eyebrows = 4,

        [Display(Name = "MakeUp")]
        MakeUp = 5
    }

}
