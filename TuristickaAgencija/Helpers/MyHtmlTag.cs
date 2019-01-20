using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Helpers
{
    public class MyHtmlTag
    {
        public static HtmlString DoubleDropDown(string name, string name2, List<SelectListItem> items, List<SelectListItem> items2, string klass)
        {
            string tag = string.Empty;
            tag += "<select style='width:62%; margin-right:2%; float:left; margin-bottom:1%;' name='" + name + "' class='" + klass + "'>";
            foreach (var x in items)
            {
                if (x.Selected)
                    tag += "<option selected value='" + x.Value + "'>" + x.Text + "</option>";
                else
                    tag += "<option value='" + x.Value + "'>" + x.Text + "</option>";

            }
            tag += "</select>";
            //za drugu listu
            tag += "<select style='width:36%;margin-bottom:1%;' name='" + name2 + "' class='" + klass + "'>";
            foreach (var x in items2)
            {
                if (x.Selected)
                    tag += "<option selected value='" + x.Value + "'>" + x.Text + "</option>";
                else
                    tag += "<option value='" + x.Value + "'>" + x.Text + "</option>";

            }
            tag += "</select>";


            return new HtmlString(tag);
        }

        public static HtmlString SingleDropDown(string name, List<SelectListItem> items, string klass)
        {
            string tag = string.Empty;
            tag += "<select name='" + name + "' class='" + klass + "'>";
            foreach (var x in items)
            {
                if (x.Selected)
                    tag += "<option selected value='" + x.Value + "'>" + x.Text + "</option>";
                else
                    tag += "<option value='" + x.Value + "'>" + x.Text + "</option>";

            }
            tag += "</select>";

            return new HtmlString(tag);
        }


    }
}
