using System.Collections.Generic;

namespace Application.Common.Models
{
    public class SelectListVm
    {
        public IList<SelectListDto> List { get; set; } = new List<SelectListDto>();
    }

    public class SelectListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
