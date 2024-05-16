using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GC.Models;
using System.IO;
using Ninject;
using System.Web.Security;
using LibraWebApp.GenericModels;
using NLog;
using LogManager = NLog.LogManager;

namespace GC.GestioneQuestionari.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    #region Loggers

        protected Logger _mainLogger = LogManager.GetCurrentClassLogger();

        protected Logger _errorLogger = LogManager.GetLogger("errors");

        #endregion
        #region UserInfo
        public UserInfo UserInfo
        {
            get
            {
                var ui = Session["userInfo"] as UserInfo;
                return ui;
            }
            set
            {
                Session["userInfo"] = value;
                Session.Timeout = (int)FormsAuthentication.Timeout.TotalMinutes;
            }
        }

        //public void RefreshUserSession()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {//TODO: de vazut poate rezolv problema cu sessiunea care moare inaintea cookie.
        //        if (UserInfo == null)
        //        {
        //            var _userBusiness = new GC.Business.UserRoleBusiness(_mainLogger);
        //            UserInfo = _userBusiness.GetUserInfoByUserName(User.Identity.Name);
        //        }
        //    }
        //}
        //#endregion

        //#region Gestione risposte JSON

        //public enum ErrorSeverity
        //{
        //    Exception,
        //    Validation,
        //    Login
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON KO con i dati dell'eccezione
        ///// </summary>
        ///// <param name="ex"></param>
        //protected virtual JsonResult CreateJsonException(Exception ex)
        //{
        //    return CreateJsonException(ex, ErrorSeverity.Exception);
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON KO con i dati dell'eccezione
        ///// </summary>
        ///// <param name="ex"></param>
        ///// <param name="severity"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonException(Exception ex, ErrorSeverity severity)
        //{
        //    var userMessage = UtilityException.GetErrorMessage(ex);
        //    _errorLogger.Error(ex, "[User: {0}] - Errore: {1}", (UserInfo != null) ? UserInfo.UserName : "unknown", userMessage);
        //    return Json(new { Result = BusinessValues.ExecutionResult.ERRORE, Message = userMessage, Severity = severity.ToString() });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON KO con il messaggio di errore indicato
        ///// </summary>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonException(string message)
        //{
        //    return CreateJsonException(message, ErrorSeverity.Exception);
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON KO con il messaggio di errore indicato
        ///// </summary>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonError(string message)
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.ERRORE, Message = message });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON KO con il messaggio di errore indicato
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="severity"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonException(string message, ErrorSeverity severity)
        //{
        //    _errorLogger.Error("[User: {0}] - Errore: {1}", (UserInfo != null) ? UserInfo.UserName : "unknown", message);
        //    return Json(new { Result = BusinessValues.ExecutionResult.ERRORE, Message = message, Severity = severity.ToString() });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON KO con il messaggio di errore di validazione indicato
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="errorList"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonValidationException(string message, IEnumerable<ModelStateError> errorList)
        //{
        //    _errorLogger.Error("[User: {0}] - Errore: {1}", (UserInfo != null) ? UserInfo.UserName : "unknown", message);
        //    return Json(new { Result = BusinessValues.ExecutionResult.ERRORE, Message = message, Errors = errorList, Severity = ErrorSeverity.Validation.ToString() });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON KO con il messaggio di errore login indicato
        ///// </summary>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonLoginException(string message)
        //{
        //    _errorLogger.Error("[User: {0}] - Errore: {1}", (UserInfo != null) ? UserInfo.UserName : "unknown", message);
        //    return Json(new { Result = BusinessValues.ExecutionResult.ERRORE, Message = message, Severity = ErrorSeverity.Login.ToString() });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON OK
        ///// </summary>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonOk()
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.OK });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON OK con messaggio
        ///// </summary>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonOk(string description)
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.OK, Description = description });
        //}


        ///// <summary>
        ///// Utility creazione risposta JSON OK con un singolo record
        ///// </summary>
        ///// <typeparam name="RecordModelT"></typeparam>
        ///// <param name="rec"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonRecordResult<RecordModelT>(RecordModelT rec) where RecordModelT : class, new()
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.OK, Record = rec });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON OK con i risultati di acquisizione dati
        ///// </summary>
        ///// <typeparam name="RecordModelT"></typeparam>
        ///// <param name="res"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonRecordsResult<RecordModelT>(RecordModelT[] records, int? totalRecordCount) where RecordModelT : class, new()
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.OK, Records = records, TotalRecordCount = totalRecordCount });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON OK con un singolo record
        ///// </summary>
        ///// <typeparam name="RecordModelT"></typeparam>
        ///// <param name="rec"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonRecordResultAction<RecordModelT>(RecordModelT rec, string Action) where RecordModelT : class, new()
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.OK, Record = rec, Action = Action });
        //}


        ///// <summary>
        ///// Utility creazione risposta JSON OK con view html (con model)
        ///// </summary>
        ///// <param name="viewName"></param>
        ///// <param name="model"></param>
        ///// <param name="isPartial"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonViewResult(string viewName, object model = null, bool isPartial = false)
        //{
        //    string s = (string.IsNullOrWhiteSpace(viewName) ? string.Empty : RenderViewToString(this.ControllerContext, viewName, model, isPartial));
        //    return Json(new { Result = BusinessValues.ExecutionResult.OK, ViewHtml = s });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON OK con File Excel
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonFileName(string fileName)
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.OK, FileName = fileName });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON OK con File Excel + path
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonPathAndFileName(string folderPath, string fileName)
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.OK, FolderPath = folderPath, FileName = fileName });
        //}

        ///// <summary>
        ///// Utility creazione risposta JSON KO con messaggio
        ///// </summary>
        ///// <returns></returns>
        //protected virtual JsonResult CreateJsonKO(string message = null)
        //{
        //    return Json(new { Result = BusinessValues.ExecutionResult.KO, Message = message == null ? string.Empty : message });
        //}

        //#endregion

        //#region View utility
        //public static string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
        //{
        //    ViewEngineResult viewEngineResult = null;
        //    if (partial)
        //        viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
        //    else
        //        viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

        //    if (viewEngineResult == null)
        //        throw new System.IO.FileNotFoundException("View cannot be found");

        //    var view = viewEngineResult.View;
        //    context.Controller.ViewData.Model = model;

        //    string result = null;
        //    using (var sw = new StringWriter())
        //    {
        //        var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
        //        view.Render(ctx, sw);
        //        result = sw.ToString();
        //    }
        //    //result = Regex.Replace(result, @"/td>\s+<td", "/td><td");
        //    //result = Regex.Replace(result, @"<tr>\s+<td", "<tr><td");
        //    //result = Regex.Replace(result, @" </td>\s</tr>", " </td></tr>");
        //    return result;
        //}
        //#endregion

        //#region Error
        //public ActionResult ShowError(Exception ex)
        //{
        //    _errorLogger.Error(ex, "Error...");
        //    ViewBag.Message = "Qualcosa è andato male.";
        //    return View("Error");
        //}

        //public ActionResult ShowErrorWithoutBar(Exception ex)
        //{
        //    _errorLogger.Error(ex, "Error...");
        //    ViewBag.Message = "Qualcosa è andato male.";
        //    return View("ErrorWithoutBar");
        //}

        //public ActionResult ShowError(string message)
        //{
        //    ViewBag.Message = message;
        //    return View("Error");
        //}

        //public ActionResult ShowErrorWithoutBar(string message)
        //{
        //    ViewBag.Message = message;
        //    return View("ErrorWithoutBar");
        //}

        //[AllowAnonymous]
        //public ActionResult ShowErrorIE()
        //{
        //    ViewBag.Message = "Per accedere al questionario Fornitori Utilizzare il browser “Google Crome”";
        //    return View("ErrorWithoutBar");
        //}

        [AllowAnonymous]
        public ActionResult ShowErrorIEWithoutBar()
        {
            ViewBag.Message = "Internet Explorer browser detectato! Per accedere il Questionario Fornitori bisogna utilizzare altro browser.";
            return View("ErrorWithoutBar");
        }
        #endregion
    }
}