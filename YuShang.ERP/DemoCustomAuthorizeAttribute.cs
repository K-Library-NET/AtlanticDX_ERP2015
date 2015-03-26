using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YuShang.ERP
{
    public class DemoCustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // get the area, controller and action
            var area = filterContext.RouteData.Values["area"];
            var controller = filterContext.RouteData.Values["controller"];
            var action = filterContext.RouteData.Values["action"];
            string verb = filterContext.HttpContext.Request.HttpMethod;

            // these values combined are our roleName
            string roleName = String.Format("{0}/{1}/{2}/{3}", area, controller, action, verb);

            // set role name to area/controller/action name
            this.Roles = roleName;

            System.Console.WriteLine(filterContext.HttpContext.User.Identity.Name);

            base.OnAuthorization(filterContext);
        }
    }

    //public class GenericCompareAttribute : ValidationAttribute, IClientValidatable
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        validationContext.p

    //        return base.IsValid(value, validationContext);
    //    }


    //    public IEnumerable<ModelClientValidationRule>
    //        GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    //    {
    //        string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
    //        ModelClientValidationRule compareRule = new ModelClientValidationRule();

    //        compareRule.ValidationParameters.Add("ID", "");
    //        compareRule.ValidationParameters.Add("Name", "");

    //        compareRule.ErrorMessage = errorMessage;


    //        compareRule.ValidationType = "genericcompare";
    //        compareRule.ValidationParameters.Add("comparetopropertyname", CompareToPropertyName);
    //        compareRule.ValidationParameters.Add("operatorname", OperatorName.ToString());
    //        yield return compareRule;
    //    }
    //}

}