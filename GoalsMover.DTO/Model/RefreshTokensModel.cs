using System;
using System.Collections.Generic;
using System.Text;

namespace GoalsMover.DTO.Model
{
    public class RefreshTokensModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
