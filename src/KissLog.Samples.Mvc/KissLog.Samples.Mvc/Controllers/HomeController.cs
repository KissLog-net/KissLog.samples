﻿using KissLog.Samples.Mvc.Exceptions;
using KissLog.Samples.Mvc.Models;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace KissLog.Samples.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController()
        {
            _logger = Logger.Factory.Get();
        }

        public ActionResult Index()
        {
            _logger.Debug("Hello world from AspNet.Mvc!");

            var viewModel = new IndexViewModel
            {
                KissLogRequestLogsUrl = $"https://kisslog.net/RequestLogs/{ConfigurationManager.AppSettings["KissLog.ApplicationId"]}/kisslog-sample",
                LocalTextFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs")
            };

            return View(viewModel);
        }

        public ActionResult TriggerException()
        {
            Random random = new Random();
            int productId = random.Next(1, 10000);

            throw new ProductNotFoundException(productId);
        }
    }
}