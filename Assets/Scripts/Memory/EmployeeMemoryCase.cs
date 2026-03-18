using System;
using System.Collections.Generic;

[Serializable]
public class EmployeeMemoryCase
{
    public string employeeId;
    public int remainingHackCount = 2;
    public List<MemorySegment> segments = new List<MemorySegment>();
}