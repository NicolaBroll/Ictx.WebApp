﻿using System;
using System.Collections.Generic;

namespace Ictx.WebApp.Api.Common.HealthCheck;

public class HealthCheckResponse
{
    public string Status { get; set; }
    public IEnumerable<HealthCheck> Checks { get; set; }
    public TimeSpan Duration { get; set; }
}