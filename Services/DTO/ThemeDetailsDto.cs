using System.Collections.Generic;

namespace Services.DTO
{
    public class ThemeDetailsDto : ThemeDto
    {
        public IEnumerable<MessageDto> Messages { set; get; }
    }
}