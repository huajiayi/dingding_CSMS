﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractStatementManagementSystem;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
namespace WebApplication4.Controllers
{
    public class ProjectController : Controller
    {
        // GET: project
        public ActionResult Project()
        {
            if (Session["cc"] != null)
            {
                ViewBag.Message = Session["cc"];
            }
            string s = ViewBag.Message;
            Guid ID = new Guid(s);
            ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
            ObservableCollection<Project_data> pd= SqlQuery.Project_dataQuery(ID);
            pd=Orderby.Project_dataPaixu(pd);
            ObservableCollection<ProjectLog> pl= SqlQuery.ProjectLogQuery(ID);
            pl=Orderby.ProjectLogPaixu(pl);
            ViewBag.ContractName = ct[0].ContractName;
            ViewBag.ProjectLogJson = JsonTools.ObjectToJson(pl);
            ViewBag.Project_dataJson = JsonTools.ObjectToJson(pd);
            return View();
        }
        public ActionResult projectLog()
        {
            if (Session["cc"] != null)
            {
                ViewBag.Message = Session["cc"];
            }
            string s = ViewBag.Message;
            Guid ID = new Guid(s);
            string ss = Request["ID"];
            Guid ID2 = new Guid(ss);
            ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
            ObservableCollection<ProjectLog> pl = SqlQuery.ProjectLogByIDQuery(ID2);
            ViewBag.ContractName = ct[0].ContractName;
            
            return View(pl[0]);
        }
        public ActionResult addProjectLog()
        {
            if (Session["cc"] != null)
            {
                ViewBag.Message = Session["cc"];
            }
            string s = ViewBag.Message;
            Guid ID = new Guid(s);
            ObservableCollection<Contract_Data> cd = SqlQuery.ContractDataQuery(ID);
            cd = Orderby.paiXu(cd);
            ViewBag.Contract_DataJson = JsonTools.ObjectToJson(cd);
            return View();
            
        }
        public ActionResult saveProjectLog(ProjectLog pl)
        {
            string jc = "";
            string s2 = Request["txt_Type"];
            Project_data pd = null;
            if (Session["cc"] != null)
            {
                ViewBag.Message = Session["cc"];
            }
            string s = ViewBag.Message;
            Guid ID = new Guid(s);
            ObservableCollection<Project_data> opd = SqlQuery.Project_dataQueryByService(pl.ServiceID);
            pd = opd[0];
            ObservableCollection<Contract_Data> cd = SqlQuery.Contract_DataByIDQuery(pl.ServiceID);
            pl.Service = cd[0].Service;
            pl.ID = Guid.NewGuid();
            pl.LogDate = DateTime.Now.ToString();
            pl.ContractID = ID;
            if (s2== "ProjectStart")
            {
                jc = pd.ProjectStart;
                if (pl.ProjectStart == null) {
                    pd.ProjectStart = " ";
                }
                pd.ProjectStart = pl.ProjectStart;
                pl.ProjectStart = jc + " 更改为 " + pd.ProjectStart;
            }
            if (s2 == "DompletedDate")
            {
                jc = pd.DompletedDate;
                if (pl.DompletedDate == null)
                {
                    pd.DompletedDate = " ";
                }
                pd.DompletedDate = pl.DompletedDate;
                pl.DompletedDate =jc+ " 更改为 " + pl.DompletedDate;
            }
            if (s2=="DompletedAcceptanceDate")
            {
                jc = pd.DompletedAcceptanceDate;
                if (pl.DompletedAcceptanceDate == null)
                {
                    pd.DompletedAcceptanceDate = " ";
                }
                pd.DompletedAcceptanceDate = pl.DompletedAcceptanceDate;
                pl.DompletedAcceptanceDate =jc+ " 更改为 " + pl.DompletedAcceptanceDate;
            }  
            GetData.ProjectGet(pl,pd);
            return RedirectToAction("Project");

        }
    }
}