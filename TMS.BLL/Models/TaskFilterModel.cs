﻿using TMS.BLL.Models;

namespace TMS.BLL.Models
{
    public class TaskFilterModel
    {
        public TaskStatus? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority? Priority { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
