﻿namespace Application.Common.Interfaces;

public interface IClock
{
    DateTime UtcNow => DateTime.Now;
}