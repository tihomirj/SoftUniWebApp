﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Blog.Classes
{
    public class Utils
    {
        public static string CutText(string text, int MaxLength = 100)
        {
            if (text == null || text.Length <= MaxLength)
                return text;

            var shortText = text.Substring(0, MaxLength - 3) + "...";

            return shortText;

        }
    }
}