using System;

namespace DreamsComeTrueAPI.Dtos
{
    public class EventToAddDto
    {
        public TodoItemDto TodoItem { get; set; }
        public DateTime PlannedFor { get; set; }
    }
}