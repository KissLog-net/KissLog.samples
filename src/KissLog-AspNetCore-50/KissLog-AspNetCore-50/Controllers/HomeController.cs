﻿using KissLog_AspNetCore_50.ActionFilters;
using KissLog_AspNetCore_50.Exceptions;
using KissLog_AspNetCore_50.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace KissLog_AspNetCore_50.Controllers
{
    [TrackExecutionTime]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        public HomeController(
            IConfiguration configuration,
            ILogger<HomeController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Hello world from KissLog!");

            _logger.LogTrace("Trace message");
            _logger.LogDebug("Debug message");
            _logger.LogInformation("Info message");
            _logger.LogWarning("Warning message");
            _logger.LogError("Error message");
            _logger.LogCritical("Critical message");

            string applicationId = _configuration["KissLog.ApplicationId"];

            var viewModel = new IndexViewModel
            {
                KissLogRequestLogsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslog-aspnetcore-5x",
                LocalTextFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs")
            };

            return View(viewModel);
        }

        public IActionResult TriggerException()
        {
            Random random = new Random();
            int productId = random.Next(1, 10000);

            throw new ProductNotFoundException(productId);
        }
    }
}
