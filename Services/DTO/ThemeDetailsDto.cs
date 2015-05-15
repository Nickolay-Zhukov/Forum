using System.Collections.Generic;
using Core.Models;

namespace Services.DTO
{
    public class ThemeDetailsDto : ThemeDto
    {
        public IEnumerable<MessageDto> Messages { set; get; }

        #region Constructor
        public ThemeDetailsDto() { }
        public ThemeDetailsDto(Theme theme) : base(theme) { }
        #endregion
    }
}