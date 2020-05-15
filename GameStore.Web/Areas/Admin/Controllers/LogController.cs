using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.LogViewModels;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        private const int _pageSize = 10;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }
        // GET: Admin/Log
        public ActionResult Index(int page = 1)
        {
            var model = GetLogListModel(page);
            return View(model);
        }

        public ActionResult GetLogs(int page = 1)
        {
            var model = GetLogListModel(page);
            return PartialView("_Logs",model);
        }

        public IndexLogViewModel GetLogListModel(int page)
        {
            var logs = _logService.GetLogs();
            var mappLogs = logs.Select(Mapper.Map<Log, LogViewModel>).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, logs.Count(), _pageSize);
            var model = new IndexLogViewModel { Logs = mappLogs, Pager = pager };
            return model;
        }
    }
}