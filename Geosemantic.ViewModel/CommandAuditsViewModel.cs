﻿using System;

namespace Ste.ViewModel
{
    public class CommandAuditsViewModel
    {
        public long Id { get; set; }
        public int LoggedOnUserId { get; set; }
        public DateTime UtcTimeStamp { get; set; }
        public Guid MessageId { get; set; }
        public bool IsSuccess { get; set; }
        public string ExceptionMessage { get; set; }
        public string CommandType { get; set; }
        public string CommandData { get; set; }
        public int Milliseconds { get; set; }
    }
}
